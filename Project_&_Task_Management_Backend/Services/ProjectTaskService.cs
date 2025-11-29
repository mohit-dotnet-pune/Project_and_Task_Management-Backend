using Project___Task_Management_Backend.Data;
using Project___Task_Management_Backend.DTO.ProjectTaskDtos;
using Project___Task_Management_Backend.Interfaces;
using Project___Task_Management_Backend.Models;

namespace Project___Task_Management_Backend.Services
{
    public class ProjectTaskService : IProjectTaskService
    {
        private readonly IProjectTaskRepository _repo;
        private readonly IAuthService _userRepo;
        private readonly IProjectRepository _projectRepo;
        private readonly IProjectRepository _fileRepo;
        private readonly AppDbContext _appDbContext;
        public ProjectTaskService(
            IProjectTaskRepository repo,
            IAuthService userRepo,
            IProjectRepository projectRepo,
            IProjectRepository fileRepo,
            AppDbContext appDbContext
            )
        {
            _repo = repo;
            _userRepo = userRepo;
            _projectRepo = projectRepo;
            _fileRepo = fileRepo;
            _appDbContext = appDbContext;
        }

        public async Task<ProjectTask?> CreateTaskAsync(CreateTaskDto dto)
        {
            var project = await _projectRepo.GetProjectById(dto.projectId);
            if (project == null) return null;

            var task = new ProjectTask
            {
                projectId = dto.projectId,
                taskTitle = dto.taskTitle,
                taskDescription = dto.taskDescription,
                taskPriority = dto.taskPriority,
                taskStatus = dto.taskStatus,
                taskDueDate = dto.taskDueDate
            };

            _appDbContext.tasks.Add(task);
            await _appDbContext.SaveChangesAsync();

            return task;
        }

        public async Task<ProjectTask?> UpdateTaskAsync(int id, UpdateTaskDto dto)
        {
            var task = await _appDbContext.tasks.FindAsync(id);
            if (task == null) return null;

            task.taskTitle = dto.taskTitle;
            task.taskDescription = dto.taskDescription;
            task.taskPriority = dto.taskPriority;
            task.taskStatus = dto.taskStatus;
            task.taskDueDate = dto.taskDueDate;

            await _appDbContext.SaveChangesAsync();
            return task;
        }

        public async Task<bool> DeleteTaskAsync(int id)
        {
            var task = await _appDbContext.tasks.FindAsync(id);
            if (task == null) return false;

            _appDbContext.tasks.Remove(task);
            await _appDbContext.SaveChangesAsync();
            return true;
        }

        public async Task<ProjectTask?> GetTaskByIdAsync(int id)
        {
            return await _repo.GetTaskWithDetailsAsync(id);
        }

        public List<ProjectTask> GetAll()
        {
            return _repo.GetAll();
        }

        public async Task<bool> AttachUserAsync(int taskId, int userId)
        {
            var task = await _appDbContext.tasks.FindAsync(taskId);
            if (task == null) return false;

            var user = await _appDbContext.users.FindAsync(userId);
            if (user == null) return false;

            task.userId = userId;
            task.user = user;
            await _appDbContext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DetachUserAsync(int taskId)
        {
            var task = await _appDbContext.tasks.FindAsync(taskId);
            if (task == null) return false;

            task.userId = null;
            task.user = null;
            await _appDbContext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> AttachFileAsync(int taskId, int fileId)
        {
            var task = await _appDbContext.tasks.FindAsync(taskId);
            if (task == null) return false;

            var file = await _appDbContext.docs.FindAsync(fileId);
            if (file == null) return false;

            task.fileId = fileId;
            task.file = file;
            
            await _appDbContext.SaveChangesAsync();
            return true;
            
        }

        public async Task<bool> DetachFileAsync(int taskId)
        {
            var task = await _appDbContext.tasks.FindAsync(taskId);
            if (task == null) return false;


            task.fileId = null;
            task.file = null;

            await _appDbContext.SaveChangesAsync();
            return true;
        }

    }

}
