using LibraryManagementSystem.Core.Interfaces;
using LibraryManagementSystem.Core.Models;
using LibraryManagementSystem.Data.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LibraryManagementSystem.Data.Repositories
{
    public class AuthorRepository : Repository<Author>, IAuthorRepository
    {
        public AuthorRepository(LibraryDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Author>> GetAuthorsWithBooksAsync()
        {
            return await _context.Authors
                .Include(a => a.Books)
                .ThenInclude(b => b.Genre)
                .ToListAsync();
        }

        public async Task<Author> GetAuthorWithDetailsAsync(int id)
        {
            return await _context.Authors
                .Include(a => a.Books)
                .ThenInclude(b => b.Genre)
                .FirstOrDefaultAsync(a => a.Id == id);
        }
    }
}