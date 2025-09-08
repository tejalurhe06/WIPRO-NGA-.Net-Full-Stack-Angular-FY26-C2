using Microsoft.EntityFrameworkCore;

namespace Entity_Framework_Core.Models
{
    public class CompanyContext : DbContext
    {
        public CompanyContext(DbContextOptions<CompanyContext> options)
            : base(options)
        {
        }

        public DbSet<Employee> Employees { get; set; }
        public DbSet<Department> Departments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Department>(entity =>
            {
                entity.Property(e => e.Name).IsRequired().HasMaxLength(100);
            });

            modelBuilder.Entity<Employee>(entity =>
            {
                entity.Property(e => e.Name).IsRequired().HasMaxLength(100).IsUnicode(false);
                entity.Property(e => e.Designation).IsRequired().HasMaxLength(100).IsUnicode(false);
                entity.HasOne(d => d.Department).WithMany().HasForeignKey(d => d.DepartmentId).OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("FK_Employee_Department")  ;
            });
        }

    }
}
