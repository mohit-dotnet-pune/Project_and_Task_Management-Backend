using global::Project___Task_Management_Backend.DTO.ProjectDtos;
using global::Project___Task_Management_Backend.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Project___Task_Management_Backend.Services;

namespace Project___Task_Management_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
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
            var result = await _service.CreateProject(dto);
            return Ok(result);
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
        public async Task<IActionResult> DeleteProject(int id)
        {
            var deleted = await _service.DeleteProject(id);
            if (!deleted) return NotFound();
            return Ok(new { message = "Project deleted successfully" });
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
    }
}
