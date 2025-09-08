using Microsoft.EntityFrameworkCore;

namespace CODEFIRST_EFCORE.Models
{
   public class CompanyContext : DbContext
   {
       public CompanyContext(DbContextOptions<CompanyContext> options) : base(options)
       {
       }

       public DbSet<Information> Information { get; set; }

       protected override void OnModelCreating(ModelBuilder modelBuilder)
       {
              modelBuilder.Entity<Information>(entity =>
              {
                  entity.ToTable("Information");
                  entity.HasKey(i => i.Id);
                  entity.Property(i => i.Name).IsRequired().HasMaxLength(100);
                  entity.Property(i => i.License).HasMaxLength(50);
                  entity.Property(i => i.Established).IsRequired();
                  entity.Property(i => i.Revenue).HasColumnType("decimal(18,2)");
              });
       }
   }
}