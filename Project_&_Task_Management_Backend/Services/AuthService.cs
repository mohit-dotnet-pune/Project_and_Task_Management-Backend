using Microsoft.AspNetCore.Identity;

using Microsoft.EntityFrameworkCore;

using Microsoft.IdentityModel.Tokens;

using System.IdentityModel.Tokens.Jwt;

using System.Security.Claims;

using System.Text;

using Project___Task_Management_Backend.Interfaces;
using Project___Task_Management_Backend.Data;
using Project___Task_Management_Backend.Helpers;
using Project___Task_Management_Backend.Models;
using Project___Task_Management_Backend.DTO.UserDtos;

namespace Project___Task_Management_Backend.Services
{

    public class AuthService : IAuthService

    {
        private static Dictionary<string, RegisterDto> tempRegisterData = new();
        private static Dictionary<string, (string Otp, DateTime Expiry)> tempOtps = new();

        private readonly AppDbContext _db;

        private readonly EmailHelper _emailHelper;

        private readonly IConfiguration _config;

        private readonly PasswordHasher<User> _passwordHasher;

        public AuthService(AppDbContext db, EmailHelper emailHelper, IConfiguration config)

        {

            _db = db;

            _emailHelper = emailHelper;

            _config = config;

            _passwordHasher = new PasswordHasher<User>();

        }

        private string GenerateOtp(int length = 6)

        {

            var rnd = new Random();

            return string.Concat(Enumerable.Range(0, length).Select(_ => rnd.Next(0, 10)));

        }


        public async Task<(bool Success, string Message)> RegisterAsync(RegisterDto dto)
        {
            // Validate role
            if (!Enum.TryParse<Role>(dto.Role, true, out Role selectedRole))
                return (false, "Invalid role. Allowed roles: Employee, Manager.");

            // Check if email already registered in DB
            if (await _db.users.AnyAsync(u => u.userEmail == dto.Email))
                return (false, "User already exists.");

            // Store temporarily (override previous)
            tempRegisterData[dto.Email] = dto;

            return (true, "Registration details saved. Please request OTP using /get-otp.");
        }

        public async Task<(bool Success, string Message)> GetOtpAsync(GetOtpDto dto)
        {
            if (string.IsNullOrEmpty(dto.Email))
                return (false, "Email is required.");

            string email = dto.Email.ToLower();

            // 1️⃣ Check if user exists in DB
            var existingUser = await _db.users.FirstOrDefaultAsync(u => u.userEmail.ToLower() == email);

            // 2️⃣ Check temp registration if not in DB
            if (existingUser == null && !tempRegisterData.ContainsKey(email))
                return (false, "No registration found. Please register first.");

            // 3️⃣ If user exists and email is already verified
            if (existingUser != null && existingUser.EmailConfirmed && !dto.ForceSend)
                return (false, "Email already verified. Set ForceSend=true to resend OTP.");

            // 4️⃣ Generate OTP
            var otp = GenerateOtp();
            tempOtps[email] = (otp, DateTime.UtcNow.AddMinutes(10));

            // 5️⃣ Send OTP
            _emailHelper.Send(email, "Your OTP Code", $"Your OTP is: {otp}");

            return (true, existingUser != null ? "OTP sent successfully." : "OTP sent for pending registration.");
        }









        public async Task<(bool Success, string Message)> VerifyEmailOtpAsync(VerifyEmailOtpDto dto)
        {
            var email = dto.Email;

            // 1️⃣ Check if user exists
            var existingUser = await _db.users.FirstOrDefaultAsync(u => u.userEmail == email);

            if (existingUser != null && existingUser.EmailConfirmed)
                return (false, "Email is already verified.");

            // 2️⃣ Check OTP exists
            if (!tempOtps.ContainsKey(email))
                return (false, "Please request OTP first.");

            var (otp, expiry) = tempOtps[email];

            if (expiry < DateTime.UtcNow)
                return (false, "OTP expired.");

            if (otp != dto.Otp)
                return (false, "Invalid OTP.");

            // 3️⃣ If user exists but not verified → mark verified
            if (existingUser != null)
            {
                existingUser.EmailConfirmed = true;
                await _db.SaveChangesAsync();

                _emailHelper.Send(email, "Email Verified!", "Your email has been successfully verified.");

                tempOtps.Remove(email);
                return (true, "Email verified successfully.");
            }

            // 4️⃣ If user not in DB → register from temp data
            var regData = tempRegisterData[email];
            Enum.TryParse<Role>(regData.Role, true, out Role role);

            var user = new User
            {
                userName = regData.Username,
                userEmail = regData.Email,
                userRole = role,
                userPassword = _passwordHasher.HashPassword(null, regData.Password),
                EmailConfirmed = true
            };

            await _db.users.AddAsync(user);
            await _db.SaveChangesAsync();

            tempOtps.Remove(email);
            tempRegisterData.Remove(email);

            _emailHelper.Send(email, "Email Verified!", "Your email has been successfully verified.");

            return (true, "Email verified and user registered successfully!");
        }





        // ----------------------------------------------------------------------

        // LOGIN (JWT + SAVE TOKEN IN DB)

        // ----------------------------------------------------------------------

