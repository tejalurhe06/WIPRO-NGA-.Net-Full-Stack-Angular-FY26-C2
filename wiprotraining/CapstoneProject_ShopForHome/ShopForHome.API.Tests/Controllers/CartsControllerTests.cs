using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using ShopForHome.API.Controllers;
using ShopForHome.API.DTOs;
using ShopForHome.API.Interfaces;
using ShopForHome.API.Tests.Utilities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace ShopForHome.API.Tests.Controllers
{
    public class CartsControllerTests
    {
        private readonly Mock<ICartService> _mockCartService;
        private readonly CartsController _controller;
        private readonly int _testUserId = 1;

        public CartsControllerTests()
        {
            // Mock the interface instead of concrete class
            _mockCartService = new Mock<ICartService>();

            // Create mock HttpContextAccessor with test user
            var httpContextAccessor = TestUtilities.GetMockHttpContextAccessor(_testUserId, "User");

            // Inject mock service and HttpContextAccessor into controller
            _controller = new CartsController(_mockCartService.Object, httpContextAccessor);
        }

        [Fact]
        public async Task GetCart_ReturnsOk_WithCartItems()
        {
            var cartItems = new List<CartItemDTO>
            {
                new CartItemDTO { CartItemId = 1, ProductId = 1, ProductName = "Product 1", Price = 10, Quantity = 2 },
                new CartItemDTO { CartItemId = 2, ProductId = 2, ProductName = "Product 2", Price = 20, Quantity = 1 }
            };

            _mockCartService.Setup(s => s.GetCartAsync(_testUserId))
                .ReturnsAsync(cartItems);

            var result = await _controller.GetCart();

            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnedItems = Assert.IsType<List<CartItemDTO>>(okResult.Value);
            Assert.Equal(2, returnedItems.Count);
        }

        [Fact]
        public async Task AddToCart_ReturnsOk_WhenSuccessful()
        {
            var request = new AddToCartRequest { ProductId = 1, Quantity = 2 };
            var expectedCartItem = new CartItemDTO { ProductId = 1, ProductName = "Test Product", Price = 10, Quantity = 2 };

            _mockCartService.Setup(s => s.AddToCartAsync(_testUserId, request))
                .ReturnsAsync(expectedCartItem);

            var result = await _controller.AddToCart(request);

            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnedItem = Assert.IsType<CartItemDTO>(okResult.Value);
            Assert.Equal("Test Product", returnedItem.ProductName);
            Assert.Equal(2, returnedItem.Quantity);
        }

        [Fact]
        public async Task AddToCart_ReturnsBadRequest_WhenProductNotFound()
        {
            var request = new AddToCartRequest { ProductId = 999, Quantity = 1 };

            _mockCartService.Setup(s => s.AddToCartAsync(_testUserId, request))
                .ThrowsAsync(new ApplicationException("Product not found"));

            var result = await _controller.AddToCart(request);

            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal("Product not found", badRequestResult.Value);
        }

        [Fact]
        public async Task AddToCart_ReturnsBadRequest_WhenNotEnoughStock()
        {
            var request = new AddToCartRequest { ProductId = 1, Quantity = 100 };

            _mockCartService.Setup(s => s.AddToCartAsync(_testUserId, request))
                .ThrowsAsync(new ApplicationException("Not enough stock available"));

            var result = await _controller.AddToCart(request);

            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal("Not enough stock available", badRequestResult.Value);
        }

        [Fact]
        public async Task UpdateCartItem_ReturnsNoContent_WhenSuccessful()
        {
            var request = new UpdateCartItemRequest { Quantity = 5 };

            _mockCartService.Setup(s => s.UpdateCartItemAsync(_testUserId, 1, request.Quantity))
                .Returns(Task.CompletedTask);

            var result = await _controller.UpdateCartItem(1, request);

            Assert.IsType<NoContentResult>(result);
        }


        [Fact]
        public async Task UpdateCartItem_ReturnsBadRequest_WhenCartItemNotFound()
        {
            var request = new UpdateCartItemRequest { Quantity = 5 };

            _mockCartService.Setup(s => s.UpdateCartItemAsync(_testUserId, 999, request.Quantity))
                .ThrowsAsync(new ApplicationException("Cart item not found"));

            var result = await _controller.UpdateCartItem(999, request);

            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal("Cart item not found", badRequestResult.Value);
        }



        [Fact]
        public async Task RemoveFromCart_ReturnsNoContent_WhenSuccessful()
        {
            _mockCartService.Setup(s => s.RemoveCartItemAsync(_testUserId, 1))
                .Returns(Task.CompletedTask);

            var result = await _controller.RemoveFromCart(1);

            Assert.IsType<NoContentResult>(result);  // ✅ instead of OkObjectResult
        }


        [Fact]
        public async Task RemoveFromCart_ReturnsBadRequest_WhenCartItemNotFound()
        {
            _mockCartService.Setup(s => s.RemoveCartItemAsync(_testUserId, 999))
                .ThrowsAsync(new ApplicationException("Cart item not found"));

            var result = await _controller.RemoveFromCart(999);

            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal("Cart item not found", badRequestResult.Value);
        }

        [Fact]
        public async Task GetCartTotal_ReturnsOk_WithTotalAmount()
        {
            _mockCartService.Setup(s => s.GetCartTotalAsync(_testUserId))
                .ReturnsAsync(150.50m);

            var result = await _controller.GetCartTotal();

            var okResult = Assert.IsType<OkObjectResult>(result);
            var total = Assert.IsType<decimal>(okResult.Value);
            Assert.Equal(150.50m, total);
        }
    }
}
