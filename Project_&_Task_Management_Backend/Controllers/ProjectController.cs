using global::Project___Task_Management_Backend.DTO.ProjectDtos;
using global::Project___Task_Management_Backend.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Project___Task_Management_Backend.DTO;
using Project___Task_Management_Backend.Services;

namespace Project___Task_Management_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize(Roles = "Manager")]
    public class ProjectController : ControllerBase
    {
        private readonly IProjectService _service;

        public ProjectController(IProjectService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<IActionResult> CreateProject([FromBody] CreateProjectDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _service.CreateProjectAsync(dto);

            if (!result.IsSuccess)
                return BadRequest(new { success = false, message = result.Message });

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
            return Ok(project); // contains tasks also
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var list = await _service.GetAllProjects();
            return Ok(list);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProject(int id, UpdateProjectDto dto)
        {
            var updated = await _service.UpdateProject(id, dto);
            if (updated == null) return NotFound();
            return Ok(updated);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<ResponseDto>> DeleteProject(int id)
        {
            var deleted = await _service.DeleteProject(id);
            if (!deleted) return NotFound(new ResponseDto { IsSuccess = false, Message = "Project deletion failed" });
            return Ok(new ResponseDto { IsSuccess = true, Message = "Project deleted successfully" });
        }

        [HttpPut("{projectId}/attach-file/{fileId}")]
        public async Task<IActionResult> AttachFileToProject(int projectId, int fileId)
        {
            var result = await _service.AttachFileToProjectAsync(projectId, fileId);

            if (!result.IsSuccess)
            {
                return BadRequest(new
                {
                    success = false,
                    message = result.Message
                });
            }

            return Ok(new
            {
                success = true,
                message = result.Message,
                projectId = projectId,
                fileId = fileId
            });
        }

        [HttpPut("{projectId}/detach-file/{fileId}")]
        public async Task<IActionResult> DettachFileToProject(int projectId, int fileId)
        {
            var result = await _service.DettachFileToProjectAsync(projectId, fileId);

            if (!result.IsSuccess)
            {
                return BadRequest(new
                {
                    success = false,
                    message = result.Message
                });
            }

            return Ok(new
            {
                success = true,
                message = result.Message,
                projectId = projectId,
                fileId = fileId
            });
        }

        [HttpPost("addUserToProject")]
        public async Task<ActionResult<ResponseDto>> AddUser(int userId, int projectId)
        {
            
            var result = await _service.AddUserToProjectAsync(userId, projectId);
            ResponseDto res = new ResponseDto { IsSuccess = result.IsSuccess, Message = result.Message };

            if (!result.IsSuccess) return BadRequest(res);

            return Ok(res);
        }

        [HttpDelete("removeUserFromProject")]
        public async Task<ActionResult<ResponseDto>> RemoveUser(int userId, int projectId)
        {
            var result = await _service.RemoveUserFromProjectAsync(userId, projectId);
            ResponseDto res = new ResponseDto { IsSuccess = result.IsSuccess, Message = result.Message };

            if (!result.IsSuccess) return BadRequest(res);

            return Ok(res);
        }

        [HttpGet("{projectId}/GetAllusers")]
        public async Task<IActionResult> Users(int projectId)
        {
            return Ok(await _service.GetUsersByProjectAsync(projectId));
        }

        [HttpGet("{userId}/GetAllprojects")]
        public async Task<IActionResult> Projects(int userId)
        {
            return Ok(await _service.GetProjectsByUserAsync(userId));
        }
    }
}
