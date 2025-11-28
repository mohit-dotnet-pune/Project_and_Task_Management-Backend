using System.ComponentModel.DataAnnotations;

namespace Project___Task_Management_Backend.DTO.UserDtos
{
    public class LoginDto
    {
        [Required, EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }
    }
}