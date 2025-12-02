using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Project___Task_Management_Backend.DTO.ActivityDtos;
using Project___Task_Management_Backend.Models;
using Project___Task_Management_Backend.Services;
using Project___Task_Management_Backend.Models;

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

            // projectId must always be included
            if (dto.projectId == null)
                return BadRequest(new { message = "projectId is required" });

            await _activityService.LogAsync(dto);

            return Ok(new { message = "Activity logged successfully" });
        }

        // --------------------------------------------------
        // 2. Get Activities by Entity Type & ProjectId
        // --------------------------------------------------
        //[HttpGet("entity")]
        //public async Task<IActionResult> GetByEntity(
        //    [FromQuery] EntityType type,
        //    [FromQuery] int projectId)
        //{
        //    var activities = await _activityService.GetActivityByProjectId(type, projectId);
        //    return Ok(activities);
        //}

        // --------------------------------------------------
        // 3. Get All Activities of a User
        // --------------------------------------------------
        [HttpGet("user/{userId}")]
        public async Task<IActionResult> GetByUser(int userId)
        {
            var activities = await _activityService.GetUserActivity(userId);
            return Ok(activities);
        }

        // --------------------------------------------------
        // 4. Get ALL Activities (Admin view)
        // --------------------------------------------------
        [HttpGet("all")]
        public async Task<IActionResult> GetAll()
        {
            var activities = await _activityService.GetAllActivities();
            return Ok(activities);
        }

        [HttpGet("project/{projectId}/activities")]
        public async Task<IActionResult> GetActivitiesByProject(int projectId)
        {
            var activities = await _activityService.GetActivitiesByProjectId(projectId);
            return Ok(activities);
        }
    }
}
