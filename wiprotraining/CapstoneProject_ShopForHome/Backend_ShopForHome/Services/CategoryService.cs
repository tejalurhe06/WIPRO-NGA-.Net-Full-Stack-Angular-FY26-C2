using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ShopForHome.API.Data;
using ShopForHome.API.Models;
using ShopForHome.API.Interfaces;

namespace ShopForHome.API.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ShopForHomeDbContext _context;
        private readonly ILogger<CategoryService> _logger;

        public CategoryService(ShopForHomeDbContext context, ILogger<CategoryService> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<List<Category>> GetCategoriesAsync()
        {
            return await _context.Categories.ToListAsync();
        }

        public async Task<Category> GetCategoryByIdAsync(int id)
        {
            var category = await _context.Categories.FindAsync(id);
            if (category == null)
                throw new ApplicationException("Category not found");
            return category;
        }

        public async Task<Category> CreateCategoryAsync(Category category)
        {
            _context.Categories.Add(category);
            await _context.SaveChangesAsync();
            _logger.LogInformation("Category {CategoryId} created", category.CategoryId);
            return category;
        }

        public async Task UpdateCategoryAsync(int id, Category category)
        {
            if (id != category.CategoryId)
                throw new ApplicationException("Category ID mismatch");

            _context.Entry(category).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
                _logger.LogInformation("Category {CategoryId} updated", id);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await _context.Categories.AnyAsync(e => e.CategoryId == id))
                    throw new ApplicationException("Category not found");
                else
                    throw;
            }
        }

        public async Task DeleteCategoryAsync(int id)
        {
            var category = await _context.Categories.FindAsync(id);
            if (category == null)
                throw new ApplicationException("Category not found");

            _context.Categories.Remove(category);
            await _context.SaveChangesAsync();

            _logger.LogInformation("Category {CategoryId} deleted", id);
        }
    }
}
