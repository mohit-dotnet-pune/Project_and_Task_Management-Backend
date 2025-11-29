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
                    .ThenInclude(c => c.file)   // 🔥 Include File inside each Comment
                .FirstOrDefaultAsync(t => t.taskId == taskId);
        }


        public async Task AddCommentAsync(Comment comment)
        {
            await _context.comments.AddAsync(comment);
        }

        public List<ProjectTask> GetAll()
        {
            return _context.tasks.ToList();
        }
    }

}
