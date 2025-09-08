using Microsoft.EntityFrameworkCore;
using webapidemo1.Models;

namespace webapidemo1.Data
{
    public class EFCodeFirstContext : DbContext
    {
        public EFCodeFirstContext(DbContextOptions<EFCodeFirstContext> options)
            : base(options)
        {
        }

        public DbSet<Student> Students { get; set; }
        public DbSet<StudentAddress> StudentAddresses { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Student>()
                .HasOne(s => s.Address)
                .WithOne(a => a.Student)
                .HasForeignKey<StudentAddress>(a => a.StudentAddressId)
                .IsRequired(false); // Optional relationship
        }
    }
}
