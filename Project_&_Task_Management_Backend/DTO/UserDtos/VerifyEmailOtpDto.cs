using System.ComponentModel.DataAnnotations;

namespace Project___Task_Management_Backend.DTO.UserDtos
{

    public class VerifyEmailOtpDto

    {

        [Required, EmailAddress]

        public string Email { get; set; }

        [Required, StringLength(10)]

        public string Otp { get; set; }

    }

}

 