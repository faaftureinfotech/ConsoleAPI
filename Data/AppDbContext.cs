using ConstructionFinance.API.Models;
using ConstructionFinance.API.Models.Master;
using ConstructionFinance.API.Models.Quotation;
using Microsoft.EntityFrameworkCore;
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
        public DbSet<Role> Roles { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Supplier> Suppliers { get; set; }
        public DbSet<Unit> Units { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Material> Materials { get; set; }
        public DbSet<BOQItem> BOQItems { get; set; }
        public DbSet<BOQMaster> BOQMasters { get; set; }
        public DbSet<Quotation> Quotations { get; set; }
        public DbSet<QuotationItem> QuotationItems { get; set; }
        public DbSet<EmployeeAllocation> EmployeeAllocations { get; set; }


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

            modelBuilder.Entity<EmployeeAllocation>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.AllocationType).HasMaxLength(50).IsRequired();
                entity.Property(e => e.Notes).HasMaxLength(1000);

                entity.HasOne(e => e.Employee)
                      .WithMany()
                      .HasForeignKey(e => e.EmployeeId)
                      .OnDelete(DeleteBehavior.Cascade)
                      .IsRequired();

                entity.HasOne(e => e.Project)
                      .WithMany()
                      .HasForeignKey(e => e.ProjectId)
                      .OnDelete(DeleteBehavior.SetNull);
            });

            // Unit Master Seed
            modelBuilder.Entity<Unit>().HasData(
                new Unit { Id = 1, Name = "Sqft" },
                new Unit { Id = 2, Name = "m3" },
                new Unit { Id = 3, Name = "Nos" },
                new Unit { Id = 4, Name = "Kg" },
                new Unit { Id = 5, Name = "Meter" }
            );

            // Category Master Seed
            modelBuilder.Entity<Category>().HasData(
                new Category { Id = 1, Name = "Civil" },
                new Category { Id = 2, Name = "Electrical" },
                new Category { Id = 3, Name = "Plumbing" },
                new Category { Id = 4, Name = "Painting" },
                new Category { Id = 5, Name = "Flooring" }
            );

            // Material Master Seed
            modelBuilder.Entity<Material>().HasData(
                new Material { Id = 1, Name = "Cement", DefaultUnitId = 3, DefaultRate = 350 },
                new Material { Id = 2, Name = "Sand", DefaultUnitId = 2, DefaultRate = 900 },
                new Material { Id = 3, Name = "Steel", DefaultUnitId = 4, DefaultRate = 65 },
                new Material { Id = 4, Name = "Tiles", DefaultUnitId = 1, DefaultRate = 45 },
                new Material { Id = 5, Name = "Paint", DefaultUnitId = 5, DefaultRate = 25 }
            );
        }
    }
}


