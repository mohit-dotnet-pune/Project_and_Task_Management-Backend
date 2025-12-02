using Project___Task_Management_Backend.Models;
using System.ComponentModel.DataAnnotations;

namespace Project___Task_Management_Backend.DTO.ProjectTaskDtos
{
    public class UpdateTaskDto
    {
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
        public IFormFile? formFile { get; set; }
    }

}
