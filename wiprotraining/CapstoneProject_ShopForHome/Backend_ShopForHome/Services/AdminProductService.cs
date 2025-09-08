using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ShopForHome.API.Data;
using ShopForHome.API.DTOs;
using ShopForHome.API.Models;
using ShopForHome.API.Interfaces;
namespace ShopForHome.API.Services
{
    public class AdminProductService : IAdminProductService
    {
        private readonly ShopForHomeDbContext _context;
        private readonly ILogger<AdminProductService> _logger;

        public AdminProductService(ShopForHomeDbContext context, ILogger<AdminProductService> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<List<AdminProductDTO>> GetAllProductsAsync()
        {
            _logger.LogInformation("Fetching all products (including inactive)");

            return await _context.Products
                .Include(p => p.Category)
                .Select(p => new AdminProductDTO
                {
                    ProductId = p.ProductId,
                    Name = p.Name,
                    Description = p.Description,
                    Price = p.Price,
                    StockQuantity = p.StockQuantity,
                    ImageUrl = p.ImageUrl,
                    AverageRating = p.AverageRating,
                    CategoryId = p.CategoryId,
                    CategoryName = p.Category.CategoryName,
                    IsActive = p.IsActive,
                    CreatedAt = p.CreatedAt
                })
                .OrderByDescending(p => p.CreatedAt)
                .ToListAsync();
        }

        public async Task<List<ProductDTO>> GetLowStockProductsAsync()
        {
            _logger.LogInformation("Fetching low stock products (<10)");

            return await _context.Products
                .Include(p => p.Category)
                .Where(p => p.StockQuantity < 10 && p.IsActive)
                .Select(p => new ProductDTO
                {
                    ProductId = p.ProductId,
                    Name = p.Name,
                    Price = p.Price,
                    StockQuantity = p.StockQuantity,
                    ImageUrl = p.ImageUrl,
                    CategoryName = p.Category.CategoryName
                })
                .OrderBy(p => p.StockQuantity)
                .ToListAsync();
        }

        public async Task<object> BulkUploadProductsAsync(List<CreateProductDTO> products)
        {
            var errors = new List<string>();
            var successCount = 0;

            foreach (var productDto in products)
            {
                try
                {
                    var categoryExists = await _context.Categories
                        .AnyAsync(c => c.CategoryId == productDto.CategoryId);

                    if (!categoryExists)
                    {
                        errors.Add($"Invalid CategoryId {productDto.CategoryId} for product: {productDto.Name}");
                        continue;
                    }

                    var product = new Product
                    {
                        Name = productDto.Name,
                        Description = productDto.Description,
                        Price = productDto.Price,
                        StockQuantity = productDto.StockQuantity,
                        ImageUrl = productDto.ImageUrl,
                        CategoryId = productDto.CategoryId,
                        CreatedAt = DateTime.UtcNow,
                        IsActive = true
                    };

                    _context.Products.Add(product);
                    successCount++;
                }
                catch (Exception ex)
                {
                    errors.Add($"Error processing product {productDto.Name}: {ex.Message}");
                }
            }

            await _context.SaveChangesAsync();

            _logger.LogInformation("Bulk upload completed: {SuccessCount} successful, {ErrorCount} errors", successCount, errors.Count);

            return new
            {
                SuccessCount = successCount,
                ErrorCount = errors.Count,
                Errors = errors
            };
        }

        public async Task UpdateStockAsync(int productId, int newStockQuantity)
        {
            if (newStockQuantity < 0)
            {
                throw new ArgumentException("Stock quantity cannot be negative");
            }

            var product = await _context.Products.FindAsync(productId);
            if (product == null)
            {
                throw new KeyNotFoundException("Product not found");
            }

            product.StockQuantity = newStockQuantity;
            await _context.SaveChangesAsync();

            _logger.LogInformation("Updated stock for product {ProductId} to {Stock}", productId, newStockQuantity);
        }

        public async Task ToggleProductStatusAsync(int productId)
        {
            var product = await _context.Products.FindAsync(productId);
            if (product == null)
                throw new KeyNotFoundException("Product not found");

            product.IsActive = !product.IsActive;
            await _context.SaveChangesAsync();

            _logger.LogInformation("Product {ProductId} status toggled to {IsActive}", productId, product.IsActive);
        }

        public async Task<object> GetProductStatsAsync()
        {
            var stats = new
            {
                TotalProducts = await _context.Products.CountAsync(),
                ActiveProducts = await _context.Products.CountAsync(p => p.IsActive),
                OutOfStockProducts = await _context.Products.CountAsync(p => p.StockQuantity == 0 && p.IsActive),
                LowStockProducts = await _context.Products.CountAsync(p => p.StockQuantity < 10 && p.StockQuantity > 0 && p.IsActive),
                TotalCategories = await _context.Categories.CountAsync()
            };

            _logger.LogInformation("Fetched product stats: {@Stats}", stats);

            return stats;
        }

        public async Task UpdateProductPricesAsync(decimal percentageChange)
        {
            if (percentageChange == 0)
                throw new ArgumentException("Percentage change cannot be zero");

            var products = await _context.Products
                .Where(p => p.IsActive)
                .ToListAsync();

            foreach (var product in products)
            {
                var newPrice = product.Price * (1 + percentageChange / 100);
                product.Price = Math.Round(newPrice, 2);
            }

            await _context.SaveChangesAsync();

            _logger.LogInformation("Updated prices by {PercentageChange}% for {Count} products", percentageChange, products.Count);
        }
    }
}
