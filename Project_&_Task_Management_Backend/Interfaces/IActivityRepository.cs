using Project___Task_Management_Backend.Models;

namespace Project___Task_Management_Backend.Interfaces
{
    public interface IActivityRepository
    {
        Task<Activity> AddAsync(Activity activity);
        Task<IEnumerable<Activity>> GetByProjectIdAsync(int projectId);
        Task<IEnumerable<Activity>> GetByUserAsync(int userId);
        Task<IEnumerable<Activity>> GetAllAsync();

    }
}
