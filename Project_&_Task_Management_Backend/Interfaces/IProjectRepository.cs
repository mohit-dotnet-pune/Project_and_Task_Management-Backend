using global::Project___Task_Management_Backend.Models;

namespace Project___Task_Management_Backend.Interfaces
{
    public interface IProjectRepository
    {
        Task<Project> CreateProject(Project project);
        Task<Project?> GetProjectById(int id);
        Task<List<Project>> GetAllProjects();
        Task<Project?> UpdateProject(Project updatedProject);
        Task<bool> DeleteProject(int id);

        // to upload file to project
        Task<Doc?> GetFileByIdAsync(int fileId);
        Task<bool> SaveChangesAsync();

        // user project
        Task<bool> ExistsAsync(int userId, int projectId);
        Task<UserProject?> GetMappingAsync(int userId, int projectId);
        Task<List<User>> GetUsersByProjectAsync(int projectId);
        Task<List<Project>> GetProjectsByUserAsync(int userId);

        Task<UserProject?> RemoveMappingAsync(int userId, int projectId);
        Task<bool> AddUserToProject(int userId, int projectId);

        public Task<bool> DeleteUsersFromProject(int projectId);
    }
}

