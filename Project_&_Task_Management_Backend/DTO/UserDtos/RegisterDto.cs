//using System.ComponentModel.DataAnnotations;

//namespace Project___Task_Management_Backend.DTO.UserDtos
//{

//    public class RegisterDto

//    {

//        [Required, StringLength(100, MinimumLength = 3)]

//        public string Username { get; set; }

//        [Required, EmailAddress]

//        public string Email { get; set; }

//        [Required, MinLength(6)]

//        public string Password { get; set; }

//        // optional: role selection

//        public string Role { get; set; } = "User";

//    }

//}



using System.ComponentModel.DataAnnotations;

namespace Project___Task_Management_Backend.DTO.UserDtos
{
    public class RegisterDto
    {
        [Required, StringLength(100, MinimumLength = 3)]
        public string Username { get; set; }

        [Required, EmailAddress]
        public string Email { get; set; }

        [Required, MinLength(6)]
        public string Password { get; set; }

        // Default role changed to Employee because enum has Employee & Manager only
        public string Role { get; set; } = "Employee";
    }
}



