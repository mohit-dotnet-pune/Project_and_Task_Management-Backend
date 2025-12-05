using Microsoft.EntityFrameworkCore;
using Project___Task_Management_Backend.Data;
using Project___Task_Management_Backend.Interfaces;
using Project___Task_Management_Backend.Models;

namespace Project___Task_Management_Backend.Repository
{
    public class ProjectTaskRepository : IProjectTaskRepository
    {
        private readonly AppDbContext _context;

        public ProjectTaskRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<ProjectTask?> GetTaskWithDetailsAsync(int taskId)
        {
            return await _context.tasks
                .Include(t => t.user)
                .Include(t => t.project)
                .Include(t => t.file)
                .Include(t => t.comments)
                    .ThenInclude(c => c.user)   // ✔ get user of each comment
                .Include(t => t.comments)
                    .ThenInclude(c => c.file)   // ✔ get file of each comment
                .FirstOrDefaultAsync(t => t.taskId == taskId);
        }



        public async Task AddCommentAsync(Comment comment)
        {
            await _context.comments.AddAsync(comment);
        }

        public List<ProjectTask> GetAll()
        {
            return _context.tasks
                .Include(c => c.comments)
                .ToList();
        }


        public async Task<List<ProjectTask>> GetTasksByProjectId(int projectId)
        {
            return await _context.tasks
                .Where(t => t.projectId == projectId)
                .Include(t => t.user)
                .Include(t => t.project)
                .Include(t => t.file)
                .Include(t => t.comments)
                    .ThenInclude(c => c.file)
                .ToListAsync();
        }


    }

}
