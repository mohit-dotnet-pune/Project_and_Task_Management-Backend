using global::Project___Task_Management_Backend.DTO.ProjectDtos;
using global::Project___Task_Management_Backend.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Project___Task_Management_Backend.Data;
using Project___Task_Management_Backend.DTO;
using Project___Task_Management_Backend.DTO.ActivityDtos;
using Project___Task_Management_Backend.DTO.CloudinaryDtos;
using Project___Task_Management_Backend.Models;
using Project___Task_Management_Backend.Models;
using Project___Task_Management_Backend.Services;

namespace Project___Task_Management_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize(Roles = "Manager")]
    public class ProjectController : ControllerBase
    {
        private readonly IProjectService _service;
        private readonly ActivityService _activityService;
        private readonly AppDbContext _appDbContext;

        public ProjectController(IProjectService service, ActivityService activityService, AppDbContext appDbContext    )
        {
            _service = service;
            _activityService = activityService;
            _appDbContext = appDbContext;
        }
        //fetch user Id from middleware
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
        // Create Project
        // ------------------------------------------------------
        [HttpPost]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> CreateProject([FromForm] CreateProjectDto dto)

        {
            //uploading file to cloudinary



            //creating project
            var result = await _service.CreateProjectAsync(dto);

            if (!result.IsSuccess)
                return BadRequest(new { success = false, message = result.Message });

            var userId = GetUserId();

            

            await _activityService.LogAsync(new CreateActivityDto
            {
                userId = userId,
                projectId = result.Data.projectId,
                activityDescription = "Project created",
                activityEntityType = EntityType.Project,
                activityEntityId = result.Data.projectId
            });

            return Ok(new
            {
                success = true,
                message = result.Message,
                project = result.Data
            });
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetProject(int id)
        {
            var project = await _service.GetProject(id);
            if (project == null) return NotFound();
            return Ok(project);
        }

        // ------------------------------------------------------
        // Get All Projects
        // ------------------------------------------------------
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _service.GetAllProjects());
        }

        // ------------------------------------------------------
        // Update Project
        // ------------------------------------------------------
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProject(int id, [FromForm] UpdateProjectDto dto)

        {
            var userId = GetUserId();

            var updated = await _service.UpdateProject(id, dto);
            if (updated == null) return NotFound();

            await _activityService.LogAsync(new CreateActivityDto
            {
                userId = userId,
                projectId = id,
                activityDescription = "Project updated",
                activityEntityType = EntityType.Project,
                activityEntityId = id
            });

            return Ok(updated);
        }


        // ------------------------------------------------------
        // Delete Project
        // ------------------------------------------------------
        [HttpDelete("{id}")]
        public async Task<ActionResult<ResponseDto>> DeleteProject(int id)
        {

            var userId = GetUserId();
            await _activityService.LogAsync(new CreateActivityDto
            {
                userId = userId,
                projectId = id,
                activityDescription = "Project deleted",
                activityEntityType = EntityType.Project,
                activityEntityId = id
            });

            var deleted = await _service.DeleteProject(id);
            if (!deleted)
                return NotFound(new ResponseDto { IsSuccess = false, Message = "Project deletion failed" });

            

            return Ok(new ResponseDto { IsSuccess = true, Message = "Project deleted successfully" });
        }

        // ------------------------------------------------------
        // Attach File
        // ------------------------------------------------------
        [HttpPut("{projectId}/attach-file/{fileId}")]
        public async Task<IActionResult> AttachFileToProject(int projectId, int fileId)
        {
            var userId = GetUserId();

            var result = await _service.AttachFileToProjectAsync(projectId, fileId);

            if (!result.IsSuccess)
                return BadRequest(result);

            await _activityService.LogAsync(new CreateActivityDto
            {
                userId = userId,
                projectId = projectId,
                activityDescription = $"File attached: {fileId}",
                activityEntityType = EntityType.File,
                activityEntityId = fileId
            });

            return Ok(result);
        }


        // ------------------------------------------------------
        // Detach File
        // ------------------------------------------------------
        [HttpPut("{projectId}/detach-file/{fileId}")]
        public async Task<ActionResult> DettachFileToProject(int projectId, int fileId)
        {
            var userId = GetUserId();

            var result = await _service.DettachFileToProjectAsync(projectId, fileId);
            
            if (!result.IsSuccess)
                return BadRequest(result);

            await _activityService.LogAsync(new CreateActivityDto
            {
                userId = userId,
                projectId = projectId,
                activityDescription = $"File detached: {fileId}",
                activityEntityType = EntityType.File,
                activityEntityId = fileId
            });

            return Ok(result);
        }


        // ------------------------------------------------------
        // Add User To Project
        // ------------------------------------------------------
        [HttpPost("addUserToProject")]
        public async Task<ActionResult<ResponseDto>> AddUser(int userId, int projectId)
        {
            var actingUserId = GetUserId();  // one who performed action

            var result = await _service.AddUserToProjectAsync(userId, projectId);

            if (!result.IsSuccess)
                return BadRequest(result);

            await _activityService.LogAsync(new CreateActivityDto
            {
                userId = actingUserId,
                projectId = projectId,
                activityDescription = $"User {userId} added to project",
                activityEntityType = EntityType.Project,
                activityEntityId = projectId
            });

            return Ok(result);
        }


        // ------------------------------------------------------
        // Remove User From Project
        // ------------------------------------------------------
        [HttpDelete("removeUserFromProject")]
        public async Task<ActionResult<ResponseDto>> RemoveUser(int userId, int projectId)
        {
            var actingUserId = GetUserId();

            var result = await _service.RemoveUserFromProjectAsync(userId, projectId);

            if (!result.IsSuccess)
                return BadRequest(result);

            await _activityService.LogAsync(new CreateActivityDto
            {
                userId = actingUserId,
                projectId = projectId,
                activityDescription = $"User {userId} removed from project",
                activityEntityType = EntityType.Project,
                activityEntityId = projectId
            });

            return Ok(result);
        }

        // project/userId/GetAllProjects

        [HttpGet("project/{userId}/GetAllProjects")]
        public IActionResult GetAllProjectByUserId(int userId)
        {
            var projects = _appDbContext.projects
                .Where(p => p.userProjects.Any(up => up.userId == userId))
                .Include(p => p.tasks)
                .ToList();

            return Ok(projects);
        }

        [HttpGet("project/{projectId}/GetAllUsers")]
        public IActionResult GetAllUsersByProjectId(int projectId)
        {
            var users = _appDbContext.userProjects
                .Where(up => up.projectId == projectId)
                .Include(up => up.user)
                .Select(up => up.user)
                .ToList();

            return Ok(users);
        }

    }
}
