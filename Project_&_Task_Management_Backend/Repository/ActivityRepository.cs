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
            await _context.activities.AddAsync(activity);
            await _context.SaveChangesAsync();
            return activity;
        }

        public async Task<IEnumerable<Activity>> GetByEntityAsync(EntityType type, int entityId)
        {
            return  _context.activities
                .Where(a => a.activityEntityType == type && a.activityEntityId == entityId)
                .OrderByDescending(a => a.activityCreatedAt)
                .ToList();
        }

        public async Task<IEnumerable<Activity>> GetByUserAsync(int userId)
        {
            return _context.activities
                .Where(a => a.userId == userId)
                .OrderByDescending(a => a.activityCreatedAt)
                .ToList();
        }

        public async Task<IEnumerable<Activity>> GetAllAsync()
        {
            return _context.activities
                    .OrderByDescending(a => a.activityCreatedAt)
                    .ToList();
        }

    }
}
