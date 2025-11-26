using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Project___Task_Management_Backend.Models
{
    public class UserProject
    {
        [Key]
        public int userProjectId { get; set; }

        [ForeignKey("project")]
        [Required]
        public int projectId { get; set; }
        [ForeignKey("user")]
        [Required]
        public int userId { get; set; }

        public Project? project { get; set; }
        public User? user { get; set; }
    }
}
