using ShopForHome.API.DTOs;

namespace ShopForHome.API.Interfaces
{
    public interface IWishlistService
    {
        Task<List<ProductDTO>> GetWishlistAsync(int userId);
        Task AddToWishlistAsync(int userId, int productId);
        Task RemoveFromWishlistAsync(int userId, int productId);
        Task<bool> IsInWishlistAsync(int userId, int productId);
    }
}
