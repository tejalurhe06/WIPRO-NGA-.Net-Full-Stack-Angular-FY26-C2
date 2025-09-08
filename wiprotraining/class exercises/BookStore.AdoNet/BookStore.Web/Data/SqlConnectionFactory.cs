using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;


namespace BookStore.Web.Data
{
    public class SqlConnectionFactory : IDbConnectionFactory
    {
        private readonly string _connString;
        public SqlConnectionFactory(IConfiguration config)
        {
        _connString = config.GetConnectionString("Default")!;
        }
        public SqlConnection Create() => new SqlConnection(_connString);
    }
}