using Microsoft.EntityFrameworkCore;
using ConstructionFinance.API.Models;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Internal;

namespace ConstructionFinance.API.Data
{
    public class AppDbContext : DbContext, IInfrastructure<IServiceProvider>, IDbContextDependencies, IDbSetCache, IDbContextPoolable, IResettableService, IDisposable, IAsyncDisposable
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Customer> Customers { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<Expense> Expenses { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<Supplier> Suppliers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Project>()
                .HasOne(p => p.Customer)
                .WithMany(c => c.Projects)
                .HasForeignKey(p => p.CustomerId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Project>()
                .Property(p => p.Id)
                .ValueGeneratedOnAdd();

            modelBuilder.Entity<Supplier>(entity =>
            {
                entity.Property(e => e.SupplierName)
                      .IsRequired()
                      .HasMaxLength(200);

                entity.Property(e => e.MobileNumber)
                      .IsRequired()
                      .HasMaxLength(20);

                entity.Property(e => e.OpeningBalance)
                      .HasPrecision(18, 2);

                entity.Property(e => e.Status)
                      .HasDefaultValue("Active");

                entity.Property(e => e.SupplierType)
                      .HasDefaultValue("Material");
            });
        }
    }
}


