using ShopForHome.API.DTOs;

namespace ShopForHome.API.Interfaces
{
    public interface IProductService
    {
        Task<List<ProductDTO>> GetProductsAsync(int? categoryId, decimal? minPrice, decimal? maxPrice);
        Task<ProductDTO> GetProductByIdAsync(int id);
        Task<int> CreateProductAsync(CreateProductDTO dto);
        Task UpdateProductAsync(UpdateProductDTO dto);
        Task DeleteProductAsync(int id);
    }
}
