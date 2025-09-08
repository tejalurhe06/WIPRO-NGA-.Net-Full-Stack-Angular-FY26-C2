using LibraryManagementSystem.Core.Models;

namespace LibraryManagementSystem.Core.Interfaces
{
    public interface IAuthorRepository : IRepository<Author>
    {
        Task<IEnumerable<Author>> GetAuthorsWithBooksAsync();
        Task<Author> GetAuthorWithDetailsAsync(int id);
    }
}