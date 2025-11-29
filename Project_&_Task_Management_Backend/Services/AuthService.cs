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

            // FIXED: convert string role to Enum

            Role selectedRole = Role.Employee;

            if (!string.IsNullOrWhiteSpace(dto.Role))

            {

                if (dto.Role.Trim().ToLower() == "manager")

                    selectedRole = Role.Manager;

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

            var subject = "Verify your email - MyAuthApi";

            var message = $"Hello {user.userName},<br/><br/>Your verification OTP is <b>{otp}</b>.";

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

            if (user == null) return (false, "User not found.");

            if (user.EmailConfirmed) return (false, "Email already verified.");

            if (user.EmailOtpExpiry == null || user.EmailOtpExpiry < DateTime.UtcNow)

                return (false, "OTP expired.");

            if (user.EmailOtp != dto.Otp)

                return (false, "Invalid OTP.");

            user.EmailConfirmed = true;

            user.EmailOtp = null;

            user.EmailOtpExpiry = null;

            _db.users.Update(user);

            await _db.SaveChangesAsync();

            return (true, "Email verified successfully.");

        }

        // ----------------------------------------------------------------------

        // LOGIN (JWT + SAVE TOKEN IN DB)

        // ----------------------------------------------------------------------

        public async Task<(bool Success, string Token, string Message)> LoginAsync(LoginDto dto)

        {

            var user = await _db.users.FirstOrDefaultAsync(u => u.userEmail == dto.Email);

            if (user == null) return (false, null, "Invalid credentials.");

            if (!user.EmailConfirmed)

                return (false, null, "Email not verified.");

            var verify = _passwordHasher.VerifyHashedPassword(user, user.userPassword, dto.Password);

            if (verify == PasswordVerificationResult.Failed)

                return (false, null, "Invalid credentials.");

            // CREATE JWT

            var token = GenerateJwtToken(user);

            // SAVE JWT TO DATABASE

            user.JwtToken = token;

            user.JwtTokenExpiry = DateTime.UtcNow.AddMinutes(_config.GetValue<int>("Jwt:ExpiryMinutes"));

            // REFRESH TOKEN

            user.RefreshToken = Guid.NewGuid().ToString("N");

            user.RefreshTokenExpiry = DateTime.UtcNow.AddDays(7);

            _db.users.Update(user);

            await _db.SaveChangesAsync();

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

            var subject = "Reset Password OTP";

            var message = $"Your OTP is <b>{otp}</b>.";

            _emailHelper.Send(user.userEmail, subject, message);

            return (true, "OTP sent successfully.");

        }

        // ----------------------------------------------------------------------

        // RESET PASSWORD

        // ----------------------------------------------------------------------

        public async Task<(bool Success, string Message)> ResetPasswordAsync(ResetPasswordDto dto)

        {

            var user = await _db.users.FirstOrDefaultAsync(u => u.userEmail == dto.Email);

            if (user == null) return (false, "User not found.");

            if (user.PasswordResetOtpExpiry < DateTime.UtcNow)

                return (false, "OTP expired.");

            if (user.PasswordResetOtp != dto.Otp)

                return (false, "Invalid OTP.");

            user.userPassword = _passwordHasher.HashPassword(user, dto.NewPassword);

            user.PasswordResetOtp = null;

            user.PasswordResetOtpExpiry = null;

            _db.users.Update(user);

            await _db.SaveChangesAsync();

            return (true, "Password reset successful.");

        }
        public List<User> GetAll()
        {
            return _db.users.ToList();
        }

    }
}

 