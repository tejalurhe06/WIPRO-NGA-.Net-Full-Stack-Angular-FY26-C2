using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShopForHome.API.DTOs;
using ShopForHome.API.Services;
using System.Security.Claims;
using ShopForHome.API.Interfaces;
namespace ShopForHome.API.Controllers
{
    [Authorize(Roles = "Admin")]
    [Route("api/admin/[controller]")]
    [ApiController]
    public class AdminCouponsController : ControllerBase
    {
        private readonly IAdminCouponService _couponService;
        private readonly int _adminId;

        public AdminCouponsController(IAdminCouponService couponService, IHttpContextAccessor httpContextAccessor)
        {
            _couponService = couponService;
            _adminId = int.Parse(httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
        }

        [HttpGet]
        public async Task<IActionResult> GetCoupons()
        {
            var coupons = await _couponService.GetAllCouponsAsync();
            return Ok(coupons);
        }

        [HttpPost]
        public async Task<IActionResult> CreateCoupon(CreateCouponDTO dto)
        {
            try
            {
                var coupon = await _couponService.CreateCouponAsync(dto, _adminId);
                return Ok(coupon);
            }
            catch (ApplicationException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("assign")]
        public async Task<IActionResult> AssignCoupon(AssignCouponDTO dto)
        {
            try
            {
                await _couponService.AssignCouponToUserAsync(dto);
                return Ok(new { message = "Coupon assigned successfully" });
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (ApplicationException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}/deactivate")]
        public async Task<IActionResult> DeactivateCoupon(int id)
        {
            try
            {
                await _couponService.DeactivateCouponAsync(id);
                return Ok(new { message = "Coupon deactivated successfully", couponId = id });
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}