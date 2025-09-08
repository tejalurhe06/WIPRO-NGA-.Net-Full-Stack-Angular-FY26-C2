using LibraryManagementSystem.Core.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LibraryManagementSystem.Core.Interfaces
{
    public interface IGenreRepository : IRepository<Genre>
    {
        Task<IEnumerable<Genre>> GetGenresWithBooksAsync();
        Task<Genre> GetGenreWithDetailsAsync(int id);
    }
}