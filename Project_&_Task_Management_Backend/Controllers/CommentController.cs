using Microsoft.AspNetCore.Mvc;
using Project___Task_Management_Backend.DTO.CommentDtos;
using Project___Task_Management_Backend.Interfaces;
using Project___Task_Management_Backend.DTO.ActivityDtos;
using Project___Task_Management_Backend.Models;

namespace Project___Task_Management_Backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CommentController : ControllerBase
    {
        private readonly ICommentService _commentService;
        private readonly IActivityService _activityService;

        public CommentController(ICommentService commentService,
                                 IActivityService activityService)
        {
            _commentService = commentService;
            _activityService = activityService;
        }

        // Fetch userId from JWT middleware
        private int GetUserId()
        {
            if (HttpContext.Items["User"] is Dictionary<string, string> userClaims &&
                userClaims.ContainsKey("userId"))
            {
                return int.Parse(userClaims["userId"]);
            }

            throw new Exception("User ID not found in token");
        }

        // ------------------------------------------------------
        // CREATE COMMENT
        // ------------------------------------------------------
        [HttpPost]
        public async Task<IActionResult> Create([FromForm] CreateCommentDto dto)
        {
            var userId = GetUserId();

            var result = await _commentService.CreateComment(dto);
            if (result == null)
                return BadRequest("Comment creation failed");

            var projectId = await _commentService.GetProjectIdByTask(dto.taskId);

            await _activityService.LogAsync(new CreateActivityDto
            {
                userId = userId,
                projectId = projectId ?? 0,
                activityDescription = "Comment created",
                activityEntityType = EntityType.Comment,
                activityEntityId = result.commentId
            });

            return Ok(result);
        }

        // ------------------------------------------------------
        // GET BY ID
        // ------------------------------------------------------
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var result = await _commentService.GetComment(id);
            return result == null ? NotFound() : Ok(result);
        }

        // ------------------------------------------------------
        // GET ALL
        // ------------------------------------------------------
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _commentService.GetComments());
        }

        // ------------------------------------------------------
        // DELETE COMMENT
        // ------------------------------------------------------
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var userId = GetUserId();

            var existing = await _commentService.GetComment(id);
            if (existing == null)
                return NotFound();

            bool success = await _commentService.DeleteComment(id);
            if (!success)
                return BadRequest("Failed to delete comment");

            var projectId = await _commentService.GetProjectIdByTask(existing.taskId);

            await _activityService.LogAsync(new CreateActivityDto
            {
                userId = userId,
                projectId = projectId ?? 0,
                activityDescription = "Comment deleted",
                activityEntityType = EntityType.Comment,
                activityEntityId = id
            });

            return Ok("Comment deleted");
        }

        // ------------------------------------------------------
        // UPDATE COMMENT
        // ------------------------------------------------------
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromForm] UpdateCommentDto dto)
        {
            var userId = GetUserId();

            var updated = await _commentService.UpdateComment(id, dto);
            if (updated == null)
                return NotFound();

            var projectId = await _commentService.GetProjectIdByTask(updated.taskId);

            await _activityService.LogAsync(new CreateActivityDto
            {
                userId = userId,
                projectId = projectId ?? 0,
                activityDescription = "Comment updated",
                activityEntityType = EntityType.Comment,
                activityEntityId = id
            });

            return Ok(updated);
        }

        // ------------------------------------------------------
        // GET COMMENTS BY TASK
        // ------------------------------------------------------
        [HttpGet("task/{taskId}")]
        public async Task<IActionResult> ByTask(int taskId)
        {
            return Ok(await _commentService.GetCommentsByTask(taskId));
        }

        // ------------------------------------------------------
        // GET COMMENTS BY USER
        // ------------------------------------------------------
        [HttpGet("user/{userId}")]
        public async Task<IActionResult> ByUser(int userId)
        {
            return Ok(await _commentService.GetCommentsByUser(userId));
        }
    }
}
