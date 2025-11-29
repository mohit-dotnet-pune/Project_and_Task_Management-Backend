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

        // ----------------------------------------------------------------------

        // REGISTER

        // ----------------------------------------------------------------------

        public async Task<(bool Success, string Message)> RegisterAsync(RegisterDto dto)
        {
            var exists = await _db.users.AnyAsync(u => u.userEmail == dto.Email || u.userName == dto.Username);
            if (exists)
                return (false, "Email or Username already in use.");

            // Only allow roles from enum
            if (string.IsNullOrWhiteSpace(dto.Role) || !Enum.TryParse<Role>(dto.Role, true, out Role selectedRole))
            {
                return (false, "Invalid role. Allowed roles are Employee or Manager.");
            }

            var user = new User
            {
                userEmail = dto.Email,
                userName = dto.Username,
                userRole = selectedRole
            };

            user.userPassword = _passwordHasher.HashPassword(user, dto.Password);

            // Generate OTP
            var otp = GenerateOtp(6);
            user.EmailOtp = otp;
            user.EmailOtpExpiry = DateTime.UtcNow.AddMinutes(15);

            await _db.users.AddAsync(user);
            await _db.SaveChangesAsync();

            // Email message
            var subject = "Welcome to Project & Task Management - Verify your email";
            var message = $"Hello {user.userName},<br/><br/>" +
                          $"Welcome to Project & Task Management!<br/>" +
                          $"Your verification OTP is <b>{otp}</b>.<br/><br/>" +
                          "Please use this OTP to verify your email within 15 minutes.";

            var sent = _emailHelper.Send(user.userEmail, subject, message);
            if (!sent)
                return (false, "User registered but failed to send OTP email.");

            return (true, "User registered. Please verify your email.");
        }



        // ----------------------------------------------------------------------

        // VERIFY EMAIL OTP

        // ----------------------------------------------------------------------

        public async Task<(bool Success, string Message)> VerifyEmailOtpAsync(VerifyEmailOtpDto dto)
        {
            var user = await _db.users.FirstOrDefaultAsync(u => u.userEmail == dto.Email);

            if (user == null)
                return (false, "User not found.");

            if (user.EmailConfirmed)
                return (false, "Email already verified.");

            if (user.EmailOtpExpiry == null || user.EmailOtpExpiry < DateTime.UtcNow)
                return (false, "OTP expired.");

            if (user.EmailOtp != dto.Otp)
                return (false, "Invalid OTP.");

            // Mark user as verified
            user.EmailConfirmed = true;
            user.EmailOtp = null;
            user.EmailOtpExpiry = null;

            _db.users.Update(user);
            await _db.SaveChangesAsync();

            // Send verification success email
            var subject = "Your Email is Successfully Verified - Project & Task Management";
            var message = $"Hello {user.userName},<br/><br/>" +
                          $"🎉 Your email has been successfully verified!<br/><br/>" +
                          $"You can now log in and start using the Project & Task Management system.<br/><br/>" +
                          $"Thank you for joining us!<br/><br/>" +
                          $"Regards,<br/>Project & Task Management Team";

            _emailHelper.Send(user.userEmail, subject, message);

            return (true, "Email verified successfully.");
        }

        // ----------------------------------------------------------------------

        // LOGIN (JWT + SAVE TOKEN IN DB)

        // ----------------------------------------------------------------------

        public async Task<(bool Success, string Token, string Message)> LoginAsync(LoginDto dto)
        {
            // 1️⃣ Check if user exists
            var user = await _db.users.FirstOrDefaultAsync(u => u.userEmail == dto.Email);

            if (user == null)
                return (false, null, "User not registered.");

            // 2️⃣ Check email verification
            if (!user.EmailConfirmed)
                return (false, null, "Email not verified.");

            // 3️⃣ Validate password
            var verify = _passwordHasher.VerifyHashedPassword(user, user.userPassword, dto.Password);

            if (verify == PasswordVerificationResult.Failed)
                return (false, null, "Invalid credentials.");

            // 4️⃣ Create JWT
            var token = GenerateJwtToken(user);

            // 5️⃣ Save JWT in database
            user.JwtToken = token;
            user.JwtTokenExpiry = DateTime.UtcNow.AddMinutes(_config.GetValue<int>("Jwt:ExpiryMinutes"));

            // 6️⃣ Generate Refresh Token
            user.RefreshToken = Guid.NewGuid().ToString("N");
            user.RefreshTokenExpiry = DateTime.UtcNow.AddDays(7);

            _db.users.Update(user);
            await _db.SaveChangesAsync();

            // 7️⃣ Successful login
            return (true, token, "Login successful.");
        }


        private string GenerateJwtToken(User user)

        {

            var key = _config["Jwt:Key"];

            var issuer = _config["Jwt:Issuer"];

            var audience = _config["Jwt:Audience"];

            var minutes = _config.GetValue<int>("Jwt:ExpiryMinutes");

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));

            var creds = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]

            {

                new Claim("username", user.userName),

                new Claim("email", user.userEmail),

                new Claim(ClaimTypes.Role, user.userRole.ToString()),

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
            var subject = "Project & Task Management - Reset Password OTP";
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
            var subject = "Your Password Has Been Successfully Reset - Project & Task Management";
            var message = $"Hello {user.userName},<br/><br/>" +
                          $"Your password has been successfully reset.<br/><br/>" +
                          $"If you did NOT request this password reset, please contact support immediately.<br/><br/>" +
                          $"Regards,<br/>" +
                          $"Project & Task Management Team";

            _emailHelper.Send(user.userEmail, subject, message);

            return (true, "Password reset successful.");
        }

    }

}

 