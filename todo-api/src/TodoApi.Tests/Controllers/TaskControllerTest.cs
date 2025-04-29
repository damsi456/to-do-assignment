using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Moq;
using TodoApi.API.Controllers;
using TodoApi.Application.DTOs;
using TodoApi.Application.Interfaces;
using Xunit;

namespace TodoApi.Tests.Controllers
{
    public class TaskControllerTest
    {
        private readonly Mock<ITaskService> _serviceMock;

        private readonly TaskController _controller;

        public TaskControllerTest()
        {
            _serviceMock = new Mock<ITaskService>();
            _controller = new TaskController(_serviceMock.Object);
        }

        [Fact]
        public async Task GetTaskById_TaskExists()  
        {
            var dto = new TaskItemDto
            {
                Id = 1,
                Title = "Redi sodann",
                IsCompleted = false,
                UserId = 1
            };
            _serviceMock.Setup(s => s.GetByIdAsync(1)).ReturnsAsync(dto);

            var result = await _controller.GetTaskById(1);

            var ok = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(dto, ok.Value);
        } 

        [Fact]
        public async Task GetTaskById_TaskDoesntExist()
        {
            _serviceMock.Setup(s => s.GetByIdAsync(2)).ReturnsAsync((TaskItemDto?)null);

            var result = await _controller.GetTaskById(2);

            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task GetAllTasks()
        {
            // Arrage the cases and mock accordingly
            var taskDtos = new List<TaskItemDto>
            {
                new TaskItemDto {Id = 1, Title = "Do Dishes", IsCompleted = false, UserId = 10 },
                new TaskItemDto { Id = 2, Title = "Hit Gym", IsCompleted = true,  UserId = 10 }
            };
            _serviceMock.Setup(s => s.GetAllAsync()).ReturnsAsync(taskDtos);
            
            // When
            var result = await _controller.GetAllTasks();
        
            // Then
            var ok = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(taskDtos, ok.Value);
        }

        [Fact]
        public async Task GetTaskByUserId_UserExists()
        {
            var userTasks = new List<TaskItemDto>
            {
                new TaskItemDto { Id = 1, Title = "Do dishes", IsCompleted = false, UserId = 5 },
                new TaskItemDto { Id = 2, Title = "Wash clothes", IsCompleted = true,  UserId = 5 }
            };
            _serviceMock.Setup(s => s.GetByUserIdAsync(5)).ReturnsAsync(userTasks);

            var result = await _controller.GetTaskByUserId(5);

            var ok = Assert.IsType<OkObjectResult>(result);
            var returned = Assert.IsAssignableFrom<IEnumerable<TaskItemDto>>(ok.Value);
            Assert.Equal(userTasks, returned);
        }

        [Fact]
        public async Task CreateTask__ReturnsCreatedAtAction()
        {
            var input = new CreateTaskItemDto { Title = "Do disehs", UserId = 5};
            var created = new TaskItemDto { Id = 1, Title = "Do disehs", IsCompleted = false,  UserId = 5 };
            _serviceMock.Setup(s => s.CreateAsync(input)).ReturnsAsync(created);

            var result = await _controller.CreateTask(input);
            var createdAt = Assert.IsType<CreatedAtActionResult>(result);

            var task = Assert.IsType<TaskItemDto>(createdAt.Value);
            Assert.Equal(1, task.Id);
        }

        [Fact]
        public async Task UpdateTask_ExistingId_ReturnsNoContent()
        {
            var id = 7;
            _serviceMock.Setup(s => s.UpdateAsync(id, It.IsAny<UpdateTaskItemDto>()))
                        .ReturnsAsync(true);

            var result = await _controller.UpdateTask(id, new UpdateTaskItemDto());

            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task UpdateTask_NonExistingId_ReturnsNotFound()
        {
            var id = 8;
            _serviceMock.Setup(s => s.UpdateAsync(id, It.IsAny<UpdateTaskItemDto>()))
                        .ReturnsAsync(false);

            var result = await _controller.UpdateTask(id, new UpdateTaskItemDto());

            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task DeleteTask_ExistingId_ReturnsNoContent()
        {
            var id = 9;
            _serviceMock.Setup(s => s.DeleteAsync(id))
                        .ReturnsAsync(true);

            var result = await _controller.DeleteTask(id);

            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task DeleteTask_NonExistingId_ReturnsNotFound()
        {
            var id = 10;
            _serviceMock.Setup(s => s.DeleteAsync(id))
                        .ReturnsAsync(false);

            var result = await _controller.DeleteTask(id);

            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task GetFilteredTasks_ReturnsOkWithFilteredTasks()
        {
            var uid    = 5;
            var status = true;
            var filtered = new List<TaskItemDto>
            {
                new TaskItemDto { Id = 11, Title = "Do disehs", IsCompleted = status, UserId = uid },
                new TaskItemDto { Id = 12, Title = "Clean the room", IsCompleted = status, UserId = uid }
            };
            _serviceMock.Setup(s => s.GetFilteredTasksAsync(5, status))
                        .ReturnsAsync(filtered);

            var result = await _controller.GetFilteredTasks(uid, status);

            var ok   = Assert.IsType<OkObjectResult>(result);
            var list = Assert.IsAssignableFrom<IEnumerable<TaskItemDto>>(ok.Value);
            Assert.Equal(filtered, list);
        }
    }
}