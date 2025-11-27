using System.ComponentModel.DataAnnotations;

namespace Project___Task_Management_Backend.DTO
{
    public class CreateProjectDto
    {
        [Required]
        public string projectName { get; set; }
        [Required]
        public string projectDescription { get; set; }
        [Required]
        public DateTime projectStartDate { get; set; }
        [Required]
        public DateTime projectEndDate { get; set; }
    }
}
