using BookStore.Web.Models;
using System.Data;


namespace BookStore.Web.Data
{
public interface IBookRepository
{
        Task<IEnumerable<Book>> GetAllAsync(CancellationToken ct = default); // SqlDataReader (connected)
        Task<Book?> GetByIdAsync(int id, CancellationToken ct = default); // Stored proc
        Task<int> AddAsync(Book book, CancellationToken ct = default); // Stored proc
        Task UpdateAsync(Book book, CancellationToken ct = default); // Stored proc
        Task DeleteAsync(int id, CancellationToken ct = default); // Stored proc


        Task<DataSet> GetAllDataSetAsync(CancellationToken ct = default); // SqlDataAdapter (disconnected)
        Task<int> UpdateViaDataSetAsync(DataSet ds, CancellationToken ct = default); // Disconnected update
}
}