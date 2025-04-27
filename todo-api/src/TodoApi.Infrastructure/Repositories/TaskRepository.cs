using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TodoApi.Application.Interfaces;
using TodoApi.Domain.Entities;
using TodoApi.Infrastructure.Persistence;

namespace TodoApi.Infrastructure.Repositories
{
    public class TaskRepository : ITaskRepository
    {
        private readonly ApplicationDbContext _context;

        // Inject db context
        public TaskRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<TaskItem?> GetByIdAsync(int id) 
        {
            return await _context.TaskItems.FindAsync(id);
        }

        public async Task<IEnumerable<TaskItem>> GetAllAsync()
        { 
            return await _context.TaskItems.ToListAsync();
        }

        public async Task<IEnumerable<TaskItem>> GetByUserIdAsync(int userId)
        {
            return await _context.TaskItems.Where(t => t.UserId == userId).ToListAsync();
        }

        public async Task AddAsync(TaskItem taskItem)
        {
            await _context.TaskItems.AddAsync(taskItem);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(TaskItem taskItem)
        {
            _context.TaskItems.Update(taskItem);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id) 
        {
            var task = await _context.TaskItems.FindAsync(id);
            if (task != null)
            {
                _context.TaskItems.Remove(task);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<TaskItem>> GetFilteredTasksAsync(int userId, bool status)
        {
            return await _context.TaskItems.Where(t => t.UserId == userId && t.IsCompleted == status).ToListAsync();
        }
    }
}