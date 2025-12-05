using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Project___Task_Management_Backend.Models
{
    public class Doc
    {
        [Key]
        public int fileId { get; set; }
        [Required]
        public string fileName { get; set; }
        [Required]
        public string fileURL { get; set; }

    }
}
