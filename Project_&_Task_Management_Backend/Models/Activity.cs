using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Project___Task_Management_Backend.Models
{
    public enum EntityType
    {
        Project,
        Task,
        File,
        Comment
    }
    public class Activity
    {
        [Key]
        public int activityId {  get; set; }

        [ForeignKey("user")]
        [Required]
        public int userId { get; set; }
        
        public int? projectId { get; set; }

        [Required]
        public string activityDescription { get; set; }
        [Required]
        public EntityType activityEntityType { get; set; }
        [Required]
        public int activityEntityId { get; set; }
        public DateTime activityCreatedAt { get; set; } = DateTime.UtcNow;

        public User? user { get; set; }
    }
}
