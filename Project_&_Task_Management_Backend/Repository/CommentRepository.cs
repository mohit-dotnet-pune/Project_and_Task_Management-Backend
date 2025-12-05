using Microsoft.EntityFrameworkCore;
using Project___Task_Management_Backend.Data;
using Project___Task_Management_Backend.Interfaces;
using Project___Task_Management_Backend.Models;

namespace Project___Task_Management_Backend.Repository
{
    public class CommentRepository : ICommentRepository
    {
        private readonly AppDbContext _context;

        public CommentRepository(AppDbContext context) 
        {
            _context = context;
        }

        public async Task<IEnumerable<Comment>> GetCommentsByTaskId(int taskId)
        {
            return await _context.comments
                .Where(c => c.taskId == taskId)
                .Include(c => c.user)
                .Include(c => c.file)
                .ToListAsync();
        }

        public async Task<IEnumerable<Comment>> GetCommentsByUserId(int userId)
        {
            return await _context.comments
                .Where(c => c.userId == userId)
                .Include(c => c.task)
                .Include(c => c.file)
                .Include(c => c.user)
                .ToListAsync();
        }
    }

}
