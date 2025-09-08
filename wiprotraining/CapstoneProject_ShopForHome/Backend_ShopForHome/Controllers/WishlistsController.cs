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
    public class WishlistsController : ControllerBase
    {
        private readonly IWishlistService _wishlistService;
        private readonly int _userId;

        public WishlistsController(IWishlistService wishlistService, IHttpContextAccessor httpContextAccessor)
        {
            _wishlistService = wishlistService;
            _userId = int.Parse(httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductDTO>>> GetWishlist()
        {
            var items = await _wishlistService.GetWishlistAsync(_userId);
            return Ok(items);
        }

        [HttpPost("{productId}")]
        public async Task<IActionResult> AddToWishlist(int productId)
        {
            try
            {
                await _wishlistService.AddToWishlistAsync(_userId, productId);
                return Ok(new { message = "Product added to wishlist" });
            }
            catch (ApplicationException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{productId}")]
        public async Task<IActionResult> RemoveFromWishlist(int productId)
        {
            try
            {
                await _wishlistService.RemoveFromWishlistAsync(_userId, productId);
                return Ok(new { message = "Product removed from wishlist" });
            }
            catch (ApplicationException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("check/{productId}")]
        public async Task<ActionResult<bool>> IsInWishlist(int productId)
        {
            var exists = await _wishlistService.IsInWishlistAsync(_userId, productId);
            return Ok(exists);
        }
    }
}
