using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using ShopForHome.API.Controllers;
using ShopForHome.API.Models;
using ShopForHome.API.Services;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Xunit;
using ShopForHome.API.Interfaces;

namespace ShopForHome.API.Tests.Controllers
{
    public class CategoriesControllerTests
    {
        private readonly Mock<ICategoryService> _mockCategoryService;
        private readonly CategoriesController _controller;

        public CategoriesControllerTests()
        {
            // Mock the interface instead of concrete class
            _mockCategoryService = new Mock<ICategoryService>();

            // Create controller with mock service
            _controller = new CategoriesController(_mockCategoryService.Object);
        }

        [Fact]
        public async Task GetCategories_ReturnsOk_WithCategories()
        {
            var categories = new List<Category>
            {
                new Category { CategoryId = 1, CategoryName = "Electronics", Description = "Electronic devices" },
                new Category { CategoryId = 2, CategoryName = "Clothing", Description = "Apparel and clothing" }
            };

            _mockCategoryService.Setup(s => s.GetCategoriesAsync()).ReturnsAsync(categories);

            var result = await _controller.GetCategories();

            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnedCategories = Assert.IsType<List<Category>>(okResult.Value);
            Assert.Equal(2, returnedCategories.Count);
        }

        [Fact]
        public async Task GetCategory_ReturnsOk_WhenCategoryExists()
        {
            var category = new Category { CategoryId = 1, CategoryName = "Electronics" };

            _mockCategoryService.Setup(s => s.GetCategoryByIdAsync(1)).ReturnsAsync(category);

            var result = await _controller.GetCategory(1);

            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnedCategory = Assert.IsType<Category>(okResult.Value);
            Assert.Equal("Electronics", returnedCategory.CategoryName);
        }

        [Fact]
        public async Task GetCategory_ReturnsNotFound_WhenCategoryDoesNotExist()
        {
            _mockCategoryService.Setup(s => s.GetCategoryByIdAsync(999))
                .ThrowsAsync(new ApplicationException("Category not found"));

            var result = await _controller.GetCategory(999);

            var notFoundResult = Assert.IsType<NotFoundObjectResult>(result);
            Assert.Equal("Category not found", notFoundResult.Value);
        }

        [Fact]
        public async Task CreateCategory_ReturnsCreated_WhenAdminUser()
        {
            var adminClaims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, "1"),
                new Claim(ClaimTypes.Role, "Admin")
            };
            var httpContext = new DefaultHttpContext { User = new ClaimsPrincipal(new ClaimsIdentity(adminClaims, "TestAuth")) };
            _controller.ControllerContext = new ControllerContext { HttpContext = httpContext };

            var newCategory = new Category { CategoryName = "Books", Description = "Books and literature" };
            var createdCategory = new Category { CategoryId = 3, CategoryName = "Books", Description = "Books and literature" };

            _mockCategoryService.Setup(s => s.CreateCategoryAsync(newCategory)).ReturnsAsync(createdCategory);

            var result = await _controller.CreateCategory(newCategory);

            var createdAtResult = Assert.IsType<CreatedAtActionResult>(result);
            Assert.Equal(nameof(CategoriesController.GetCategory), createdAtResult.ActionName);
            Assert.Equal(3, createdAtResult.RouteValues["id"]);
            var returnedCategory = Assert.IsType<Category>(createdAtResult.Value);
            Assert.Equal("Books", returnedCategory.CategoryName);
        }

        [Fact]
        public async Task UpdateCategory_ReturnsOk_WhenSuccessful()
        {
            var adminClaims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, "1"),
                new Claim(ClaimTypes.Role, "Admin")
            };
            var httpContext = new DefaultHttpContext { User = new ClaimsPrincipal(new ClaimsIdentity(adminClaims, "TestAuth")) };
            _controller.ControllerContext = new ControllerContext { HttpContext = httpContext };

            var updatedCategory = new Category { CategoryId = 1, CategoryName = "Updated Electronics" };

            _mockCategoryService.Setup(s => s.UpdateCategoryAsync(1, updatedCategory)).Returns(Task.CompletedTask);

            var result = await _controller.UpdateCategory(1, updatedCategory);

            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal("Category updated successfully.", okResult.Value);
        }

        [Fact]
        public async Task UpdateCategory_ReturnsBadRequest_WhenIdMismatch()
        {
            var adminClaims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, "1"),
                new Claim(ClaimTypes.Role, "Admin")
            };
            var httpContext = new DefaultHttpContext { User = new ClaimsPrincipal(new ClaimsIdentity(adminClaims, "TestAuth")) };
            _controller.ControllerContext = new ControllerContext { HttpContext = httpContext };

            var updatedCategory = new Category { CategoryId = 1, CategoryName = "Updated" };

            _mockCategoryService.Setup(s => s.UpdateCategoryAsync(2, updatedCategory))
                .ThrowsAsync(new ApplicationException("Category ID mismatch"));

            var result = await _controller.UpdateCategory(2, updatedCategory);

            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal("Category ID mismatch", badRequestResult.Value);
        }

        [Fact]
        public async Task DeleteCategory_ReturnsOk_WhenSuccessful()
        {
            var adminClaims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, "1"),
                new Claim(ClaimTypes.Role, "Admin")
            };
            var httpContext = new DefaultHttpContext { User = new ClaimsPrincipal(new ClaimsIdentity(adminClaims, "TestAuth")) };
            _controller.ControllerContext = new ControllerContext { HttpContext = httpContext };

            _mockCategoryService.Setup(s => s.DeleteCategoryAsync(1)).Returns(Task.CompletedTask);

            var result = await _controller.DeleteCategory(1);

            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal("Category deleted successfully.", okResult.Value);
        }

        [Fact]
        public async Task DeleteCategory_ReturnsBadRequest_WhenCategoryNotFound()
        {
            var adminClaims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, "1"),
                new Claim(ClaimTypes.Role, "Admin")
            };
            var httpContext = new DefaultHttpContext { User = new ClaimsPrincipal(new ClaimsIdentity(adminClaims, "TestAuth")) };
            _controller.ControllerContext = new ControllerContext { HttpContext = httpContext };

            _mockCategoryService.Setup(s => s.DeleteCategoryAsync(999))
                .ThrowsAsync(new ApplicationException("Category not found"));

            var result = await _controller.DeleteCategory(999);

            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal("Category not found", badRequestResult.Value);
        }
    }
}
