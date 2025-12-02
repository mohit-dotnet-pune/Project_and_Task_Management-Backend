using Project___Task_Management_Backend.Data;
using Project___Task_Management_Backend.DTO.CloudinaryDtos;
using Project___Task_Management_Backend.DTO.ProjectTaskDtos;
using Project___Task_Management_Backend.Interfaces;
using Project___Task_Management_Backend.Models;
using System.Threading.Tasks;

namespace Project___Task_Management_Backend.Services
{
    public class ProjectTaskService : IProjectTaskService
    {
        private readonly IProjectTaskRepository _repo;
        private readonly IAuthService _userRepo;
        private readonly IProjectRepository _projectRepo;
        private readonly IProjectRepository _fileRepo;
        private readonly AppDbContext _appDbContext;
        private readonly NotificationService _notifier;
        private readonly CloudinaryService _cloudinaryService;


        public ProjectTaskService(
            IProjectTaskRepository repo,
            IAuthService userRepo,
            IProjectRepository projectRepo,
            IProjectRepository fileRepo,
            AppDbContext appDbContext,
            NotificationService notifer,
            CloudinaryService cloudinaryService
            )
        {
            _repo = repo;
            _userRepo = userRepo;
            _projectRepo = projectRepo;
            _fileRepo = fileRepo;
            _appDbContext = appDbContext;
            _notifier = notifer;
            _cloudinaryService = cloudinaryService;
        }

        public async Task<ProjectTask?> CreateTaskAsync(CreateTaskDto dto)
        {
            var project = await _projectRepo.GetProjectById(dto.projectId);
            if (project == null) return null;

            Doc? doc = null;

            // ⭐ Upload file only if a file is attached
            if (dto.fileForm != null)
            {
                UploadResponse result_doc = await _cloudinaryService.UploadFileAsync(dto.fileForm);

                doc = new Doc
                {
                    fileName = result_doc.FileName,
                    fileURL = result_doc.FileUrl
                };

                _appDbContext.docs.Add(doc);
                await _appDbContext.SaveChangesAsync();
            }

            var task = new ProjectTask
            {
                projectId = dto.projectId,
                taskTitle = dto.taskTitle,
                taskDescription = dto.taskDescription,
                taskPriority = dto.taskPriority,
                taskStatus = dto.taskStatus,
                userId = dto.userId,
                taskDueDate = dto.taskDueDate,

                // Only apply file if it was uploaded
                fileId = doc?.fileId,
                file = doc
            };

            _appDbContext.tasks.Add(task);
            await _appDbContext.SaveChangesAsync();

            return task;
        }


        public async Task<ProjectTask?> UpdateTaskAsync(int id, UpdateTaskDto dto)
        {
            var task = await _appDbContext.tasks.FindAsync(id);
            if (task == null) return null;

            Doc? doc = null;

            // ⭐ Upload file only if a file was actually attached
            if (dto.formFile != null)
            {
                UploadResponse result_doc = await _cloudinaryService.UploadFileAsync(dto.formFile);

                doc = new Doc
                {
                    fileName = result_doc.FileName,
                    fileURL = result_doc.FileUrl
                };

                _appDbContext.docs.Add(doc);
                await _appDbContext.SaveChangesAsync();

                // Update file reference only when a new file is uploaded
                task.fileId = doc.fileId;
                task.file = doc;
            }

            // 🔔 Send notification if status changed
            if (task.taskStatus != dto.taskStatus && task.userId != null)
            {
                var msg = new NotificationMessage
                {
                    Type = "taskStatusUpdated",
                    Title = $"Task status updated to {dto.taskStatus}",
                    Body = $"Your task '{task.taskTitle}' (ID: {task.taskId}) was updated"
                };

                await _notifier.SendToUserAsync(task.userId.ToString(), msg);
            }

            // Update task fields
            task.taskTitle = dto.taskTitle;
            task.taskDescription = dto.taskDescription;
            task.taskPriority = dto.taskPriority;
            task.taskStatus = dto.taskStatus;
            task.taskDueDate = dto.taskDueDate;

            await _appDbContext.SaveChangesAsync();
            return task;
        }


        public async Task<(bool IsSuccess, string Message)> DeleteTaskAsync(int id)
        {
            var task = await _appDbContext.tasks.FindAsync(id);
            if (task == null) return (false, $"Task is not present with id {id}");

            _appDbContext.tasks.Remove(task);
            await _appDbContext.SaveChangesAsync();
            return (true, "Task deleted successfully!");
        }

