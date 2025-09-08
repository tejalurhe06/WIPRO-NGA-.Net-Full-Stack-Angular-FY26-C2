using ShopForHome.API.DTOs;

namespace ShopForHome.API.Interfaces
{
    public interface IOrderService
    {
        Task<List<OrderDTO>> GetUserOrdersAsync(int userId);
        Task<OrderDTO> GetOrderByIdAsync(int orderId, int userId);
        Task<int> CreateOrderAsync(int userId, CreateOrderRequest request);
    }
}
