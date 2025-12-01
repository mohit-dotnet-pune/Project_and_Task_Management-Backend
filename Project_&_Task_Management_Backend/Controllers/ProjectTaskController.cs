using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Project___Task_Management_Backend.DTO.ProjectTaskDtos;
using Project___Task_Management_Backend.Interfaces;
using Project___Task_Management_Backend.Models;

namespace Project___Task_Management_Backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProjectTaskController : ControllerBase
    {
        private readonly IProjectTaskService _service;

        public ProjectTaskController(IProjectTaskService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateTaskDto dto)
        {
            var task = await _service.CreateTaskAsync(dto);
            if (task == null) return BadRequest("Invalid project ID.");

            return Ok(task);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, UpdateTaskDto dto)
        {
            var task = await _service.UpdateTaskAsync(id, dto);
            if (task == null) return NotFound();

            return Ok(task);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _service.DeleteTaskAsync(id);
            if(!result.IsSuccess) return NotFound(result);

            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var task = await _service.GetTaskByIdAsync(id);
            if (task == null) return NotFound();

            return Ok(task);
        }

        [HttpGet("getAll")]
        public async Task<IActionResult> GetAllTasks()
        {
            var task =  _service.GetAll();
            if (task == null) return NotFound();

            return Ok(task);
        }

        // Attach/Detach User
        [HttpPost("{taskId}/attach-user/{userId}")]
        public async Task<IActionResult> AttachUser(int taskId, int userId)
            => Ok(await _service.AttachUserAsync(taskId, userId));

        [HttpPost("{taskId}/detach-user")]
        public async Task<IActionResult> DetachUser(int taskId)
            => Ok(await _service.DetachUserAsync(taskId));

        // Attach/Detach File
        [HttpPost("{taskId}/attach-file/{fileId}")]
        public async Task<IActionResult> AttachFile(int taskId, int fileId)
            => Ok(await _service.AttachFileAsync(taskId, fileId));

        [HttpPost("{taskId}/detach-file")]
        public async Task<IActionResult> DetachFile(int taskId)
            => Ok(await _service.DetachFileAsync(taskId));

        [HttpGet("getAllTask/{userId}")]
        public IActionResult GetAllTaskOfUser(int userId)
        {
            List<ProjectTask> tasks = _service.GetAllTasks(userId);
            return Ok(tasks);
        }


        [HttpPost("update-tasks-status")]
        public async Task<IActionResult> UpdateTasksStatus(UpdateTasksStatusDto dto)
        {
            var result = await _service.UpdateTasksStatusAsync(dto);

            if (!result.IsSuccess)
                return BadRequest(result);

            return Ok(result);
        }

    }

}
