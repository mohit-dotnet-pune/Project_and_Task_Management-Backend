using global::Project___Task_Management_Backend.DTO.ProjectDtos;
using global::Project___Task_Management_Backend.Models;

namespace Project___Task_Management_Backend.Interfaces
{
    public interface IProjectService
    {
        Task<(bool IsSuccess, string Message, Project? Data)> CreateProjectAsync(CreateProjectDto dto);
        Task<Project?> GetProject(int id);
        Task<List<Project>> GetAllProjects();
        Task<Project?> UpdateProject(int id, UpdateProjectDto dto);
        Task<bool> DeleteProject(int id);

        // for file attach
        Task<(bool IsSuccess, string Message)> AttachFileToProjectAsync(int projectId, int fileId);
        Task<(bool IsSuccess, string Message)> DettachFileToProjectAsync(int projectId, int fileId);

        // for userProject
        Task<(bool IsSuccess, string Message)> AddUserToProjectAsync(int userId, int projectId);
        Task<(bool IsSuccess, string Message)> RemoveUserFromProjectAsync(int userId, int projectId);
        Task<List<User>> GetUsersByProjectAsync(int projectId);
        Task<List<Project>> GetProjectsByUserAsync(int userId);
    }
}

