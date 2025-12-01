using global::Project___Task_Management_Backend.DTO.ProjectDtos;
using global::Project___Task_Management_Backend.Interfaces;
using global::Project___Task_Management_Backend.Models;

namespace Project___Task_Management_Backend.Services
{
    public class ProjectService : IProjectService
    {
        private readonly IProjectRepository _repo;

        public ProjectService(IProjectRepository repo)
        {
            _repo = repo;
        }

        public async Task<Project> CreateProject(CreateProjectDto dto)
        {
            var project = new Project
            {
                projectName = dto.projectName,
                projectDescription = dto.projectDescription,
                projectStartDate = dto.projectStartDate,
                projectEndDate = dto.projectEndDate,
                projectCreatedAt = DateTime.UtcNow
            };

            return await _repo.CreateProject(project);
        }

        public Task<Project?> GetProject(int id)
        {
            return _repo.GetProjectById(id);
        }

        public Task<List<Project>> GetAllProjects()
        {
            return _repo.GetAllProjects();
        }

        public async Task<Project?> UpdateProject(int id, UpdateProjectDto dto)
        {
            var project = new Project
            {
                projectId = id,
                projectName = dto.projectName,
                projectDescription = dto.projectDescription,
                projectStartDate = dto.projectStartDate,
                projectEndDate = dto.projectEndDate
            };

            return await _repo.UpdateProject(project);
        }

        public Task<bool> DeleteProject(int id)
        {
            return _repo.DeleteProject(id);
        }

        public async Task<(bool IsSuccess, string Message)> AttachFileToProjectAsync(int projectId, int fileId)
        {
            var project = await _repo.GetProjectById(projectId);
            if (project == null)
                return (false, "Project not found");

            var file = await _repo.GetFileByIdAsync(fileId);
            if (file == null)
                return (false, "File not found");

            // Link file
            project.fileId = fileId;
            project.file = file;

            bool result = await _repo.SaveChangesAsync();
            if (!result)
                return (false, "Failed to attach file to project");

            return (true, "File attached to project successfully");
        }

        public async Task<(bool IsSuccess, string Message)> DettachFileToProjectAsync(int projectId, int fileId)
        {
            var project = await _repo.GetProjectById(projectId);
            if (project == null)
                return (false, "Project not found");

            var file = await _repo.GetFileByIdAsync(fileId);
            if (file == null)
                return (false, "File not found");

            // unLink file
            project.fileId = null;
            project.file = null;

            bool result = await _repo.SaveChangesAsync();
            if (!result)
                return (false, "Failed to dettach file to project");

            return (true, "File dettached to project successfully");
        }


        // userProject
        public async Task<(bool IsSuccess, string Message)> AddUserToProjectAsync(int userId, int projectId)
        {
            bool result = await _repo.AddUserToProject(userId, projectId);
            if (result) return (true, "user added to project successfully");
            else return (false, "user id or project id not correct");
        }

        public async Task<(bool IsSuccess, string Message)> RemoveUserFromProjectAsync(int userId, int projectId)
        {
            var ans = await _repo.RemoveMappingAsync(userId, projectId);
            if (ans == null) return (false, "user id or project id is not correct");
            return (true, "user removed from project successfully");
            
        }

        public async Task<List<User>> GetUsersByProjectAsync(int projectId)
        {
            return await _repo.GetUsersByProjectAsync(projectId);
        }

        public async Task<List<Project>> GetProjectsByUserAsync(int userId)
        {
            return await _repo.GetProjectsByUserAsync(userId);
        }

    }
}
