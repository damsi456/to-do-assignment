using System;
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
    public class UserServiceTest
    {
        private readonly Mock<IUserRepository> _repoMock;
        private readonly UserService _service;

        public UserServiceTest()
        {
            _repoMock = new Mock<IUserRepository>();
            _service = new UserService(_repoMock.Object);
        }

        [Fact]
        public async Task GetByIdAsync_UserExists()
        {
            var user = new User { Id = 5, Username = "damsi", Email = "damsi@12.com", Auth0Id = "auth0|5" };
            _repoMock.Setup(r => r.GetByIdAsync(5)).ReturnsAsync(user);

            // Act
            var dto = await _service.GetByIdAsync(5);

            // Assert
            Assert.NotNull(dto);
            Assert.Equal(5, dto.Id);
            Assert.Equal("damsi", dto.Username);
        }

        [Fact]
        public async Task GetByIdAsync_UserDoesntExist()
        {
            _repoMock.Setup(r => r.GetByIdAsync(99)).ReturnsAsync((User?)null);

            var dto = await _service.GetByIdAsync(99);

            Assert.Null(dto);
        }

        [Fact]
        public async Task CreateAsync_ExistingAuth0Id()
        {
            var existing = new User { Id = 7, Username = "ann", Email = "a@x.com", Auth0Id = "auth0|7" };
            _repoMock.Setup(r => r.GetByAuth0IdAsync("auth0|7")).ReturnsAsync(existing);

            var result = await _service.CreateAsync(
                new CreateUserDto { Username = "ann", Email = "a@x.com", Auth0Id = "auth0|7" });

            Assert.NotNull(result);
            Assert.Equal(7, result.Id);
        }

        [Fact]
        public async Task UpdateAsync_UserNotFound_ReturnsFalse()
        {
            _repoMock.Setup(r => r.GetByIdAsync(20)).ReturnsAsync((User?)null);

            var response = await _service.UpdateAsync(20, new CreateUserDto());

            Assert.False(response);
        }
    }
}