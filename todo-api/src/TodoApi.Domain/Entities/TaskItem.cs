using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TodoApi.Domain.Entities
{
    public class TaskItem
    {
        public int Id { get; set; }

        [Required] 
        [StringLength(100)]
        public string Title { get; set; } = string.Empty; // Default to empty string

        public DateTime CreatedDate { get; set; } = DateTime.UtcNow; 

        public bool IsCompleted { get; set; } = false; // Default status

        public int UserId { get; set; }

        public User? User { get; set; } //Navigation to user
    }
}