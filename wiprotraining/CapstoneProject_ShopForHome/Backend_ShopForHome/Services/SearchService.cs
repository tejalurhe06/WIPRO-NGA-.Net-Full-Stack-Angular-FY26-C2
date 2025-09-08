using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ShopForHome.API.Data;
using ShopForHome.API.DTOs;
using ShopForHome.API.Interfaces;
namespace ShopForHome.API.Services
{
    public class SearchService : ISearchService
    {
        private readonly ShopForHomeDbContext _context;
        private readonly ILogger<SearchService> _logger;

        public SearchService(ShopForHomeDbContext context, ILogger<SearchService> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<List<ProductDTO>> SearchProductsAsync(
            string term = "",
            int? categoryId = null,
            decimal? minPrice = null,
            decimal? maxPrice = null,
            decimal? minRating = null)
        {
            var query = _context.Products
                .Include(p => p.Category)
                .Where(p => p.IsActive)
                .AsQueryable();

            if (!string.IsNullOrEmpty(term))
            {
                term = term.ToLower();
                query = query.Where(p =>
                    p.Name.ToLower().Contains(term) ||
                    p.Description.ToLower().Contains(term));
            }

            if (categoryId.HasValue)
                query = query.Where(p => p.CategoryId == categoryId);

            if (minPrice.HasValue)
                query = query.Where(p => p.Price >= minPrice);

            if (maxPrice.HasValue)
                query = query.Where(p => p.Price <= maxPrice);

            if (minRating.HasValue)
                query = query.Where(p => p.AverageRating >= minRating);

            var results = await query
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

            _logger.LogInformation(
                "Search executed with term: {Term}, categoryId: {CategoryId}, minPrice: {MinPrice}, maxPrice: {MaxPrice}, minRating: {MinRating}, ResultsCount: {Count}",
                term, categoryId, minPrice, maxPrice, minRating, results.Count);

            return results;
        }
    }
}
