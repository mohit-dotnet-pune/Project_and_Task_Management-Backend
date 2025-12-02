using Project___Task_Management_Backend.DTO.ActivityDtos;
using Project___Task_Management_Backend.Models;

namespace Project___Task_Management_Backend.Interfaces
{
    public interface IActivityService
    {
        Task LogAsync(CreateActivityDto dto);
        Task<IEnumerable<ActivityResponseDto>> GetEntityActivity(EntityType entityType, int entityId);
        Task<IEnumerable<ActivityResponseDto>> GetUserActivity(int userId);
        Task<IEnumerable<ActivityResponseDto>> GetAllActivities();
    }
}
