using Microsoft.EntityFrameworkCore;
using Project___Task_Management_Backend.Data;
using Project___Task_Management_Backend.Interfaces;
using Project___Task_Management_Backend.Models;

namespace Project___Task_Management_Backend.Repository
{
    public class ActivityRepository : IActivityRepository
    {
        private readonly AppDbContext _context;

        public ActivityRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Activity> AddAsync(Activity activity)
        {
             _context.activities.Add(activity);
            await _context.SaveChangesAsync();
            return activity;
        }

        public async Task<IEnumerable<Activity>> GetByEntityAsync(EntityType type, int projectId)
        {
            return await _context.activities
                .Where(a => a.activityEntityType == type && a.projectId == projectId)
                .OrderByDescending(a => a.activityCreatedAt)
                .ToListAsync();
        }

        public async Task<IEnumerable<Activity>> GetByUserAsync(int userId)
        {
            return await _context.activities
                .Where(a => a.userId == userId)
                .OrderByDescending(a => a.activityCreatedAt)
                .ToListAsync();
        }

        public async Task<IEnumerable<Activity>> GetAllAsync()
        {
            return await _context.activities
                    .OrderByDescending(a => a.activityCreatedAt)
                .ToListAsync();
        }
        public async Task<IEnumerable<Activity>> GetByProjectIdAsync(int projectId)
        {
            return await _context.activities
                .Where(a => a.projectId == projectId)
                .OrderByDescending(a => a.activityCreatedAt)
                .ToListAsync();
        }

    }
}
