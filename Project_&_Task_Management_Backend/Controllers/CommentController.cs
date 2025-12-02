using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Project___Task_Management_Backend.DTO.CommentDtos;
using Project___Task_Management_Backend.Interfaces;

namespace Project___Task_Management_Backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    //[Authorize]
    public class CommentController : ControllerBase
    {
        private readonly ICommentService _commentService;

        public CommentController(ICommentService commentService)
        {
            _commentService = commentService;
        }

        // CREATE
        [HttpPost]
        public async Task<IActionResult> Create(CreateCommentDto dto)
        {
            var result = await _commentService.CreateComment(dto);
            return Ok(result);
        }

        // GET BY ID
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var result = await _commentService.GetComment(id);
            return result == null ? NotFound() : Ok(result);
        }

        // GET ALL
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _commentService.GetComments());
        }

        // DELETE
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var success = await _commentService.DeleteComment(id);
            return success ? Ok("Deleted") : NotFound();
        }

        // UPDATE
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, UpdateCommentDto dto)
        {
            var result = await _commentService.UpdateComment(id, dto);
            return result == null ? NotFound() : Ok(result);
        }

        // GET BY TASK
        [HttpGet("task/{taskId}")]
        public async Task<IActionResult> ByTask(int taskId)
        {
            return Ok(await _commentService.GetCommentsByTask(taskId));
        }

        // GET BY USER
        [HttpGet("user/{userId}")]
        public async Task<IActionResult> ByUser(int userId)
        {
            return Ok(await _commentService.GetCommentsByUser(userId));
        }
    }

}
