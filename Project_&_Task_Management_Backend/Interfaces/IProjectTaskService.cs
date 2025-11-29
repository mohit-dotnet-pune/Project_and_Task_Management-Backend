using Project___Task_Management_Backend.DTO.ProjectTaskDtos;
using Project___Task_Management_Backend.Models;

namespace Project___Task_Management_Backend.Interfaces
{
    public interface IProjectTaskService
    {
        Task<ProjectTask?> CreateTaskAsync(CreateTaskDto dto);
        Task<ProjectTask?> UpdateTaskAsync(int id, UpdateTaskDto dto);
        Task<bool> DeleteTaskAsync(int id);
        Task<ProjectTask?> GetTaskByIdAsync(int id);

        // Special APIs
        Task<bool> AttachUserAsync(int taskId, int userId);
        Task<bool> DetachUserAsync(int taskId);

        Task<bool> AttachFileAsync(int taskId, int fileId);
        Task<bool> DetachFileAsync(int taskId);
        List<ProjectTask> GetAll();
    }

}