        public async Task<(bool Success, UserResponseDto User, string Message)> LoginAsync(LoginDto dto)
        {
            // 1️⃣ Check if user exists
            var user = await _db.users.FirstOrDefaultAsync(u => u.userEmail == dto.Email);

            if (user == null)
                return (false, null, "User not registered.");

            // 2️⃣ Check email verification
            if (!user.EmailConfirmed)
                return (false, null, "Please verify your email before logging in.");

            // 3️⃣ Validate password
            var verify = _passwordHasher.VerifyHashedPassword(user, user.userPassword, dto.Password);

            if (verify == PasswordVerificationResult.Failed)
                return (false, null, "Invalid credentials.");

            // 4️⃣ Generate JWT


            var token = GenerateJwtToken(user);

            // 5️⃣ Save JWT in database
            user.JwtToken = token;
            user.JwtTokenExpiry = DateTime.UtcNow.AddMinutes(_config.GetValue<int>("Jwt:ExpiryMinutes"));

            // 6️⃣ Refresh Token
            user.RefreshToken = Guid.NewGuid().ToString("N");
            user.RefreshTokenExpiry = DateTime.UtcNow.AddDays(7);

            _db.users.Update(user);
            await _db.SaveChangesAsync();

            // 7️⃣ Prepare sanitized user response
            var responseUser = new UserResponseDto
            {
                userId = user.userId,
                userName = user.userName,
                userEmail = user.userEmail,
                userRole = user.userRole,
                EmailConfirmed = user.EmailConfirmed,
                JwtToken = user.JwtToken,
                JwtTokenExpiry = (DateTime)user.JwtTokenExpiry,
                RefreshToken = user.RefreshToken,
                RefreshTokenExpiry = (DateTime)user.RefreshTokenExpiry
            };

            // 8️⃣ Return response
            return (true, responseUser, "Login successful.");
        }



        private string GenerateJwtToken(User user)
        {
            var key = Environment.GetEnvironmentVariable("JWT_KEY");
            var issuer = Environment.GetEnvironmentVariable("JWT_ISSUER");
            var audience = Environment.GetEnvironmentVariable("JWT_AUDIENCE");
            var minutes = int.Parse(Environment.GetEnvironmentVariable("JWT_EXPIRY_MINUTES") ?? "6000");

            if (string.IsNullOrEmpty(key))
                throw new Exception("JWT Key is missing. Check your .env file.");

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
            var creds = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {

                new Claim("username", user.userName),

                new Claim("email", user.userEmail),

                new Claim(ClaimTypes.Role, user.userRole.ToString()),
                new Claim("userId", user.userId.ToString()),

                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())

            };

            var token = new JwtSecurityToken(
                issuer: issuer,
                audience: audience,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(minutes),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }


        // ----------------------------------------------------------------------

        // FORGOT PASSWORD

        // ----------------------------------------------------------------------

        public async Task<(bool Success, string Message)> ForgotPasswordAsync(ForgotPasswordDto dto)

        {

            var user = await _db.users.FirstOrDefaultAsync(u => u.userEmail == dto.Email);

            if (user == null) return (false, "User not found.");

            var otp = GenerateOtp(6);

            user.PasswordResetOtp = otp;

            user.PasswordResetOtpExpiry = DateTime.UtcNow.AddMinutes(15);

            _db.users.Update(user);

            await _db.SaveChangesAsync();

            //var subject = "Reset Password OTP";

            //var message = $"Your OTP is <b>{otp}</b>.";
            var subject = "Skedulo - Reset Password OTP";
            var message = $"Hello {user.userName},<br/><br/>" +
                          $"Your Password Reset OTP is <b>{otp}</b>.<br/><br/>" +
                          "Please use this OTP to verify your email within 15 minutes.";

            _emailHelper.Send(user.userEmail, subject, message);

            return (true, "OTP sent successfully.");

        }

        // ----------------------------------------------------------------------

        // RESET PASSWORD

        // ----------------------------------------------------------------------

        public async Task<(bool Success, string Message)> ResetPasswordAsync(ResetPasswordDto dto)
        {
            var user = await _db.users.FirstOrDefaultAsync(u => u.userEmail == dto.Email);

            if (user == null)
                return (false, "User not found.");

            if (user.PasswordResetOtpExpiry == null || user.PasswordResetOtpExpiry < DateTime.UtcNow)
                return (false, "OTP expired.");

            if (user.PasswordResetOtp != dto.Otp)
                return (false, "Invalid OTP.");

            // Update new password
            user.userPassword = _passwordHasher.HashPassword(user, dto.NewPassword);

            // Clear OTP
            user.PasswordResetOtp = null;
            user.PasswordResetOtpExpiry = null;

            _db.users.Update(user);
            await _db.SaveChangesAsync();

            // Send confirmation email
            var subject = "Your Password Has Been Successfully Reset - Skedulo";
            var message = $"Hello {user.userName},<br/><br/>" +
                          $"Your password has been successfully reset.<br/><br/>" +
                          $"If you did NOT request this password reset, please contact support immediately.<br/><br/>" +
                          $"Regards,<br/>" +
                          $"Skedulo Team";

            _emailHelper.Send(user.userEmail, subject, message);

            return (true, "Password reset successful.");
        }
        public List<User> GetAll()
        {
            return _db.users.ToList();
        }

        
    }
}

 