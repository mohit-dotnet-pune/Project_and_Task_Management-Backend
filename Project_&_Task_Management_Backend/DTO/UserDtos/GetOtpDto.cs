namespace Project___Task_Management_Backend.DTO.UserDtos
{
    public class GetOtpDto
    {
        public string Email { get; set; }      // Client provides email
        public bool ForceSend { get; set; } = false;  // Optional: resend OTP
    }
}
