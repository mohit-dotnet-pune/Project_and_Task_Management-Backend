using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;

namespace Project___Task_Management_Backend.Models
{
    public enum Role
    {
        Employee,
        Manager
    }

    public class User
    {
        [Key]
        public int userId {  get; set; }

        [Required]
        public string userName { get; set; }
        [Required]
        public string userEmail { get; set; }
        [Required]
        public string userPassword { get; set; }
        [Required]
        public Role userRole { get; set; }

        public ICollection <UserProject>? userProjects { get; set; }
        public ICollection<ProjectTask>? tasks { get; set; }
        public ICollection<Activity>? activities { get; set; }
        public ICollection<Comment>? comments { get; set; }


    }
}
