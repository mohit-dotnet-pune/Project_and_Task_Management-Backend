using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Project___Task_Management_Backend.DTO.ActivityDtos;
using Project___Task_Management_Backend.Models;
using Project___Task_Management_Backend.Services;

namespace Project___Task_Management_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ActivityController : ControllerBase
    {
        private readonly ActivityService _activityService;

        public ActivityController(ActivityService activityService)
        {
            _activityService = activityService;
        }

        // ------------------------------------------
        // 1. Create Activity
        // ------------------------------------------
        [HttpPost("create")]
        public async Task<IActionResult> CreateActivity([FromBody] CreateActivityDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await _activityService.LogAsync(dto);

            return Ok(new { message = "Activity logged successfully" });
        }

        // ------------------------------------------
        // 2. Get Activities for a Specific Entity
        // ------------------------------------------
        [HttpGet("entity")]
        public async Task<IActionResult> GetByEntity(
            [FromQuery] EntityType type,
            [FromQuery] int entityId)
        {
            var activities = await _activityService.GetEntityActivity(type, entityId);
            return Ok(activities);
        }

        // ------------------------------------------
        // 3. Get Activities for a User
        // ------------------------------------------
        [HttpGet("user/{userId}")]
        public async Task<IActionResult> GetByUser(int userId)
        {
            var activities = await _activityService.GetUserActivity(userId);
            return Ok(activities);
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetAll()
        {
            var activities = await _activityService.GetAllActivities();
            return Ok(activities);
        }
    }
}
