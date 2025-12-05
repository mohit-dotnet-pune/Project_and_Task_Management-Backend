using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Project___Task_Management_Backend.Data;
using Project___Task_Management_Backend.Models;

namespace Project___Task_Management_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly AppDbContext _appDbContext;
        public UserController(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        // Fix 1: Make updateUserDto public and move it outside UserController for consistent accessibility
        public class UpdateUserDto
        {
            public string Email { get; set; }
            public string UserName { get; set; }
        }

        // Fix 2: Add parameter name to method signature
        [HttpPut]
        public async Task<IActionResult> UpdateUserProfile([FromBody] UpdateUserDto updateUserDto)
        {
            // Fetch userId from JWT middleware
            int userId = 0;
            if (HttpContext.Items["User"] is Dictionary<string, string> userClaims)
            {
                if (userClaims.TryGetValue("userId", out var userIdString) &&
                    int.TryParse(userIdString, out var parsedUserId))
                {
                    userId = parsedUserId;
                }
            }
            var user = await _appDbContext.users.FindAsync(userId);
            if (user == null)
            {
                return NotFound("User not found.");
            }
        
            user.userEmail = updateUserDto.Email;
            user.userName = updateUserDto.UserName;
            _appDbContext.users.Update(user);
            await _appDbContext.SaveChangesAsync();
            return Ok("Profile updated successfully.");
        }

        [HttpDelete("{userId}")]
        public async Task<IActionResult> DeleteUserProfile(int userId)
        {
            User user = await _appDbContext.users.FindAsync(userId);
            _appDbContext.users.Remove(user);
            await _appDbContext.SaveChangesAsync();
            return Ok("user deleted success");
        }
    }
}
