using System.Data;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;


namespace BookStore.Web.Data
{
    public interface IDbConnectionFactory
    {
        SqlConnection Create();
    }

 public class DbConnectionFactory : IDbConnectionFactory
    {
        private readonly string _connectionString;

        public DbConnectionFactory(IConfiguration configuration)
        {
            // "DefaultConnection" should match your appsettings.json
            _connectionString = configuration.GetConnectionString("DefaultConnection")
                                ?? throw new InvalidOperationException("Connection string not found.");
        }

        public SqlConnection Create()
        {
            return new SqlConnection(_connectionString);
        }
    }
}