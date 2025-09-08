using LibraryManagementSystem.Core.Interfaces;
using LibraryManagementSystem.Core.Models;
using LibraryManagementSystem.Data.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LibraryManagementSystem.Data.Repositories
{
    public class GenreRepository : Repository<Genre>, IGenreRepository
    {
        public GenreRepository(LibraryDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Genre>> GetGenresWithBooksAsync()
        {
            return await _context.Genres
                .Include(g => g.Books)
                .ThenInclude(b => b.Author)
                .ToListAsync();
        }

        public async Task<Genre> GetGenreWithDetailsAsync(int id)
        {
            return await _context.Genres
                .Include(g => g.Books)
                .ThenInclude(b => b.Author)
                .FirstOrDefaultAsync(g => g.Id == id);
        }
    }
}