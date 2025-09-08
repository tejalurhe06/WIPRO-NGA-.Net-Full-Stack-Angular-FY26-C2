using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ShopForHome.API.Data;
using ShopForHome.API.DTOs;
using ShopForHome.API.Models;
using ShopForHome.API.Interfaces;
namespace ShopForHome.API.Services
{
    public class CartService : ICartService
    {
        private readonly ShopForHomeDbContext _context;
        private readonly ILogger<CartService> _logger;

        public CartService(ShopForHomeDbContext context, ILogger<CartService> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<List<CartItemDTO>> GetCartAsync(int userId)
        {
            return await _context.CartItems
                .Where(ci => ci.Cart.UserId == userId)
                .Include(ci => ci.Product)
                .Select(ci => new CartItemDTO
                {
                    CartItemId = ci.CartItemId,
                    ProductId = ci.ProductId,
                    ProductName = ci.Product.Name,
                    Price = ci.Product.Price,
                    ImageUrl = ci.Product.ImageUrl,
                    Quantity = ci.Quantity
                })
                .ToListAsync();
        }

        public async Task<CartItemDTO> AddToCartAsync(int userId, AddToCartRequest request)
        {
            var cart = await _context.Carts.FirstOrDefaultAsync(c => c.UserId == userId);
            if (cart == null) throw new ApplicationException("Cart not found");

            var product = await _context.Products.FirstOrDefaultAsync(p => p.ProductId == request.ProductId && p.IsActive);
            if (product == null) throw new ApplicationException("Product not found");
            if (product.StockQuantity < request.Quantity) throw new ApplicationException("Not enough stock available");

            var existingCartItem = await _context.CartItems.FirstOrDefaultAsync(ci => ci.CartId == cart.CartId && ci.ProductId == request.ProductId);

            if (existingCartItem != null)
            {
                existingCartItem.Quantity += request.Quantity;
            }
            else
            {
                var cartItem = new CartItem
                {
                    CartId = cart.CartId,
                    ProductId = request.ProductId,
                    Quantity = request.Quantity,
                    AddedAt = DateTime.UtcNow
                };
                _context.CartItems.Add(cartItem);
            }

            await _context.SaveChangesAsync();

            _logger.LogInformation("User {UserId} added product {ProductId} to cart", userId, request.ProductId);

            return new CartItemDTO
            {
                ProductId = product.ProductId,
                ProductName = product.Name,
                Price = product.Price,
                ImageUrl = product.ImageUrl,
                Quantity = existingCartItem?.Quantity ?? request.Quantity
            };
        }

        public async Task UpdateCartItemAsync(int userId, int cartItemId, int newQuantity)
        {
            var cartItem = await _context.CartItems
                .Include(ci => ci.Cart)
                .FirstOrDefaultAsync(ci => ci.CartItemId == cartItemId && ci.Cart.UserId == userId);

            if (cartItem == null) throw new ApplicationException("Cart item not found");

            var product = await _context.Products.FindAsync(cartItem.ProductId);
            if (product.StockQuantity < newQuantity) throw new ApplicationException("Not enough stock available");

            cartItem.Quantity = newQuantity;
            await _context.SaveChangesAsync();

            _logger.LogInformation("User {UserId} updated cart item {CartItemId} quantity to {Quantity}", userId, cartItemId, newQuantity);
        }

        public async Task RemoveCartItemAsync(int userId, int cartItemId)
        {
            var cartItem = await _context.CartItems
                .Include(ci => ci.Cart)
                .FirstOrDefaultAsync(ci => ci.CartItemId == cartItemId && ci.Cart.UserId == userId);

            if (cartItem == null) throw new ApplicationException("Cart item not found");

            _context.CartItems.Remove(cartItem);
            await _context.SaveChangesAsync();

            _logger.LogInformation("User {UserId} removed cart item {CartItemId}", userId, cartItemId);
        }

        public async Task<decimal> GetCartTotalAsync(int userId)
        {
            return await _context.CartItems
                .Where(ci => ci.Cart.UserId == userId)
                .Include(ci => ci.Product)
                .SumAsync(ci => ci.Product.Price * ci.Quantity);
        }
    }
}
