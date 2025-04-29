using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TodoApi.Application.DTOs;

namespace TodoApi.Application.Interfaces
{
    public interface ITaskService
    {
        Task<TaskItemDto?> GetByIdAsync(int id);
        Task<IEnumerable<TaskItemDto>> GetAllAsync();
        public Task<IEnumerable<TaskItemDto>> GetByUserIdAsync(int userId);
        Task<TaskItemDto> CreateAsync(CreateTaskItemDto dto);
        Task<bool> UpdateAsync(int id, UpdateTaskItemDto dto);
        Task<bool> DeleteAsync(int id); 
        Task<IEnumerable<TaskItemDto>> GetFilteredTasksAsync(int userId, bool status);
    }
}