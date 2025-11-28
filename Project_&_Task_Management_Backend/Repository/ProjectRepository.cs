using global::Project___Task_Management_Backend.Data;
using global::Project___Task_Management_Backend.Interfaces;
using global::Project___Task_Management_Backend.Models;
using Microsoft.EntityFrameworkCore;

namespace Project___Task_Management_Backend.Repository
{
    public class ProjectRepository : IProjectRepository
    {
        private readonly AppDbContext _db;

        public ProjectRepository(AppDbContext db)
        {
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
                .FirstOrDefaultAsync(p => p.projectId == id);
        }

        public async Task<List<Project>> GetAllProjects()
        {
            return await _db.projects
                .Include(p => p.tasks)    // return tasks with list also
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

    }
}

