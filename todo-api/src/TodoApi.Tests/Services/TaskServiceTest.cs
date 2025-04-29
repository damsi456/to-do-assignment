using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Moq;
using TodoApi.Application.DTOs;
using TodoApi.Application.Interfaces;
using TodoApi.Application.Services;
using TodoApi.Domain.Entities;
using Xunit;

namespace TodoApi.Tests.Services
{
    public class TaskServiceTest
    {
        private readonly Mock<ITaskRepository> _repoMock;
        private readonly TaskService _service;

        public TaskServiceTest()
        {
            _repoMock = new Mock<ITaskRepository>();
            _service = new TaskService(_repoMock.Object);
        }

        [Fact]
        public async Task GetByIdAsync_TaskExists()
        {
            var task = new TaskItem { Id = 3, Title = "Do dishes", IsCompleted = true, UserId = 2 };
            _repoMock.Setup(r => r.GetByIdAsync(3)).ReturnsAsync(task);

            var dto = await _service.GetByIdAsync(3);

            Assert.NotNull(dto);
            Assert.Equal("Do dishes", dto.Title);
        }

        [Fact]
        public async Task GetAllAsync()
        {
            var items = new[]
            {
                new TaskItem { Id = 1, Title = "A", IsCompleted = false, UserId = 9 },
                new TaskItem { Id = 2, Title = "B", IsCompleted = true,  UserId = 9 }
            };
            _repoMock.Setup(r => r.GetAllAsync()).ReturnsAsync(items);

            var list = (await _service.GetAllAsync()).ToList();

            Assert.Equal(2, list.Count);
            Assert.Equal("B", list[1].Title);
        }

        [Fact]
        public async Task CreateAsync_SetsDefaultsAndReturnsDto()
        {
            var input = new CreateTaskItemDto { Title = "X", UserId = 8 };
            TaskItem captured = null!;
            _repoMock.Setup(r => r.AddAsync(It.IsAny<TaskItem>()))
                     .Callback<TaskItem>(t => captured = t)
                     .Returns(Task.CompletedTask);

            var result = await _service.CreateAsync(input);

            Assert.NotNull(captured);
            Assert.Equal("X", captured.Title);
            Assert.Equal(8, result.UserId);
        }

        [Fact]
        public async Task DeleteAsync_NonExisting()
        {
            _repoMock.Setup(r => r.GetByIdAsync(42)).ReturnsAsync((TaskItem?)null);

            var ok = await _service.DeleteAsync(42);

            Assert.False(ok);
        }
    }
}