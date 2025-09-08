using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ShopForHome.API.Data;
using ShopForHome.API.Interfaces;
namespace ShopForHome.API.Services
{
    public class ReportsService : IReportsService
    {
        private readonly ShopForHomeDbContext _context;
        private readonly ILogger<ReportsService> _logger;

        public ReportsService(ShopForHomeDbContext context, ILogger<ReportsService> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<object> GetSalesReportAsync(DateTime startDate, DateTime endDate)
        {
            var report = await _context.OrderItems
                .Where(oi => oi.Order.OrderDate >= startDate && oi.Order.OrderDate <= endDate)
                .GroupBy(oi => oi.Product.Name)
                .Select(g => new
                {
                    ProductName = g.Key,
                    TotalQuantitySold = g.Sum(oi => oi.Quantity),
                    TotalRevenue = g.Sum(oi => oi.Quantity * oi.UnitPrice)
                })
                .OrderByDescending(r => r.TotalRevenue)
                .ToListAsync();

            var summary = new
            {
                StartDate = startDate,
                EndDate = endDate,
                TotalRevenue = report.Sum(r => r.TotalRevenue),
                TotalItemsSold = report.Sum(r => r.TotalQuantitySold),
                Products = report
            };

            _logger.LogInformation("Generated sales report from {StartDate} to {EndDate}", startDate, endDate);
            return summary;
        }

        public async Task<List<object>> GetLowStockReportAsync()
        {
            var lowStockProducts = await _context.Products
                .Where(p => p.StockQuantity < 10 && p.IsActive)
                .Select(p => new
                {
                    p.ProductId,
                    p.Name,
                    p.StockQuantity,
                    p.Price
                })
                .OrderBy(p => p.StockQuantity)
                .ToListAsync();

            _logger.LogInformation("Fetched low stock report: {Count} products found", lowStockProducts.Count);
            return lowStockProducts.Cast<object>().ToList();
        }
    }
}
