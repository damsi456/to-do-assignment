using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TodoApi.Application.DTOs;
using TodoApi.Application.Interfaces;

namespace TodoApi.API.Controllers
{
    [ApiController]
    [Route("api/tasks/")]
    public class TaskController : ControllerBase
    {
        private readonly ITaskService _service;

        public TaskController(ITaskService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllTasks()
        {
            var tasks = await _service.GetAllAsync();
            return Ok(tasks);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetTaskById(int id)
        {
            var task = await _service.GetByIdAsync(id);
            if (task == null) return NotFound();
            return Ok(task);
        }

        [HttpPost]
        public async Task<IActionResult> CreateTask([FromBody] CreateTaskItemDto dto)
        {
            var task = await _service.CreateAsync(dto);
            return CreatedAtAction(nameof(GetTaskById), new { id = task.Id }, task);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTask(int id, [FromBody] UpdateTaskItemDto dto)
        {
            var updated = await _service.UpdateAsync(id, dto);
            if (!updated) return NotFound();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTask(int id)
        {
            var deleted = await _service.DeleteAsync(id);
            if (!deleted) return NotFound();
            return NoContent();
        }       

        [HttpGet("/filter/{uid}/{status}")]
        public async Task<IActionResult> GetFilteredTasks(int uid, bool status) 
        {
            var filteredTasks = await _service.GetFilteredTasksAsync(uid, status);
            return Ok(filteredTasks);
        }
    }
}