using ShopForHome.API.DTOs;
using ShopForHome.API.Models;
namespace ShopForHome.API.Interfaces
{
    public interface IAdminProductService
    {
        Task<List<AdminProductDTO>> GetAllProductsAsync();
        Task<List<ProductDTO>> GetLowStockProductsAsync();
        Task<object> BulkUploadProductsAsync(List<CreateProductDTO> products);
        Task UpdateStockAsync(int productId, int newStockQuantity);
        Task ToggleProductStatusAsync(int productId);
        Task<object> GetProductStatsAsync();
        Task UpdateProductPricesAsync(decimal percentageChange);

    }
}
