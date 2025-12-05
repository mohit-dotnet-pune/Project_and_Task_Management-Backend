using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;

namespace Project___Task_Management_Backend.Models
{
    public enum Role
    {
        Employee,
        Manager
    }

    public class User
    {
        [Key]
        public int userId {  get; set; }

        [Required]
        public string userName { get; set; }
        [Required]
        public string userEmail { get; set; }
        [Required]
        public string userPassword { get; set; }
        [Required]
        public Role userRole { get; set; }

        public bool EmailConfirmed { get; set; } = false;

        // OTP for email verification (NOW NULLABLE)

        public string? EmailOtp { get; set; }

        public DateTime? EmailOtpExpiry { get; set; }

        // OTP for password reset (NOW NULLABLE)

        public string? PasswordResetOtp { get; set; }

        public DateTime? PasswordResetOtpExpiry { get; set; }

        // JWT Token (Stored in table)

        public string? JwtToken { get; set; }

        // JWT Expiry Time

        public DateTime? JwtTokenExpiry { get; set; }

        // Recommended: Refresh Token

        public string? RefreshToken { get; set; }

        public DateTime? RefreshTokenExpiry { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public ICollection <UserProject>? userProjects { get; set; }
        public ICollection<ProjectTask>? tasks { get; set; }
        public ICollection<Activity>? activities { get; set; }
        public ICollection<Comment>? comments { get; set; }


    }
}
