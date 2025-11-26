using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Project___Task_Management_Backend.Models
{
    public class Project
    {
        [Key]
        public int projectId { get; set; }

        [Required]
        public string projectName { get; set; }
        [Required]
        public string projectDescription { get; set; }
        [Required]
        public DateTime projectStartDate { get; set; }
        [Required]
        public DateTime projectEndDate { get; set; }
        public DateTime projectCreatedAt { get; set; } = DateTime.UtcNow;

        [ForeignKey("file")]    
        public int? fileId { get; set; }  //❌❌🤣🤣
        public Doc? file { get;set; }  //❌❌🤣🤣

        public ICollection<ProjectTask>? tasks { get; set; }
        public ICollection<UserProject>? userProjects { get; set; }

    }
}
