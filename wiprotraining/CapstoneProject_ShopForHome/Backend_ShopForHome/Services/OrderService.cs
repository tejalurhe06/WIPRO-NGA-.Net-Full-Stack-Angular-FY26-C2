using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ShopForHome.API.Data;
using ShopForHome.API.DTOs;
using ShopForHome.API.Interfaces;
namespace ShopForHome.API.Services
{
    public class OrderService : IOrderService
    {
        private readonly ShopForHomeDbContext _context;
        private readonly ILogger<OrderService> _logger;
        private readonly ICouponService _couponService;

        public OrderService(ShopForHomeDbContext context, ILogger<OrderService> logger, ICouponService couponService)
        {
            _context = context;
            _logger = logger;
            _couponService = couponService;
        }

        public async Task<List<OrderDTO>> GetUserOrdersAsync(int userId)
        {
            _logger.LogInformation("Fetching orders for user {UserId}", userId);

            return await _context.Orders
                .Where(o => o.UserId == userId)
                .Include(o => o.OrderItems)
                    .ThenInclude(oi => oi.Product)
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
        }

        public async Task<OrderDTO> GetOrderByIdAsync(int orderId, int userId)
        {
            var order = await _context.Orders
                .Include(o => o.OrderItems)
                    .ThenInclude(oi => oi.Product)
                .Where(o => o.OrderId == orderId && o.UserId == userId)
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
                .FirstOrDefaultAsync();

            if (order == null)
            {
                _logger.LogWarning("Order {OrderId} not found for user {UserId}", orderId, userId);
                throw new KeyNotFoundException("Order not found.");
            }

            return order;
        }

        public async Task<int> CreateOrderAsync(int userId, CreateOrderRequest request)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();

            try
            {
                var cart = await _context.Carts
                    .Include(c => c.CartItems)
                        .ThenInclude(ci => ci.Product)
                    .FirstOrDefaultAsync(c => c.UserId == userId);

                if (cart == null || !cart.CartItems.Any())
                    throw new ApplicationException("Cart is empty");

                var address = await _context.Addresses
                    .FirstOrDefaultAsync(a => a.AddressId == request.AddressId && a.UserId == userId);

                if (address == null)
                    throw new ApplicationException("Invalid address");

                // Calculate total
                decimal total = cart.CartItems.Sum(ci => ci.Product.Price * ci.Quantity);
                decimal discount = 0;

                // Apply coupon using CouponService
                if (!string.IsNullOrEmpty(request.CouponCode))
                {
                    var couponResult = await _couponService.ApplyCouponAsync(userId, request.CouponCode, total);
                    discount = couponResult.Discount;
                }

                decimal finalTotal = total - discount;

                var order = new Models.Order
                {
                    UserId = userId,
                    OrderTotal = finalTotal,
                    OrderStatus = "Pending",
                    OrderDate = DateTime.UtcNow,
                    ShippingAddressId = request.AddressId
                };
                _context.Orders.Add(order);

                foreach (var ci in cart.CartItems)
                {
                    _context.OrderItems.Add(new Models.OrderItem
                    {
                        Order = order,
                        ProductId = ci.ProductId,
                        Quantity = ci.Quantity,
                        UnitPrice = ci.Product.Price
                    });

                    var product = await _context.Products.FindAsync(ci.ProductId);
                    product.StockQuantity -= ci.Quantity;
                }

                _context.CartItems.RemoveRange(cart.CartItems);
                await _context.SaveChangesAsync();

                await transaction.CommitAsync();
                _logger.LogInformation("Order {OrderId} created for user {UserId}", order.OrderId, userId);

                return order.OrderId;
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
        }
    }
}
