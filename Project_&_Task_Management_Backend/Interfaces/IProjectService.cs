using global::Project___Task_Management_Backend.DTO.ProjectDtos;
using global::Project___Task_Management_Backend.Models;

namespace Project___Task_Management_Backend.Interfaces
{
    public interface IProjectService
    {
        Task<Project> CreateProject(CreateProjectDto dto);
        Task<Project?> GetProject(int id);
        Task<List<Project>> GetAllProjects();
        Task<Project?> UpdateProject(int id, UpdateProjectDto dto);
        Task<bool> DeleteProject(int id);

        // for file attach
        Task<(bool IsSuccess, string Message)> AttachFileToProjectAsync(int projectId, int fileId);

    }
}

