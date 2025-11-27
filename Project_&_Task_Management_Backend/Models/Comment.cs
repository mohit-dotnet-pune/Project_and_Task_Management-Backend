using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Project___Task_Management_Backend.Models
{
    public class Comment
    {
        [Key]
        public int commentId { get; set; }

        [ForeignKey("task")]
        public int taskId { get; set; }

        [ForeignKey("file")]
        public int? fileId { get; set; }

        [ForeignKey("user")]
        public int userId { get; set; }

        public string? commentMessage { get; set; }
        public DateTime commentCreatedAt { get; set; } = DateTime.UtcNow;

        public User? user { get; set; }
        public ProjectTask? task { get; set; }
        public Doc? file { get; set; }
    }
}
