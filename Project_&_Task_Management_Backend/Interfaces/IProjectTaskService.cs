using Project___Task_Management_Backend.DTO.ProjectTaskDtos;
using Project___Task_Management_Backend.Models;

namespace Project___Task_Management_Backend.Interfaces
{
    public interface IProjectTaskService
    {
        Task<ProjectTask?> CreateTaskAsync(CreateTaskDto dto);
        Task<ProjectTask?> UpdateTaskAsync(int id, UpdateTaskDto dto);
        Task<(bool IsSuccess, string Message)> DeleteTaskAsync(int id);
        Task<ProjectTask?> GetTaskByIdAsync(int id);

        // Special APIs
        Task<(bool IsSuccess, string Message)> AttachUserAsync(int taskId, int userId);
        Task<(bool IsSuccess, string Message)> DetachUserAsync(int taskId);

        Task<(bool IsSuccess, string Message)> AttachFileAsync(int taskId, int fileId);
        Task<(bool IsSuccess, string Message)> DetachFileAsync(int taskId);
        List<ProjectTask> GetAll();
        List<ProjectTask> GetAllTasks(int userId);

        Task<(bool IsSuccess, string Message)> UpdateTasksStatusAsync(UpdateTasksStatusDto dto);

        Task<List<ProjectTask>> GetTasksForProjectAsync(int projectId);

    }

}
