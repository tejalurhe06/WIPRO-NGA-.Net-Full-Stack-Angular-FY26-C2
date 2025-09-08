using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using OnlineBookstoreMVC.Models;

namespace OnlineBookstoreMVC.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Book> Books { get; set; }
        public DbSet<Order> Orders { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        // Seed some sample books
        builder.Entity<Book>().HasData(
            new Book { Id = 1, Title = "C# in Depth", Author = "Jon Skeet", ISBN = "1234567890", Price = 450 },
            new Book { Id = 2, Title = "ASP.NET Core MVC", Author = "Adam Freeman", ISBN = "1234567890123", Price = 500 },
            new Book { Id = 3, Title = "Clean Code", Author = "Robert Martin", ISBN = "0987654321", Price = 400 }
        );
    }
    }
}
