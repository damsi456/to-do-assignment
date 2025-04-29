using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Moq;
using TodoApi.API.Controllers;
using TodoApi.Application.DTOs;
using TodoApi.Application.Interfaces;
using Xunit;

namespace TodoApi.Tests.Controllers
{
    public class UserControllerTests
    {
        private readonly Mock<IUserService> _serviceMock;
        private readonly UserController _controller;

        public UserControllerTests()
        {
            _serviceMock = new Mock<IUserService>();
            _controller = new UserController(_serviceMock.Object);
        }

        [Fact]
        public async Task GetUserById_UserExists()
        {
            // Arrange test cases and mock service accordingly
            var dto = new UserDto { Id = 1, Username = "damsi456", Email = "damsi@456.com", Auth0Id = "auth0|1" };
            _serviceMock.Setup(s => s.GetByIdAsync(1)).ReturnsAsync(dto);

            // Act
            var result = await _controller.GetUserbyId(1);

            // Assert (check)
            var ok = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(dto, ok.Value);
        }

        [Fact]
        public async Task GetUserById_UserNotFound()
        {
            _serviceMock.Setup(s => s.GetByIdAsync(2)).ReturnsAsync((UserDto?)null);

            var result = await _controller.GetUserbyId(2);

            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task GetUserByEmail_UserExists()
        {
            var dto = new UserDto { Id = 3, Username = "bob", Email = "b@x.com", Auth0Id = "auth0|3" };
            _serviceMock.Setup(s => s.GetByEmailAsync("b@x.com")).ReturnsAsync(dto);

            var result = await _controller.GetUserByEmail("b@x.com");

            var ok = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(dto, ok.Value);
        }

        [Fact]
        public async Task GetUserByEmail_UserNotFound()
        {
            _serviceMock.Setup(s => s.GetByEmailAsync("no@x.com")).ReturnsAsync((UserDto?)null);

            var result = await _controller.GetUserByEmail("no@x.com");

            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task CreateUser_NewUser_ReturnsCreatedAtAction()
        {
            var input = new CreateUserDto { Username = "sam", Email = "s@x.com", Auth0Id = "auth0|4" };
            var created = new UserDto { Id = 4, Username = "sam", Email = "s@x.com", Auth0Id = "auth0|4" };
            _serviceMock.Setup(s => s.CreateAsync(input)).ReturnsAsync(created);

            var result = await _controller.CreateUser(input);
            var createdAt = Assert.IsType<CreatedAtActionResult>(result);

            var user = Assert.IsType<UserDto>(createdAt.Value);
            Assert.Equal(4, user.Id);
        }

        [Fact]
        public async Task CreateUser_DuplicateAuth0Id_ReturnsBadRequest()
        {
            var input = new CreateUserDto { Username = "sunil", Email = "suni@x.com", Auth0Id = "auth0|5" };
            _serviceMock.Setup(s => s.CreateAsync(input)).ReturnsAsync((UserDto?)null);

            var result = await _controller.CreateUser(input);

            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public async Task UpdateUser_ExistingUser_ReturnsNoContent()
        {
            _serviceMock.Setup(s => s.UpdateAsync(5, It.IsAny<CreateUserDto>())).ReturnsAsync(true);

            var result = await _controller.UpdateUser(5, new CreateUserDto());

            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task UpdateUser_NotFound_ReturnsNotFound()
        {
            _serviceMock.Setup(s => s.UpdateAsync(6, It.IsAny<CreateUserDto>())).ReturnsAsync(false);

            var result = await _controller.UpdateUser(6, new CreateUserDto());

            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task DeleteUser_ExistingUser_ReturnsNoContent()
        {
            _serviceMock.Setup(s => s.DeleteAsync(7)).ReturnsAsync(true);

            var result = await _controller.DeleteUser(7);

            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task DeleteUser_NotFound_ReturnsNotFound()
        {
            _serviceMock.Setup(s => s.DeleteAsync(8)).ReturnsAsync(false);

            var result = await _controller.DeleteUser(8);

            Assert.IsType<NotFoundResult>(result);
        }
    }
}