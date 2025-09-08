using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ShopForHome.API.Data;
using ShopForHome.API.DTOs;
using ShopForHome.API.Interfaces;
namespace ShopForHome.API.Services
{
    public class AdminOrderService : IAdminOrderService
    {
        private readonly ShopForHomeDbContext _context;
        private readonly ILogger<AdminOrderService> _logger;

        public AdminOrderService(ShopForHomeDbContext context, ILogger<AdminOrderService> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<List<OrderDTO>> GetAllOrdersAsync(string status = "", DateTime? startDate = null, DateTime? endDate = null)
        {
            _logger.LogInformation("Fetching orders with status='{Status}', startDate={StartDate}, endDate={EndDate}", status, startDate, endDate);

            var query = _context.Orders
                .Include(o => o.User)
                .Include(o => o.OrderItems)
                .ThenInclude(oi => oi.Product)
                .AsQueryable();

            if (!string.IsNullOrEmpty(status))
                query = query.Where(o => o.OrderStatus == status);

            if (startDate.HasValue)
                query = query.Where(o => o.OrderDate >= startDate.Value);

            if (endDate.HasValue)
                query = query.Where(o => o.OrderDate <= endDate.Value);

            var orders = await query
                .Select(o => new OrderDTO
                {
                    OrderId = o.OrderId,
                    OrderDate = o.OrderDate,
                    OrderTotal = o.OrderTotal,
                    OrderStatus = o.OrderStatus,
                    OrderItems = o.OrderItems.Select(oi => new OrderItemDTO
                    {
                        ProductId = oi.ProductId,
                        ProductName = oi.Product.Name,
                        UnitPrice = oi.UnitPrice,
                        Quantity = oi.Quantity
                    }).ToList()
                })
                .OrderByDescending(o => o.OrderDate)
                .ToListAsync();

            _logger.LogInformation("Fetched {Count} orders", orders.Count);

            return orders;
        }

        public async Task UpdateOrderStatusAsync(int orderId, string newStatus)
        {
            var order = await _context.Orders.FindAsync(orderId);
            if (order == null)
            {
                _logger.LogWarning("Order with ID {OrderId} not found", orderId);
                throw new KeyNotFoundException("Order not found.");
            }

            var oldStatus = order.OrderStatus;
            order.OrderStatus = newStatus;

            await _context.SaveChangesAsync();
            _logger.LogInformation("Order {OrderId} status updated from '{OldStatus}' to '{NewStatus}'", orderId, oldStatus, newStatus);
        }

        public async Task<object> GetOrderStatsAsync()
        {
            var stats = new
            {
                TotalOrders = await _context.Orders.CountAsync(),
                PendingOrders = await _context.Orders.CountAsync(o => o.OrderStatus == "Pending"),
                CompletedOrders = await _context.Orders.CountAsync(o => o.OrderStatus == "Completed"),
                TotalRevenue = await _context.Orders.SumAsync(o => o.OrderTotal)
            };

            _logger.LogInformation("Fetched order stats: {@Stats}", stats);

            return stats;
        }
    }
}