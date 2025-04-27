using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TodoApi.Domain.Entities
{
    public class User
    {
        public int Id { get; set;}
        [Required]
        public string Username { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public List<TaskItem> Tasks { get; set; } = new List<TaskItem>();
    }
}