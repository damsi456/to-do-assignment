using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TodoApi.Application.DTOs;
using TodoApi.Application.Interfaces;
using TodoApi.Domain.Entities;

namespace TodoApi.Application.Services
{
    // Mapping dto to the entity and do the logic using repo
    public class TaskService : ITaskService
    {
        private readonly ITaskRepository _repo;

        public TaskService(ITaskRepository repo)
        {
            _repo = repo;
        }

        public async Task<TaskItemDto> GetByIdAsync(int id)
        {
            var t = await _repo.GetByIdAsync(id);
            if (t == null)
            {
                return null;
            } 
            return new TaskItemDto
            {
                Id = t.Id,
                Title = t.Title,
                IsCompleted = t.IsCompleted,
                UserId = t.UserId
            };
        }

        public async Task<IEnumerable<TaskItemDto>> GetAllAsync()
        {
            var tasks = await _repo.GetAllAsync();
            return tasks.Select(t => new TaskItemDto
            {
                Id = t.Id,
                Title = t.Title,
                IsCompleted = t.IsCompleted,
                UserId = t.UserId
            });
        }

        public async Task<TaskItemDto> CreateAsync(CreateTaskItemDto dto)
        {
            var task = new TaskItem
            {
                Title = dto.Title,
                UserId = dto.UserId
            };
            await _repo.AddAsync(task);
            return new TaskItemDto
            {
                Id = task.Id,
                Title = task.Title,
                IsCompleted = task.IsCompleted,
                UserId = task.UserId
            };
        }

        public async Task<bool> UpdateAsync(int id, UpdateTaskItemDto dto)
        {
            var task = await _repo.GetByIdAsync(id);
            if (task == null) return false;

            task.Title = dto.Title;
            task.IsCompleted = dto.IsCompleted;
            await _repo.UpdateAsync(task);
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var task = await _repo.GetByIdAsync(id);
            if (task == null) return false;

            await _repo.DeleteAsync(id);
            return true;
        }

        public async Task<IEnumerable<TaskItemDto>> GetFilteredTasksAsync(int userId, bool status)
        {
            var tasks = await _repo.GetFilteredTasksAsync(userId, status);
            return tasks.Select(t => new TaskItemDto
                {
                    Id = t.Id,
                    Title = t.Title,
                    IsCompleted = t.IsCompleted,
                    UserId = t.UserId
                });
        }
    }
}