using Microsoft.AspNetCore.Mvc;
using Moq;
using MotoRent.Application.Interfaces.Service;
using MotoRent.Application.Services;
using MotoRent.Controllers;
using MotoRent.Domain.Enums;
using MotoRent.DTOs;
using MotoRent.Services;
using NSubstitute;

namespace MotoRent.Tests
{
    public class UserControllerTests
    {
        private readonly Mock<IUserService> _serviceMock;
        private readonly UserController _controller;

        public UserControllerTests()
        {
            var mockUserRepository = new Mock<IUserRepository>();
            var mockTokenService = new Mock<ITokenService>();
            mockTokenService.Setup(x => x.GenerateToken(It.IsAny<Guid>(), It.IsAny<string>(), It.IsAny<string>()))
                            .Returns("fake-token");
            _serviceMock = new Mock<IUserService>();
            _controller = new UserController(_serviceMock.Object);
        }

        [Fact]
        public async Task Create_ShouldReturnCreatedResult_WhenUserIsCreated()
        {
            // Arrange
            var createUserDto = new CreateUserDto
            {
                Name = "John Doe",
                Email = "john@example.com",
                Password = "password123",
                Role = Domain.Enums.UserRole.Admin // Assuming 1 is a valid role
            };

            var userResponse = new UserResponseDto
            {
                Id = Guid.NewGuid(),
                Name = "John Doe",
                Email = "john@example.com",
                Token = "KJFKALGJALlkflkdgldçdkgçldagkç123456"
            };

            _serviceMock.Setup(s => s.CreateAsync(createUserDto)).ReturnsAsync(userResponse);

            // Act
            var result = await _controller.Create(createUserDto);

            // Assert
            var createdResult = Assert.IsType<CreatedAtActionResult>(result);
            Assert.Equal(201, createdResult.StatusCode);
            Assert.Equal("GetById", createdResult.ActionName);
            Assert.Equal(userResponse, createdResult.Value);
        }

        [Fact]
        public async Task Login_ShouldReturnOkResult_WithValidUserResponseDto()
        {
            // Arrange
            var loginDto = new LoginDto { Email = "john@example.com", Password = "password123" };

            var userResponseDto = new UserResponseDto
            {
                Id = Guid.NewGuid(),
                Name = "John Doe",
                Email = "john@example.com",
                Role = UserRole.Admin,
                Token = "valid-token"
            };

            _serviceMock.Setup(s => s.LoginAsync(loginDto.Email, loginDto.Password))
                        .ReturnsAsync(userResponseDto);

            // Act
            var result = await _controller.Login(loginDto);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var response = Assert.IsType<UserResponseDto>(okResult.Value);

            Assert.Equal("John Doe", response.Name);
            Assert.Equal("john@example.com", response.Email);
            Assert.Equal(UserRole.Admin, response.Role);
            Assert.Equal("valid-token", response.Token);
        }

        [Fact]
        public async Task Login_ShouldReturnUnauthorized_WhenCredentialsAreInvalid()
        {
            // Arrange
            var loginDto = new LoginDto { Email = "john@example.com", Password = "wrongpassword" };
            _serviceMock.Setup(s => s.LoginAsync(loginDto.Email, loginDto.Password)).ThrowsAsync(new UnauthorizedAccessException());

            // Act
            var result = await _controller.Login(loginDto);

            // Assert
            var unauthorizedResult = Assert.IsType<UnauthorizedObjectResult>(result);
            Assert.Equal("Credenciais inválidas.", unauthorizedResult.Value);
        }

        [Fact]
        public async Task GetAll_ShouldReturnOkResult_WhenUsersExist()
        {
            // Arrange
            var userList = new List<UserResponseDto>
            {
                new() { Id = Guid.NewGuid(), Name = "John Doe", Email = "john@example.com", Token = "KJFKALGJALlkflkdgldçdkgçldagkç123456"},
                new() { Id = Guid.NewGuid(), Name = "Jane Smith", Email = "jane@example.com", Token = "KJFKALGJALlkflkdgldçdkgçldagkç123456" }
            };

            // Configuração do mock para retornar um Task com a lista de usuários
            _serviceMock.Setup(s => s.GetAllAsync()).ReturnsAsync(userList);

            // Act
            var result = await _controller.GetAll();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnedUsers = Assert.IsAssignableFrom<IEnumerable<UserResponseDto>>(okResult.Value);
            Assert.Equal(userList.Count, returnedUsers.Count());
        }


        [Fact]
        public async Task GetById_ShouldReturnNotFound_WhenUserDoesNotExist()
        {
            // Arrange
            var userId = Guid.NewGuid(); // Usamos um ID de usuário que não existirá no banco

            // Mock do serviço GetByIdAsync retornando null, indicando que o usuário não existe
            _serviceMock.Setup(s => s.GetByIdAsync(userId)).ReturnsAsync((UserResponseDto)null);

            // Act
            var result = await _controller.GetById(userId);

            // Assert
            var notFoundResult = Assert.IsType<NotFoundResult>(result);
            Assert.Equal(404, notFoundResult.StatusCode); // Verifica o código de status 404
        }

        [Fact]
        public async Task Update_ShouldReturnNoContent_WhenUserIsUpdated()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var updateUserDto = new UpdateUserDto { Name = "Updated Name", Role = Domain.Enums.UserRole.Admin };
            _serviceMock.Setup(s => s.UpdateAsync(userId, updateUserDto)).Returns(Task.CompletedTask);

            // Act
            var result = await _controller.Update(userId, updateUserDto);

            // Assert
            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task Delete_ShouldReturnNoContent_WhenUserIsDeleted()
        {
            // Arrange
            var userId = Guid.NewGuid();
            _serviceMock.Setup(s => s.DeleteAsync(userId)).Returns(Task.CompletedTask);

            // Act
            var result = await _controller.Delete(userId);

            // Assert
            Assert.IsType<NoContentResult>(result);
        }
    }
}
