using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Project___Task_Management_Backend.Models
{
    public enum projStatus
    {
        ToDo,
        Inprogress,
        Done
    }
    public class Project
    {
        [Key]
        public int projectId { get; set; }

        [ForeignKey("file")]
        public int? fileId { get; set; }

        [Required]
        public string projectName { get; set; }
        [Required]
        public string projectDescription { get; set; }
        [Required]
        public DateTime projectStartDate { get; set; }
        [Required]
        public projStatus projectStatus { get; set; }
        [Required]
        public DateTime projectEndDate { get; set; }
        public DateTime projectCreatedAt { get; set; } = DateTime.UtcNow;


        public ICollection<ProjectTask>? tasks { get; set; }
        public ICollection<UserProject>? userProjects { get; set; }
        public Doc? file { get; set; }

    }
}
