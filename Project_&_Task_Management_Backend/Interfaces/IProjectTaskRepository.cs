using Project___Task_Management_Backend.Models;

namespace Project___Task_Management_Backend.Interfaces
{
    public interface IProjectTaskRepository
    {
        Task<ProjectTask?> GetTaskWithDetailsAsync(int taskId);
        Task AddCommentAsync(Comment comment);
        List<ProjectTask> GetAll();

        Task<List<ProjectTask>> GetTasksByProjectId(int projectId);
    }

}
