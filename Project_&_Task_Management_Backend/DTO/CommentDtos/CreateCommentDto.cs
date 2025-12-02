using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Project___Task_Management_Backend.DTO.CommentDtos
{
    public class CreateCommentDto
    {
        [Required]
        public int taskId { get; set; }
        [Required]
        public int userId { get; set; }

        public string? commentMessage { get; set; }
        public IFormFile? commentFileForm { get; set; }

    }
}
