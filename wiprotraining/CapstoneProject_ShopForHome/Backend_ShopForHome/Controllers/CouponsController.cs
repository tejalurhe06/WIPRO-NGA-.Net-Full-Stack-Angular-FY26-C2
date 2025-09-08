using Microsoft.AspNetCore.Mvc;
using ShopForHome.API.Interfaces;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

namespace ShopForHome.API.Controllers
{
    [Authorize] // User must be logged in
    [Route("api/[controller]")]
    [ApiController]
    public class CouponsController : ControllerBase
    {
        private readonly ICouponService _couponService;
        private readonly int _userId;

        public CouponsController(ICouponService couponService, IHttpContextAccessor httpContextAccessor)
        {
            _couponService = couponService;
            _userId = int.Parse(httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
        }

        [HttpGet]
        public async Task<IActionResult> GetUserCoupons()
        {
            var coupons = await _couponService.GetUserCouponsAsync(_userId);
            return Ok(coupons);
        }

        [HttpPost("apply/{code}")]
        public async Task<IActionResult> ApplyCoupon(string code, [FromBody] decimal cartTotal)
        {
            try
            {
                var result = await _couponService.ApplyCouponAsync(_userId, code, cartTotal);
                return Ok(new
                {
                    Discount = result.Discount,
                    FinalTotal = result.FinalTotal,
                    CouponCode = result.Code
                });
            }
            catch (ApplicationException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
