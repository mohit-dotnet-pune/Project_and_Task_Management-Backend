using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Project___Task_Management_Backend.DTO;
using Project___Task_Management_Backend.DTO.ProjectTaskDtos;
using Project___Task_Management_Backend.Interfaces;
using Project___Task_Management_Backend.Models;
using System.Threading.Tasks;

namespace Project___Task_Management_Backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    //[Authorize]
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
        public async Task<ActionResult<ResponseDto>> Delete(int id)
        {
            var result = await _service.DeleteTaskAsync(id);
            ResponseDto res = new ResponseDto { IsSuccess = result.IsSuccess, Message = result.Message };

            if (!result.IsSuccess) return NotFound(res);

            return Ok(res);
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
        public async Task<ActionResult<ResponseDto>> AttachUser(int taskId, int userId)
        {
            
                var result = await _service.AttachUserAsync(taskId, userId);
                ResponseDto res = new ResponseDto { IsSuccess = result.IsSuccess, Message = result.Message };

                if (!result.IsSuccess) return BadRequest(res);

                return Ok(res);
            
        }

        [HttpPost("{taskId}/detach-user")]
        public async Task<ActionResult<ResponseDto>> DetachUser(int taskId)
        {
        var result =  await _service.DetachUserAsync(taskId);
        ResponseDto res = new ResponseDto { IsSuccess = result.IsSuccess, Message = result.Message };

            if (!result.IsSuccess) return BadRequest(res);

            return Ok(res);
        }

        // Attach/Detach File
        [HttpPost("{taskId}/attach-file/{fileId}")]
        public async Task<ActionResult<ResponseDto>> AttachFile(int taskId, int fileId)
        {
            var result = await _service.AttachFileAsync(taskId, fileId);
            ResponseDto res = new ResponseDto { IsSuccess = result.IsSuccess, Message = result.Message };

            if (!result.IsSuccess) return BadRequest(res);

            return Ok(res);

        }

        [HttpPost("{taskId}/detach-file")]
        public async Task<ActionResult<ResponseDto>> DetachFile(int taskId)
        {
            var result = await _service.DetachFileAsync(taskId);
            ResponseDto res = new ResponseDto { IsSuccess = result.IsSuccess, Message = result.Message };

            if (!result.IsSuccess) return BadRequest(res);

            return Ok(res);
        }
          

        [HttpGet("getAllTask/{userId}")]
        public IActionResult GetAllTaskOfUser(int userId)
        {
            List<ProjectTask> tasks = _service.GetAllTasks(userId);
            return Ok(tasks);
        }


        [HttpPost("update-tasks-status")]
        public async Task<ActionResult<ResponseDto>> UpdateTasksStatus(UpdateTasksStatusDto dto)
        {
            var result = await _service.UpdateTasksStatusAsync(dto);

            ResponseDto res = new ResponseDto { IsSuccess = result.IsSuccess, Message = result.Message };

            if (!result.IsSuccess) return BadRequest(res);

            return Ok(res);
        }

        [HttpGet("project/{projectId}/tasks")]
        public async Task<IActionResult> GetTasksByProject(int projectId)
        {
            var tasks = await _service.GetTasksForProjectAsync(projectId);
            if (tasks == null || tasks.Count == 0)
                return NotFound($"No tasks found for project {projectId}"); 

            return Ok(tasks);
        }


    }

}
