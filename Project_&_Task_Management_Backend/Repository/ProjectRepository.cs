using global::Project___Task_Management_Backend.Data;
using global::Project___Task_Management_Backend.Interfaces;
using global::Project___Task_Management_Backend.Models;
using Microsoft.EntityFrameworkCore;
using Project___Task_Management_Backend.Services;
using System.Threading.Tasks;

namespace Project___Task_Management_Backend.Repository
{
    public class ProjectRepository : IProjectRepository
    {
        private readonly AppDbContext _db;
        private readonly NotificationService _notifier;

        public ProjectRepository(AppDbContext db, NotificationService notifier)
        {
            _notifier = notifier;
            _db = db;
        }

        public async Task<Project> CreateProject(Project project)
        {
            _db.projects.Add(project);
            await _db.SaveChangesAsync();
            return project;
        }

        public async Task<Project?> GetProjectById(int id)
        {
            return await _db.projects
                .Include(p => p.tasks)
                .Include(p => p.file) // 🟢 return tasks also
                .Include(p => p.userProjects)
                    .ThenInclude(up => up.user)
                .FirstOrDefaultAsync(p => p.projectId == id);
        }

        public async Task<List<Project>> GetAllProjects()
        {
            return await _db.projects
                .Include(p => p.tasks)    // return tasks with list also
                .Include(p => p.file)
                .Include(p => p.userProjects)
                .ToListAsync();
        }

        public async Task<Project?> UpdateProject(Project updatedProject)
        {
            var existing = await _db.projects.FindAsync(updatedProject.projectId);
            if (existing == null) return null;

            existing.projectName = updatedProject.projectName;
            existing.projectDescription = updatedProject.projectDescription;
            existing.projectStartDate = updatedProject.projectStartDate;
            existing.projectEndDate = updatedProject.projectEndDate;

            await _db.SaveChangesAsync();
            return existing;
        }

        public async Task<bool> DeleteProject(int id)
        {
            var existing = await _db.projects.FindAsync(id);
            if (existing == null) return false;

            _db.projects.Remove(existing);
            await _db.SaveChangesAsync();
            return true;
        }

        public async Task<Doc?> GetFileByIdAsync(int fileId)
        {
            return await _db.docs.FindAsync(fileId);
        }

        public async Task<bool> SaveChangesAsync()
        {
            return await _db.SaveChangesAsync() > 0;
        }


        // user project
        public async Task<bool> ExistsAsync(int userId, int projectId)
        {
            return await _db.userProjects
                .AnyAsync(up => up.userId == userId && up.projectId == projectId);
        }

        public async Task<UserProject?> GetMappingAsync(int userId, int projectId)
        {
            return await _db.userProjects
                .FirstOrDefaultAsync(up => up.userId == userId && up.projectId == projectId);
        }

        public async Task<UserProject?> RemoveMappingAsync(int userId, int projectId)
        {
            var mapping = await GetMappingAsync(userId, projectId);
            if (mapping == null) return null;
            _db.Remove(mapping);
             await _db.SaveChangesAsync();

            var msg = new NotificationMessage
            {
                Type = "removedFromproject",
                Title = "Removed from Project ",
                Body = $"You have been Removed from Project  (ID: {projectId})"
            };

            await _notifier.SendToUserAsync(userId.ToString(), msg);
            return mapping;
        }

        public async Task<bool> DeleteUsersFromProject(int projectId)
        {
            var existing = _db.userProjects.Where(up => up.projectId == projectId);
            _db.userProjects.RemoveRange(existing);
            await _db.SaveChangesAsync();
            return true;
        }

        public async Task<bool> AddUserToProject(int userId, int projectId)
        {
            if (await ExistsAsync(userId, projectId))
                return false;

            var model = new UserProject
            {
                userId = userId,
                projectId = projectId
            };

             _db.userProjects.Add(model);
             await _db.SaveChangesAsync();

            var msg = new NotificationMessage
            {
                Type = "projectAssigned",
                Title = "Project Assigned",
                Body = $"You have been assigned to Project  (ID: {projectId})"
            };

            await _notifier.SendToUserAsync(userId.ToString(), msg);

            return true;
        }

        public async Task<List<User>> GetUsersByProjectAsync(int projectId)
        {
            return await _db.userProjects
                .Where(up => up.projectId == projectId)
                .Select(up => up.user!)
                .ToListAsync();
        }

        public async Task<List<Project>> GetProjectsByUserAsync(int userId)
        {
            return await _db.userProjects
                .Where(up => up.userId == userId)
                .Select(up => up.project!)
                .ToListAsync();
        }

        
    }
}

