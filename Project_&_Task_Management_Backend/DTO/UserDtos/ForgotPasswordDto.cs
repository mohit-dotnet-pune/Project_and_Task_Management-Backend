using System.ComponentModel.DataAnnotations;

namespace Project___Task_Management_Backend.DTO.UserDtos

{

    public class ForgotPasswordDto

    {

        [Required, EmailAddress]

        public string Email { get; set; }

    }

}