        public async Task<ProjectTask?> GetTaskByIdAsync(int id)
        {
            return await _repo.GetTaskWithDetailsAsync(id);
        }

        public List<ProjectTask> GetAll()
        {
            return _repo.GetAll();
        }

        public async Task<(bool IsSuccess, string Message)> AttachUserAsync(int taskId, int userId)
        {
            var task = await _appDbContext.tasks.FindAsync(taskId);
            if (task == null) return (false, $"Task is not present with id {taskId}");

            var user = await _appDbContext.users.FindAsync(userId);
            if (user == null) return (false, $"User is not present with id {userId}");

            task.userId = userId;
            task.user = user;
            await _appDbContext.SaveChangesAsync();

            var msg = new NotificationMessage
            {
                Type = "taskAssigned",
                Title = "Task Assigned",
                Body = $"You have been assigned to Task {task.taskTitle},  (ID: {taskId})"
            };

            await _notifier.SendToUserAsync(userId.ToString(), msg);

            return (true, $"Task {task.taskTitle} is assigned to user {user.userName}"); ;
        }

        public async Task<(bool IsSuccess, string Message)> DetachUserAsync(int taskId)
        {
            var task = await _appDbContext.tasks.FindAsync(taskId);
            if (task == null) return (false, $"Task is not present with id {taskId}");;

            var msg = new NotificationMessage
            {
                Type = "removedFromtask",
                Title = "Removed from Task",
                Body = $"You have been removed From Task {task.taskTitle},  (ID: {taskId})"
            };

            await _notifier.SendToUserAsync(task.userId.ToString(), msg);

            task.userId = null;
            task.user = null;
            await _appDbContext.SaveChangesAsync();

       
            return (true, $"Task {task.taskTitle} is  Removed from user");
        }

        public async Task<(bool IsSuccess, string Message)> AttachFileAsync(int taskId, int fileId)
        {
            var task = await _appDbContext.tasks.FindAsync(taskId);
            if (task == null) return (false, $"Task {task.taskTitle} is not present");

            var file = await _appDbContext.docs.FindAsync(fileId);
            if (file == null) return (false, $"file not present with file id {fileId}");

            task.fileId = fileId;
            task.file = file;
            
            await _appDbContext.SaveChangesAsync();
            return (true, $"File {file.fileName} is attached to taks {task.taskTitle}");

        }

        public async Task<(bool IsSuccess, string Message)> DetachFileAsync(int taskId)
        {
            var task = await _appDbContext.tasks.FindAsync(taskId);
            if (task == null) return (false, $"Task {task.taskTitle} is not present");


            task.fileId = null;
            task.file = null;

            await _appDbContext.SaveChangesAsync();
            return (true, $"File  is dettached from taks {task.taskTitle}");
        }

        public List<ProjectTask> GetAllTasks(int userId)
        {
            return _appDbContext.tasks.Where(t => t.userId == userId).ToList();
        }

        public async Task<(bool IsSuccess, string Message)> UpdateTasksStatusAsync(UpdateTasksStatusDto dto)
        {
            // Update TODO tasks
            if (dto.TodoTaskidUpdate != null)
            {
                foreach (var id in dto.TodoTaskidUpdate)
                {
                    var task = await _appDbContext.tasks.FindAsync(id);
                    if (task != null)
                        task.taskStatus = Status.Todo;
                }
            }

            // Update INPROGRESS tasks
            if (dto.InProgressTaskidUpdate != null)
            {
                foreach (var id in dto.InProgressTaskidUpdate)
                {
                    var task = await _appDbContext.tasks.FindAsync(id);
                    if (task != null)
                        task.taskStatus = Status.Inprogress;
                }
            }

            // Update DONE tasks
            if (dto.DoneTaskidUpdate != null)
            {
                foreach (var id in dto.DoneTaskidUpdate)
                {
                    var task = await _appDbContext.tasks.FindAsync(id);
                    if (task != null)
                        task.taskStatus = Status.Done;
                }
            }

            await _appDbContext.SaveChangesAsync();

            return (true, "Tasks status updated successfully!");
        }
        public async Task<List<ProjectTask>> GetTasksForProjectAsync(int projectId)
        {
            var project = await _projectRepo.GetProjectById(projectId);
            if (project == null) return null;

            return await _repo.GetTasksByProjectId(projectId);
        }


    }

}
