using Microsoft.AspNetCore.Mvc;
using ShopForHome.API.DTOs;
using ShopForHome.API.Services;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using ShopForHome.API.Interfaces;
namespace ShopForHome.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class CartsController : ControllerBase
    {
        private readonly ICartService _cartService;
        private readonly int _userId;

        public CartsController(ICartService cartService, IHttpContextAccessor httpContextAccessor)
        {
            _cartService = cartService;
            _userId = int.Parse(httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
        }

        [HttpGet]
        public async Task<IActionResult> GetCart()
        {
            var cartItems = await _cartService.GetCartAsync(_userId);
            return Ok(cartItems);
        }

        [HttpPost]
        public async Task<IActionResult> AddToCart(AddToCartRequest request)
        {
            try
            {
                var cartItem = await _cartService.AddToCartAsync(_userId, request);
                return Ok(cartItem);
            }
            catch (ApplicationException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{cartItemId}")]
        public async Task<IActionResult> UpdateCartItem(int cartItemId, UpdateCartItemRequest request)
        {
            try
            {
                await _cartService.UpdateCartItemAsync(_userId, cartItemId, request.Quantity);
                return NoContent();
            }
            catch (ApplicationException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{cartItemId}")]
        public async Task<IActionResult> RemoveFromCart(int cartItemId)
        {
            try
            {
                await _cartService.RemoveCartItemAsync(_userId, cartItemId);
                return NoContent();
            }
            catch (ApplicationException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("total")]
        public async Task<IActionResult> GetCartTotal()
        {
            var total = await _cartService.GetCartTotalAsync(_userId);
            return Ok(total);
        }
    }
}
