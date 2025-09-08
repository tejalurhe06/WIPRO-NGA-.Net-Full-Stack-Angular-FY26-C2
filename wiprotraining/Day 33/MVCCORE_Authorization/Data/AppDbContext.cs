using Microsoft.EntityFrameworkCore;
using MVCCoreLoginModule.Models;

namespace MVCCoreLoginModule.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
    }
}
