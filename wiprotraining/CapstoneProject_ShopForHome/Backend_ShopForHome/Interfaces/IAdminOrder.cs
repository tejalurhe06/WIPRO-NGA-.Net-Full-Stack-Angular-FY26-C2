using ShopForHome.API.DTOs;

namespace ShopForHome.API.Interfaces
{
    public interface IAdminOrderService
    {
        Task<List<OrderDTO>> GetAllOrdersAsync(string status = "", DateTime? startDate = null, DateTime? endDate = null);
        Task UpdateOrderStatusAsync(int orderId, string newStatus);
        Task<object> GetOrderStatsAsync();
    }
}
