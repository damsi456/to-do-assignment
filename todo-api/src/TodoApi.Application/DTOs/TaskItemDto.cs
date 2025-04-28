using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TodoApi.Application.DTOs
{
    public class TaskItemDto
    {
        public int Id { get; set; }
        public string title { get; set; } = string.Empty;
        public bool IsCompleted { get; set; }
        public int UserId { get; set; }
    }

    public class CreateTaskItemDto
    {
        public string Title { get; set; } = string.Empty;
        public int UserId { get; set; }
    }

    public class UpdateTaskItemDto
    {
        public string Title { get; set;} = string.Empty;
        public bool IsCompleted { get; set; }
    }
}