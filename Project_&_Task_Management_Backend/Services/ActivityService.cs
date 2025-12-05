using Project___Task_Management_Backend.DTO.ActivityDtos;
using Project___Task_Management_Backend.Interfaces;
using Project___Task_Management_Backend.Models;

namespace Project___Task_Management_Backend.Services
{
    public class ActivityService : IActivityService
    {
        private readonly IActivityRepository _repo;

        public ActivityService(IActivityRepository repo)
        {
            _repo = repo;
        }

        public async Task LogAsync(CreateActivityDto dto)
        {
            Activity activity = new Activity
            {
                userId = dto.userId,
                projectId = dto.projectId,        // ADD THIS
                activityDescription = dto.activityDescription,
                activityEntityType = dto.activityEntityType,
                activityEntityId = dto.activityEntityId,
                activityCreatedAt = DateTime.UtcNow
            };

            await _repo.AddAsync(activity);
        }


        //public async Task<IEnumerable<ActivityResponseDto>> GetActivityByProjectId(EntityType entityType, int projectId)
        //{
        //    var data = await _repo.GetByEntityAsync(entityType, projectId);

        //    return data.Select(a => new ActivityResponseDto
        //    {
        //        activityId = a.activityId,
        //        userId = a.userId,
        //        activityDescription = a.activityDescription,
        //        activityEntityType = a.activityEntityType,
        //        activityEntityId = a.activityEntityId,
        //        activityCreatedAt = a.activityCreatedAt
        //    });
        //}

        public async Task<IEnumerable<ActivityResponseDto>> GetUserActivity(int userId)
        {
            var data = await _repo.GetByUserAsync(userId);

            return data.Select(a => new ActivityResponseDto
            {
                activityId = a.activityId,
                userId = a.userId,
                activityDescription = a.activityDescription,
                activityEntityType = a.activityEntityType,
                activityEntityId = a.activityEntityId,
                activityCreatedAt = a.activityCreatedAt,
                projectId = a.projectId
            });
        }
       public async Task<IEnumerable<ActivityResponseDto>> GetAllActivities()
        {
            var data = await _repo.GetAllAsync();

            return data.Select(a => new ActivityResponseDto
            {
                activityId = a.activityId,
                userId = a.userId,
                activityDescription = a.activityDescription,
                activityEntityType = a.activityEntityType,
                activityEntityId = a.activityEntityId,
                activityCreatedAt = a.activityCreatedAt,
                projectId = a.projectId,
            });
        }

        public async Task<IEnumerable<ActivityResponseDto>> GetActivitiesByProjectId(int projectId)
        {
            var data = await _repo.GetByProjectIdAsync(projectId);

            return data.Select(a => new ActivityResponseDto
            {
                activityId = a.activityId,
                userId = a.userId,
                activityDescription = a.activityDescription,
                activityEntityType = a.activityEntityType,
                activityEntityId = a.activityEntityId,
                activityCreatedAt = a.activityCreatedAt,
                projectId  = a.projectId
            });
        }

    }
}
