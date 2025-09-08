using LibraryManagementSystem.Core.Models;

namespace LibraryManagementSystem.Core.Interfaces
{
    public interface IBookRepository : IRepository<Book>
    {
        Task<IEnumerable<Book>> GetBooksWithAuthorsAndGenresAsync();
        Task<Book> GetBookWithDetailsAsync(int id);
        Task<IEnumerable<Book>> GetBooksByAuthorAsync(int authorId);
        Task<IEnumerable<Book>> GetBooksByGenreAsync(int genreId);
    }
}