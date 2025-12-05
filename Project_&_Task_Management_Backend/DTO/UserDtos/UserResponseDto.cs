using Project___Task_Management_Backend.Models;

namespace Project___Task_Management_Backend.DTO.UserDtos
{
    public class UserResponseDto
    {
        public int userId { get; set; }
        public string userName { get; set; }
        public string userEmail { get; set; }
        public Role userRole { get; set; }
        public bool EmailConfirmed { get; set; }

        public string JwtToken { get; set; }
        public DateTime JwtTokenExpiry { get; set; }
        public string RefreshToken { get; set; }
        public DateTime RefreshTokenExpiry { get; set; }
    }
}
