using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ShopForHome.API.Data;
using ShopForHome.API.DTOs;
using ShopForHome.API.Interfaces;
namespace ShopForHome.API.Services
{
    public class WishlistService : IWishlistService
    {
        private readonly ShopForHomeDbContext _context;
        private readonly ILogger<WishlistService> _logger;

        public WishlistService(ShopForHomeDbContext context, ILogger<WishlistService> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<List<ProductDTO>> GetWishlistAsync(int userId)
        {
            var wishlistItems = await _context.Wishlists
                .Where(w => w.UserId == userId)
                .Include(w => w.Product)
                    .ThenInclude(p => p.Category)
                .Select(w => new ProductDTO
                {
                    ProductId = w.Product.ProductId,
                    Name = w.Product.Name,
                    Description = w.Product.Description,
                    Price = w.Product.Price,
                    StockQuantity = w.Product.StockQuantity,
                    ImageUrl = w.Product.ImageUrl,
                    AverageRating = w.Product.AverageRating,
                    CategoryId = w.Product.CategoryId,
                    CategoryName = w.Product.Category.CategoryName
                })
                .ToListAsync();

            _logger.LogInformation("User {UserId} retrieved wishlist with {Count} items", userId, wishlistItems.Count);
            return wishlistItems;
        }

        public async Task AddToWishlistAsync(int userId, int productId)
        {
            var product = await _context.Products.FirstOrDefaultAsync(p => p.ProductId == productId && p.IsActive);
            if (product == null) throw new ApplicationException("Product not found");

            var existingItem = await _context.Wishlists.FirstOrDefaultAsync(w => w.UserId == userId && w.ProductId == productId);
            if (existingItem != null) throw new ApplicationException("Product already in wishlist");

            _context.Wishlists.Add(new Models.Wishlist
            {
                UserId = userId,
                ProductId = productId,
                AddedAt = DateTime.UtcNow
            });

            await _context.SaveChangesAsync();
            _logger.LogInformation("User {UserId} added product {ProductId} to wishlist", userId, productId);
        }

        public async Task RemoveFromWishlistAsync(int userId, int productId)
        {
            var wishlistItem = await _context.Wishlists.FirstOrDefaultAsync(w => w.UserId == userId && w.ProductId == productId);
            if (wishlistItem == null) throw new ApplicationException("Product not found in wishlist");

            _context.Wishlists.Remove(wishlistItem);
            await _context.SaveChangesAsync();
            _logger.LogInformation("User {UserId} removed product {ProductId} from wishlist", userId, productId);
        }

        public async Task<bool> IsInWishlistAsync(int userId, int productId)
        {
            var exists = await _context.Wishlists.AnyAsync(w => w.UserId == userId && w.ProductId == productId);
            _logger.LogInformation("Checked wishlist for user {UserId} and product {ProductId}: {Exists}", userId, productId, exists);
            return exists;
        }
    }
}
