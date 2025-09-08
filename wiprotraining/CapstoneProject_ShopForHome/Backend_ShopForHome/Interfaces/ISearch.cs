using ShopForHome.API.DTOs;

namespace ShopForHome.API.Interfaces
{
    public interface ISearchService
    {
        Task<List<ProductDTO>> SearchProductsAsync(
            string term = "",
            int? categoryId = null,
            decimal? minPrice = null,
            decimal? maxPrice = null,
            decimal? minRating = null);
    }
}
