using Project___Task_Management_Backend.DTO.UserDtos;
using Project___Task_Management_Backend.Models;

namespace Project___Task_Management_Backend.Interfaces
{
    public interface IAuthService
    {
        Task<(bool Success, string Message)> RegisterAsync(RegisterDto dto);

        // UPDATED HERE ↓ (now uses GetOtpDto)
        Task<(bool Success, string Message)> GetOtpAsync(GetOtpDto dto);


        Task<(bool Success, string Message)> VerifyEmailOtpAsync(VerifyEmailOtpDto dto);

        Task<(bool Success, UserResponseDto User, string Message)> LoginAsync(LoginDto dto);

        Task<(bool Success, string Message)> ForgotPasswordAsync(ForgotPasswordDto dto);

        Task<(bool Success, string Message)> ResetPasswordAsync(ResetPasswordDto dto);
        List<User> GetAll();

    }
}
