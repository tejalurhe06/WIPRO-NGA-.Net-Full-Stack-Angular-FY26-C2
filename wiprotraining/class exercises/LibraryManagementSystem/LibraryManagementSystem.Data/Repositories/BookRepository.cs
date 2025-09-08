using LibraryManagementSystem.Core.Interfaces;
using LibraryManagementSystem.Core.Models;
using LibraryManagementSystem.Data.Data;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagementSystem.Data.Repositories
{
    public class BookRepository : Repository<Book>, IBookRepository
    {
        public BookRepository(LibraryDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Book>> GetBooksWithAuthorsAndGenresAsync()
        {
            return await _context.Books
                .Include(b => b.Author)
                .Include(b => b.Genre)
                .ToListAsync();
        }

        public async Task<Book> GetBookWithDetailsAsync(int id)
        {
            return await _context.Books
                .Include(b => b.Author)
                .Include(b => b.Genre)
                .FirstOrDefaultAsync(b => b.Id == id);
        }

        public async Task<IEnumerable<Book>> GetBooksByAuthorAsync(int authorId)
        {
            return await _context.Books
                .Include(b => b.Author)
                .Include(b => b.Genre)
                .Where(b => b.AuthorId == authorId)
                .ToListAsync();
        }

        public async Task<IEnumerable<Book>> GetBooksByGenreAsync(int genreId)
        {
            return await _context.Books
                .Include(b => b.Author)
                .Include(b => b.Genre)
                .Where(b => b.GenreId == genreId)
                .ToListAsync();
        }
    }
}