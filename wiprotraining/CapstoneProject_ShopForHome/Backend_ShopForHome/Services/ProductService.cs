using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ShopForHome.API.Data;
using ShopForHome.API.DTOs;
using ShopForHome.API.Models;
using ShopForHome.API.Interfaces;
namespace ShopForHome.API.Services
{
    public class ProductService : IProductService
    {
        private readonly ShopForHomeDbContext _context;
        private readonly ILogger<ProductService> _logger;

        public ProductService(ShopForHomeDbContext context, ILogger<ProductService> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<List<ProductDTO>> GetProductsAsync(int? categoryId, decimal? minPrice, decimal? maxPrice)
        {
            _logger.LogInformation("Fetching products with filters CategoryId={CategoryId}, MinPrice={MinPrice}, MaxPrice={MaxPrice}", categoryId, minPrice, maxPrice);

            var query = _context.Products
                .Include(p => p.Category)
                .Where(p => p.IsActive)
                .AsQueryable();

            if (categoryId.HasValue && categoryId > 0)
                query = query.Where(p => p.CategoryId == categoryId);

            if (minPrice.HasValue)
                query = query.Where(p => p.Price >= minPrice);

            if (maxPrice.HasValue)
                query = query.Where(p => p.Price <= maxPrice);

            var products = await query
                .Select(p => new ProductDTO
                {
                    ProductId = p.ProductId,
                    Name = p.Name,
                    Description = p.Description,
                    Price = p.Price,
                    StockQuantity = p.StockQuantity,
                    ImageUrl = p.ImageUrl,
                    AverageRating = p.AverageRating,
                    CategoryId = p.CategoryId,
                    CategoryName = p.Category.CategoryName
                })
                .ToListAsync();

            _logger.LogInformation("Fetched {Count} products", products.Count);
            return products;
        }

        public async Task<ProductDTO> GetProductByIdAsync(int id)
        {
            var product = await _context.Products
                .Include(p => p.Category)
                .Where(p => p.ProductId == id && p.IsActive)
                .Select(p => new ProductDTO
                {
                    ProductId = p.ProductId,
                    Name = p.Name,
                    Description = p.Description,
                    Price = p.Price,
                    StockQuantity = p.StockQuantity,
                    ImageUrl = p.ImageUrl,
                    AverageRating = p.AverageRating,
                    CategoryId = p.CategoryId,
                    CategoryName = p.Category.CategoryName
                })
                .FirstOrDefaultAsync();

            if (product == null)
            {
                _logger.LogWarning("Product {ProductId} not found", id);
                throw new KeyNotFoundException("Product not found.");
            }

            return product;
        }

        public async Task<int> CreateProductAsync(CreateProductDTO dto)
        {
            var product = new Product
            {
                Name = dto.Name,
                Description = dto.Description,
                Price = dto.Price,
                StockQuantity = dto.StockQuantity,
                ImageUrl = dto.ImageUrl,
                CategoryId = dto.CategoryId,
                CreatedAt = DateTime.UtcNow,
                IsActive = true
            };

            _context.Products.Add(product);
            await _context.SaveChangesAsync();

            _logger.LogInformation("Product {ProductId} created", product.ProductId);
            return product.ProductId;
        }

        public async Task UpdateProductAsync(UpdateProductDTO dto)
        {
            var product = await _context.Products.FindAsync(dto.ProductId);
            if (product == null)
            {
                _logger.LogWarning("Product {ProductId} not found for update", dto.ProductId);
                throw new KeyNotFoundException("Product not found.");
            }

            product.Name = dto.Name;
            product.Description = dto.Description;
            product.Price = dto.Price;
            product.StockQuantity = dto.StockQuantity;
            product.ImageUrl = dto.ImageUrl;
            product.CategoryId = dto.CategoryId;

            _context.Entry(product).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            _logger.LogInformation("Product {ProductId} updated", dto.ProductId);
        }

        public async Task DeleteProductAsync(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                _logger.LogWarning("Product {ProductId} not found for deletion", id);
                throw new KeyNotFoundException("Product not found.");
            }

            product.IsActive = false; // Soft delete
            await _context.SaveChangesAsync();

            _logger.LogInformation("Product {ProductId} soft-deleted", id);
        }
    }
}
