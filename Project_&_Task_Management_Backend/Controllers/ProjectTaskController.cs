using Microsoft.AspNetCore.Mvc;
using Project___Task_Management_Backend.DTO;
using Project___Task_Management_Backend.DTO.ActivityDtos;
using Project___Task_Management_Backend.DTO.ProjectTaskDtos;
using Project___Task_Management_Backend.Interfaces;
using Project___Task_Management_Backend.Models;
using Project___Task_Management_Backend.Services;

namespace Project___Task_Management_Backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProjectTaskController : ControllerBase
    {
        private readonly IProjectTaskService _service;
        private readonly ActivityService _activityService;

        public ProjectTaskController(IProjectTaskService service, ActivityService activityService)
        {
            _service = service;
            _activityService = activityService;
        }

        // -------------------------------
        // Fetch userId from JWT middleware
        // -------------------------------
        private int GetUserId()
        {
            if (HttpContext.Items["User"] is Dictionary<string, string> userClaims)
            {
                if (userClaims.TryGetValue("userId", out var userIdString) &&
                    int.TryParse(userIdString, out var userId))
                {
                    return userId;
                }
            }
            return 0;
        }

        // ----------------------------------------
        // Fetch projectId from taskId (local method)
        // ----------------------------------------
        private async Task<int?> GetProjectIdFromTask(int taskId)
        {
            var task = await _service.GetTaskByIdAsync(taskId);
            return task?.projectId;
        }

        // ----------------------
        // Create Task
        // ----------------------
        [HttpPost]
        public async Task<IActionResult> Create([FromForm]CreateTaskDto dto)
        {
            var task = await _service.CreateTaskAsync(dto);
            if (task == null) return BadRequest("Invalid project ID.");

            int? projectId = await GetProjectIdFromTask(dto.projectId);

            await _activityService.LogAsync(new CreateActivityDto
            {
                userId = GetUserId(),
                projectId = projectId,
                activityDescription = $"Task created: {task.taskId}",
                activityEntityType = EntityType.Task,
                activityEntityId = task.taskId
            });

            return Ok(task);
        }

        // ----------------------
        // Update Task
        // ----------------------
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromForm] UpdateTaskDto dto)
        {
            var task = await _service.UpdateTaskAsync(id, dto);
            if (task == null) return NotFound();

            int? projectId = await GetProjectIdFromTask(id);

            await _activityService.LogAsync(new CreateActivityDto
            {
                userId = GetUserId(),
                projectId = projectId ?? 0,
                activityDescription = $"Task updated: {id}",
                activityEntityType = EntityType.Task,
                activityEntityId = id
            });

            return Ok(task);
        }

        // ----------------------
        // Delete Task
        // ----------------------
        [HttpDelete("{id}")]
        public async Task<ActionResult<ResponseDto>> Delete(int id)
        {
            var result = await _service.DeleteTaskAsync(id);
            int? projectId = await GetProjectIdFromTask(id);
            ResponseDto res = new ResponseDto { IsSuccess = result.IsSuccess, Message = result.Message };

            if (!result.IsSuccess) return NotFound(res);


            await _activityService.LogAsync(new CreateActivityDto
            {
                userId = GetUserId(),
                projectId = projectId,
                activityDescription = $"Task deleted: {id}",
                activityEntityType = EntityType.Task,
                activityEntityId = id
            });

            return Ok(res);
        }

        // ----------------------
        // Get Task By Id
        // ----------------------
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var task = await _service.GetTaskByIdAsync(id);
            if (task == null) return NotFound();

            return Ok(task);
        }

        // ----------------------
        // Get All Tasks
        // ----------------------
        [HttpGet("getAll")]
        public async Task<IActionResult> GetAllTasks()
        {
            var tasks = _service.GetAll();
            if (tasks == null) return NotFound();
            return Ok(tasks);
        }

        // --------------------------
        // Attach User to Task
        // --------------------------
        [HttpPost("{taskId}/attach-user/{userId}")]
        public async Task<ActionResult<ResponseDto>> AttachUser(int taskId, int userId)
        {
            int? projectId = await GetProjectIdFromTask(taskId);
            var result = await _service.AttachUserAsync(taskId, userId);
            if (!result.IsSuccess) return BadRequest(result);

            await _activityService.LogAsync(new CreateActivityDto
            {
                userId = GetUserId(),
                projectId = projectId,
                activityDescription = $"User {userId} attached to task {taskId}",
                activityEntityType = EntityType.Task,
                activityEntityId = taskId
            });

            return Ok(result);
        }

        // --------------------------
        // Detach User from Task
        // --------------------------
        [HttpPost("{taskId}/detach-user")]
        public async Task<ActionResult<ResponseDto>> DetachUser(int taskId)
        {
            int? projectId = await GetProjectIdFromTask(taskId);
            var result = await _service.DetachUserAsync(taskId);
            if (!result.IsSuccess) return BadRequest(result);

            await _activityService.LogAsync(new CreateActivityDto
            {
                userId = GetUserId(),
                projectId = projectId,
                activityDescription = $"User detached from task {taskId}",
                activityEntityType = EntityType.Task,
                activityEntityId = taskId
            });

            return Ok(result);
        }

        // --------------------------
        // Attach File to Task
        // --------------------------
        [HttpPost("{taskId}/attach-file/{fileId}")]
        public async Task<ActionResult<ResponseDto>> AttachFile(int taskId, int fileId)
        {
            int? projectId = await GetProjectIdFromTask(taskId);
            var result = await _service.AttachFileAsync(taskId, fileId);
            if (!result.IsSuccess) return BadRequest(result);

            await _activityService.LogAsync(new CreateActivityDto
            {
                userId = GetUserId(),
                projectId = projectId,
                activityDescription = $"File {fileId} attached to task {taskId}",
                activityEntityType = EntityType.File,
                activityEntityId = fileId
            });

            return Ok(result);
        }

        // --------------------------
        // Detach File from Task
        // --------------------------
        [HttpPost("{taskId}/detach-file")]
        public async Task<ActionResult<ResponseDto>> DetachFile(int taskId)
        {
            int? projectId = await GetProjectIdFromTask(taskId);
            var result = await _service.DetachFileAsync(taskId);
            if (!result.IsSuccess) return BadRequest(result);

            await _activityService.LogAsync(new CreateActivityDto
            {
                userId = GetUserId(),
                projectId = projectId,
                activityDescription = $"File detached from task {taskId}",
                activityEntityType = EntityType.File,
                activityEntityId = taskId
            });

            return Ok(result);
        }

        // --------------------------
        // Get all tasks for a user
        // --------------------------
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
