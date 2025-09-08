using BookStore.Web.Models;
using Microsoft.Data.SqlClient;
using System.Data;
namespace BookStore.Web.Data
{
    public class BookRepository : IBookRepository
    {
        private readonly IDbConnectionFactory _factory;
        private readonly ILogger<BookRepository> _logger;
        public BookRepository(IDbConnectionFactory factory,
        ILogger<BookRepository> logger)
        {
            _factory = factory;
            _logger = logger;
        }
        // Connected – SqlDataReader (forward-only, fast)
        public async Task<IEnumerable<Book>> GetAllAsync(CancellationToken ct =
        default)
        {
            var results = new List<Book>();
            await using var conn = _factory.Create();
            await conn.OpenAsync(ct);
            const string sql = "SELECT * FROM dbo.Books ORDER BY CreatedAt DESC";
            await using var cmd = new SqlCommand(sql, conn)
            {
                CommandType =
            CommandType.Text
            };
            await using var reader = await cmd.ExecuteReaderAsync(ct);
            while (await reader.ReadAsync(ct))
            {
                results.Add(MapBook(reader));
            }
            return results;
        }
        // Stored Procedure – Get by Id
        public async Task<Book?> GetByIdAsync(int id, CancellationToken ct =
        default)
        {
            await using var conn = _factory.Create();
            await conn.OpenAsync(ct);
            await using var cmd = new SqlCommand("dbo.usp_GetBookById", conn)
            { CommandType = CommandType.StoredProcedure };
            cmd.Parameters.Add(new SqlParameter("@Id", SqlDbType.Int)
            {
                Value =
            id
            });
            await using var reader = await cmd.ExecuteReaderAsync(ct);
            return await reader.ReadAsync(ct) ? MapBook(reader) : null;
        }

        // Stored Procedure – Add (returns new Id)
        public async Task<int> AddAsync(Book book, CancellationToken ct =
        default)
        {
            await using var conn = _factory.Create();
            await conn.OpenAsync(ct);
            await using var cmd = new SqlCommand("dbo.usp_AddBook", conn)
            { CommandType = CommandType.StoredProcedure };
            cmd.Parameters.AddRange(new[]
            {
new SqlParameter("@Title", SqlDbType.NVarChar, 200) { Value =
book.Title },
new SqlParameter("@Author", SqlDbType.NVarChar, 150) { Value =
book.Author },
new SqlParameter("@Genre", SqlDbType.NVarChar, 100) { Value =
(object?)book.Genre ?? DBNull.Value },
new SqlParameter("@ISBN", SqlDbType.NVarChar, 20) { Value =
(object?)book.ISBN ?? DBNull.Value },
new SqlParameter("@Price", SqlDbType.Decimal) { Precision=10,
Scale=2, Value = book.Price },
new SqlParameter("@Stock", SqlDbType.Int) { Value =
book.Stock },
new SqlParameter("@PublishedDate", SqlDbType.Date) { Value =
(object?)book.PublishedDate ?? DBNull.Value }
});
            var newId = 0;
            await using var reader = await cmd.ExecuteReaderAsync(ct);
            if (await reader.ReadAsync(ct))
            {
                newId = Convert.ToInt32(reader["NewId"]);
            }
            return newId;
        }
        // Stored Procedure – Update
        public async Task UpdateAsync(Book book, CancellationToken ct = default)
        {
            await using var conn = _factory.Create();
            await conn.OpenAsync(ct);
            await using var cmd = new SqlCommand("dbo.usp_UpdateBook", conn)
            { CommandType = CommandType.StoredProcedure };
            cmd.Parameters.AddRange(new[]
            {
            new SqlParameter("@Id", SqlDbType.Int) { Value = book.Id },
            new SqlParameter("@Title", SqlDbType.NVarChar, 200) { Value =book.Title },
            new SqlParameter("@Author", SqlDbType.NVarChar, 150) { Value =book.Author },
            new SqlParameter("@Genre", SqlDbType.NVarChar, 100) { Value =
            (object?)book.Genre ?? DBNull.Value },new SqlParameter("@ISBN", SqlDbType.NVarChar, 20) { Value =(object?)book.ISBN ?? DBNull.Value },new SqlParameter("@Price", SqlDbType.Decimal) { Precision=10,Scale=2, Value = book.Price },
            new SqlParameter("@Stock", SqlDbType.Int) { Value =book.Stock },
            new SqlParameter("@PublishedDate", SqlDbType.Date) { Value =(object?)book.PublishedDate ?? DBNull.Value }
            });
            await cmd.ExecuteNonQueryAsync(ct);
        }
        // Stored Procedure – Delete
        public async Task DeleteAsync(int id, CancellationToken ct = default)
        {
            await using var conn = _factory.Create();
            await conn.OpenAsync(ct);
            await using var cmd = new SqlCommand("dbo.usp_DeleteBook", conn)
            { CommandType = CommandType.StoredProcedure };
            cmd.Parameters.Add(new SqlParameter("@Id", SqlDbType.Int)
            {
                Value =
            id
            });
            await cmd.ExecuteNonQueryAsync(ct);
        }
        // Disconnected – DataSet via SqlDataAdapter
        public async Task<DataSet> GetAllDataSetAsync(CancellationToken ct =
        default)
        {
            await using var conn = _factory.Create();
            await conn.OpenAsync(ct);
            const string sql = "SELECT * FROM dbo.Books ORDER BY CreatedAt DESC";
            using var adapter = new SqlDataAdapter(sql, conn);
            var ds = new DataSet();
            adapter.Fill(ds, "Books"); // Fill DataSet with DataTable named "Books"
            return await Task.FromResult(ds);
        }
        // Disconnected update: apply DataTable changes back to DB
        public async Task<int> UpdateViaDataSetAsync(DataSet ds,
        CancellationToken ct = default)
        {
            if (!ds.Tables.Contains("Books")) return 0;
            await using var conn = _factory.Create();
            await conn.OpenAsync(ct);
            const string sql = "SELECT Id, Title, Author, Genre, ISBN, Price, Stock, PublishedDate FROM dbo.Books";
            using var adapter = new SqlDataAdapter(sql, conn);
            using var builder = new
            SqlCommandBuilder(adapter); // auto-generate INSERT/UPDATE/DELETE .Ensure commands are parameterized (SqlCommandBuilder handles this)
            var rows = adapter.Update(ds, "Books");
            return rows;
        }
        private static Book MapBook(SqlDataReader reader)
        {
            return new Book
            {
                Id = reader.GetInt32(reader.GetOrdinal("Id")),
                Title = reader.GetString(reader.GetOrdinal("Title")),
                Author = reader.GetString(reader.GetOrdinal("Author")),
                Genre = reader.IsDBNull(reader.GetOrdinal("Genre")) ? null : reader.GetString(reader.GetOrdinal("Genre")),
                ISBN = reader.IsDBNull(reader.GetOrdinal("ISBN")) ? null : reader.GetString(reader.GetOrdinal("ISBN")),
                Price = reader.GetDecimal(reader.GetOrdinal("Price")),
                Stock = reader.GetInt32(reader.GetOrdinal("Stock")),
                PublishedDate = reader.IsDBNull(reader.GetOrdinal("PublishedDate")) ? (DateTime?)null :reader.GetDateTime(reader.GetOrdinal("PublishedDate")),
                CreatedAt = reader.GetDateTime(reader.GetOrdinal("CreatedAt")),
                UpdatedAt = reader.GetDateTime(reader.GetOrdinal("UpdatedAt"))
            };
        }
    }
}
