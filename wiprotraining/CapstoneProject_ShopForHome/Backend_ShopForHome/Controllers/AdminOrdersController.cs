using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShopForHome.API.DTOs;
using ShopForHome.API.Services;
using ShopForHome.API.Interfaces;
namespace ShopForHome.API.Controllers
{
    [Authorize(Roles = "Admin")]
    [Route("api/admin/[controller]")]
    [ApiController]
    public class AdminOrdersController : ControllerBase
    {
        private readonly IAdminOrderService _orderService;

        public AdminOrdersController(IAdminOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllOrders([FromQuery] string status = "", [FromQuery] DateTime? startDate = null, [FromQuery] DateTime? endDate = null)
        {
            var orders = await _orderService.GetAllOrdersAsync(status, startDate, endDate);
            return Ok(orders);
        }

        [HttpPut("{id}/status")]
        public async Task<IActionResult> UpdateOrderStatus(int id, [FromBody] string status)
        {
            try
            {
                await _orderService.UpdateOrderStatusAsync(id, status);
                return Ok(new
                {
                    message = $"Order status updated successfully",
                    orderId = id,
                    newStatus = status,
                    updatedAt = DateTime.UtcNow
                });
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpGet("stats")]
        public async Task<IActionResult> GetOrderStats()
        {
            var stats = await _orderService.GetOrderStatsAsync();
            return Ok(stats);
        }
    }
}