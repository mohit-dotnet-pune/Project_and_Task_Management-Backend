using Project___Task_Management_Backend.DTO.UserDtos;

namespace Project___Task_Management_Backend.Interfaces
{

    public interface IAuthService

    {

        Task<(bool Success, string Message)> RegisterAsync(RegisterDto dto);

        Task<(bool Success, string Message)> VerifyEmailOtpAsync(VerifyEmailOtpDto dto);

        Task<(bool Success, string Token, string Message)> LoginAsync(LoginDto dto);

        Task<(bool Success, string Message)> ForgotPasswordAsync(ForgotPasswordDto dto);

        Task<(bool Success, string Message)> ResetPasswordAsync(ResetPasswordDto dto);

    }

}



