using Project___Task_Management_Backend.Models;

namespace Project___Task_Management_Backend.DTO.ActivityDtos
{
    public class ActivityResponseDto
    {
        public int activityId { get; set; }
        public int userId { get; set; }
        public int? projectId { get; set; }
        public string activityDescription { get; set; }
        public EntityType activityEntityType { get; set; }
        public int activityEntityId { get; set; }
        public DateTime activityCreatedAt { get; set; }
    }
}
