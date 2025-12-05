using global::Project___Task_Management_Backend.DTO.ProjectDtos;
using global::Project___Task_Management_Backend.Interfaces;
using global::Project___Task_Management_Backend.Models;
using Project___Task_Management_Backend.Data;
using Project___Task_Management_Backend.DTO.CloudinaryDtos;
using Sprache;

namespace Project___Task_Management_Backend.Services
{
    public class ProjectService : IProjectService
    {
        private readonly IProjectRepository _repo;

        private readonly CloudinaryService _cloudinaryService;
        private readonly AppDbContext _db;

        public ProjectService(IProjectRepository repo, CloudinaryService cloudinaryService, AppDbContext db)
        {
            _repo = repo;
            _cloudinaryService = cloudinaryService;
            _db = db;
        }

        public async Task<(bool IsSuccess, string Message, Project? Data)> CreateProjectAsync(CreateProjectDto dto)
        {
            try
            {
                Doc? doc = null;

                // ⭐ Upload only when file is attached
                if (dto.formFile != null)
                {
                    UploadResponse file_response = await _cloudinaryService.UploadFileAsync(dto.formFile);

                    doc = new Doc
                    {
                        fileName = file_response.FileName,
                        fileURL = file_response.FileUrl
                    };

                    _db.docs.Add(doc);
                    await _db.SaveChangesAsync();
                }

                var project = new Project
                {
                    projectName = dto.projectName,
                    projectDescription = dto.projectDescription,
                    projectStartDate = dto.projectStartDate,
                    projectEndDate = dto.projectEndDate,
                    projectStatus = projStatus.ToDo,
                    projectCreatedAt = DateTime.UtcNow,
                    file = doc,
                    fileId = doc?.fileId  // ⭐ handles null safely
                };

                // Create Project
                var savedProject = await _repo.CreateProject(project);
                if (savedProject == null)
                    return (false, "Failed to create project.", null);

                // Add users
                if (dto.UserIds != null && dto.UserIds.Count > 0)
                {
                    foreach (var userId in dto.UserIds.Distinct())
                    {
                        await _repo.AddUserToProject(userId, savedProject.projectId);
                    }
                }

                return (true, "Project created successfully.", savedProject);
            }
            catch (Exception ex)
            {
                return (false, $"Error: {ex.Message}", null);
            }
        }

        public async Task<Project?> UpdateProject(int id, UpdateProjectDto dto)
        {
            // Get project
            var project = await _repo.GetProjectById(id);
            if (project == null)
                return null;

            Doc? doc = null;

            // ⭐ Upload new file only if the user actually provided it
            if (dto.formfile != null)
            {
                UploadResponse file_response = await _cloudinaryService.UploadFileAsync(dto.formfile);

                doc = new Doc
                {
                    fileName = file_response.FileName,
                    fileURL = file_response.FileUrl
                };

                _db.docs.Add(doc);
                await _db.SaveChangesAsync();

                // Update only if file exists
                project.fileId = doc.fileId;
                project.file = doc;
            }

            // 🔹 Validation
            if (dto.projectEndDate < dto.projectStartDate)
                throw new Exception("End date cannot be before start date.");

            // 🔥 Update fields
            project.projectName = dto.projectName;
            project.projectDescription = dto.projectDescription;
            project.projectStartDate = dto.projectStartDate;
            project.projectEndDate = dto.projectEndDate;

            // ===============================
            // UPDATE ASSIGNED USERS
            // ===============================

            // Remove existing users first
            await _repo.DeleteUsersFromProject(id);

            // Add updated users
            if (dto.UserIds != null && dto.UserIds.Count > 0)
            {
                foreach (var userId in dto.UserIds.Distinct())
                {
                    await _repo.AddUserToProject(userId, id);
                }
            }

            // Save project changes
            var updatedProject = await _repo.UpdateProject(project);

            return updatedProject;
        }



        public Task<Project?> GetProject(int id)
        {
            return _repo.GetProjectById(id);
        }

        public Task<List<Project>> GetAllProjects()
        {
            return _repo.GetAllProjects();
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
            else return (false, "user is already present in the project ");
        }

        public async Task<(bool IsSuccess, string Message)> RemoveUserFromProjectAsync(int userId, int projectId)
        {
            var ans = await _repo.RemoveMappingAsync(userId, projectId);
            if (ans == null) return (false, "user id or project id is not present");
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
