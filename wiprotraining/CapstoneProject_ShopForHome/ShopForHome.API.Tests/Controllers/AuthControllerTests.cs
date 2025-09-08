using Microsoft.AspNetCore.Mvc;
using Moq;
using ShopForHome.API.Controllers;
using ShopForHome.API.DTOs;
using ShopForHome.API.Interfaces;
using System;
using System.Threading.Tasks;
using Xunit;

namespace ShopForHome.API.Tests.Controllers
{
    public class AuthControllerTests
    {
        private readonly Mock<IAuthService> _mockAuthService;
        private readonly AuthController _controller;

        public AuthControllerTests()
        {
            // Mock the interface instead of concrete class
            _mockAuthService = new Mock<IAuthService>();

            // Inject mock into controller
            _controller = new AuthController(_mockAuthService.Object);
        }

        [Fact]
        public async Task Register_ReturnsOk_WhenRegistrationSuccessful()
        {
            // Arrange
            var createUserDto = new CreateUserDTO
            {
                Email = "test@example.com",
                Password = "Password123",
                FirstName = "Test",
                LastName = "User",
                UserType = 1
            };

            var expectedUserDto = new UserDTO
            {
                UserId = 1,
                Email = "test@example.com",
                FirstName = "Test",
                LastName = "User",
                UserType = 1
            };

            _mockAuthService.Setup(s => s.RegisterAsync(createUserDto))
                .ReturnsAsync(expectedUserDto);

            // Act
            var result = await _controller.Register(createUserDto);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnedUser = Assert.IsType<UserDTO>(okResult.Value);
            Assert.Equal("test@example.com", returnedUser.Email);
        }

        [Fact]
        public async Task Register_ReturnsBadRequest_WhenEmailAlreadyExists()
        {
            // Arrange
            var createUserDto = new CreateUserDTO
            {
                Email = "existing@example.com",
                Password = "Password123",
                FirstName = "Test",
                LastName = "User",
                UserType = 1
            };

            _mockAuthService.Setup(s => s.RegisterAsync(createUserDto))
                .ThrowsAsync(new ApplicationException("Email already exists"));

            // Act
            var result = await _controller.Register(createUserDto);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal("Email already exists", badRequestResult.Value);
        }

        [Fact]
        public async Task Login_ReturnsOk_WhenCredentialsValid()
        {
            // Arrange
            var loginRequest = new LoginRequest
            {
                Email = "test@example.com",
                Password = "Password123"
            };

            var expectedResponse = new LoginResponse
            {
                Token = "fake-jwt-token",
                User = new UserDTO
                {
                    UserId = 1,
                    Email = "test@example.com",
                    FirstName = "Test",
                    LastName = "User",
                    UserType = 1
                }
            };

            _mockAuthService.Setup(s => s.LoginAsync(loginRequest))
                .ReturnsAsync(expectedResponse);

            // Act
            var result = await _controller.Login(loginRequest);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var loginResponse = Assert.IsType<LoginResponse>(okResult.Value);
            Assert.Equal("fake-jwt-token", loginResponse.Token);
            Assert.Equal("test@example.com", loginResponse.User.Email);
        }

        [Fact]
        public async Task Login_ReturnsUnauthorized_WhenCredentialsInvalid()
        {
            // Arrange
            var loginRequest = new LoginRequest
            {
                Email = "test@example.com",
                Password = "WrongPassword"
            };

            _mockAuthService.Setup(s => s.LoginAsync(loginRequest))
                .ThrowsAsync(new ApplicationException("Invalid credentials"));

            // Act
            var result = await _controller.Login(loginRequest);

            // Assert
            var unauthorizedResult = Assert.IsType<UnauthorizedObjectResult>(result);
            Assert.Equal("Invalid credentials", unauthorizedResult.Value);
        }

        [Fact]
        public async Task Login_ReturnsUnauthorized_WhenUserNotFound()
        {
            // Arrange
            var loginRequest = new LoginRequest
            {
                Email = "nonexistent@example.com",
                Password = "Password123"
            };

            _mockAuthService.Setup(s => s.LoginAsync(loginRequest))
                .ThrowsAsync(new ApplicationException("Invalid credentials"));

            // Act
            var result = await _controller.Login(loginRequest);

            // Assert
            var unauthorizedResult = Assert.IsType<UnauthorizedObjectResult>(result);
            Assert.Equal("Invalid credentials", unauthorizedResult.Value);
        }
    }
}
