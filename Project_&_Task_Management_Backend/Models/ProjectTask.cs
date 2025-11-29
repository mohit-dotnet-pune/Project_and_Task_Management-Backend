using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Project___Task_Management_Backend.Models
{
    public enum Priority
    {
        Low,
        Medium,
        High
    }

    public enum Status
    {
        Todo,
        Inprogress,
        Done
    }
    public class ProjectTask
    {
        [Key]
        public int taskId { get; set; }

        [ForeignKey("project")]
        [Required]
        public int projectId { get; set; }

        [ForeignKey("user")]

        public int? userId { get; set; }

        [ForeignKey("file")]

        public int? fileId { get; set; }

        [Required]
        public string taskTitle { get; set; }
        [Required]
        public string taskDescription { get; set; }
        [Required]
        public Priority taskPriority { get; set; }
        [Required]
        public Status taskStatus { get; set; }
        [Required]
        public DateTime taskDueDate { get; set; }
        public DateTime taskCreatedAt { get; set; } = DateTime.UtcNow;

        public User? user {  get; set; }
        public Project? project {  get; set; }
        public ICollection<Comment>? comments { get; set; }
        public Doc? file { get; set; }
    }
}
