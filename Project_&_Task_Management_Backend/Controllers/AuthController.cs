using Microsoft.AspNetCore.Mvc;
using Project___Task_Management_Backend.DTO.UserDtos;
using Project___Task_Management_Backend.Models;
using Project___Task_Management_Backend.Interfaces;

namespace Project___Task_Management_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _svc;

        public AuthController(IAuthService svc)
        {
            _svc = svc;
        }

        // 1. Register
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var (success, message) = await _svc.RegisterAsync(dto);

            if (!success) return BadRequest(new { message });

            return Ok(new { message });
        }

       
        [HttpPost("get-otp")]
        public async Task<IActionResult> GetOtp([FromBody] GetOtpDto dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var (success, message) = await _svc.GetOtpAsync(dto);

            if (!success) return BadRequest(new { message });

            return Ok(new { message });
        }



        // 3. Verify OTP & Final Registration
        [HttpPost("verify-email")]
        public async Task<IActionResult> VerifyEmail([FromBody] VerifyEmailOtpDto dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var (success, message) = await _svc.VerifyEmailOtpAsync(dto);

            if (!success) return BadRequest(new { message });

            return Ok(new { message });
        }

        // 4. Login
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var (success, token, message) = await _svc.LoginAsync(dto);

            if (!success) return Unauthorized(new { message });

            return Ok(new { token, message });
        }

        // 5. Forgot Password
        [HttpPost("forgot-password")]
        public async Task<IActionResult> ForgotPassword([FromBody] ForgotPasswordDto dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var (success, message) = await _svc.ForgotPasswordAsync(dto);

            if (!success) return BadRequest(new { message });

            return Ok(new { message });
        }

        // 6. Reset Password
        [HttpPost("reset-password")]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordDto dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var (success, message) = await _svc.ResetPasswordAsync(dto);

            if (!success) return BadRequest(new { message });

            return Ok(new { message });
        }

        [HttpGet("GetAllUsers")]
        public async Task<IActionResult> GetAllUsers()
        {
            List<User> list = _svc.GetAll();
            return await Task.FromResult(Ok(list));
        }
    }
}
