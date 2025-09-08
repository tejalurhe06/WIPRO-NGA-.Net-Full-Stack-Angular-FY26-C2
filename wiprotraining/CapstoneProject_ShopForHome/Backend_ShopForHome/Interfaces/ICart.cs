using ShopForHome.API.DTOs;

namespace ShopForHome.API.Interfaces
{
    public interface ICartService
    {
        Task<List<CartItemDTO>> GetCartAsync(int userId);
        Task<CartItemDTO> AddToCartAsync(int userId, AddToCartRequest request);
        Task UpdateCartItemAsync(int userId, int cartItemId, int newQuantity);
        Task RemoveCartItemAsync(int userId, int cartItemId);
        Task<decimal> GetCartTotalAsync(int userId);
    }
}
