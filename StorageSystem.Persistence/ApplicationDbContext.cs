using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using StorageSystem.Domain.Entities;
using StorageSystem.Persistence.Contracts;
using StorageSystem.Persistence.Models;
using System.Reflection.Emit;

namespace StorageSystem.Persistence;

public class ApplicationDbContext : IdentityDbContext<ApplicationUser, IdentityRole, string>, IApplicationDbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
        
    }

    //public DbSet<Product> Products => Set<Product>();
    public DbSet<Product> Products { get; set; }
    public DbSet<Category> Categories { get ; set ; }
    public DbSet<ProductImage> ProductImages { get ; set ; }
    public DbSet<Bill> Bills { get ; set ; }
    public DbSet<Coupon> Coupons { get ; set ; }
    public DbSet<Customer> Customers { get ; set ; }
    public DbSet<Supplier> Suppliers { get ; set ; }
    public DbSet<Unit> Units { get ; set ; }
    public DbSet<Order> Orders { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        // Bỏ tiền tố AspNet của các bảng: mặc định các bảng trong IdentityDbContext có
        // tên với tiền tố AspNet như: AspNetUserRoles, AspNetUser ...
        // Đoạn mã sau chạy khi khởi tạo DbContext, tạo database sẽ loại bỏ tiền tố đó
        foreach (var entityType in builder.Model.GetEntityTypes())
        {
            var tableName = entityType.GetTableName();
            if (tableName!.StartsWith("AspNet"))
            {
                entityType.SetTableName(tableName.Substring(6));
            }
        }
        //builder
        //.Entity<Supplier>(
        //    eb =>
        //    {
        //        eb.HasNoKey();
        //        eb.Property(v => v.ProductIds);
        //    });
        //builder.Entity<Category>()
        //.Property(f => f.Id)
        //.ValueGeneratedOnAdd();

        //builder.Entity<Product>()
        //.Property(f => f.Id)
        //.ValueGeneratedOnAdd();

        //builder.Entity<ProductImage>()
        //.Property(f => f.Id)
        //.ValueGeneratedOnAdd();



        //Id = new Guid("4b703409-a32a-49cf-9943-d5a68dbedd6f"),
        //Name = "Thùng",
        //Id = new Guid("bb7b0969-4383-446f-8ab9-6d766df08359"),
        //Name = "Kg",
        //Id = new Guid("54cb56aa-a710-47e0-a386-4cc493d46747"),
        //Name = "Hộp",

        //builder.Entity<ProductUnit>().HasData(
        // new ProductUnit
        // {
        //     ProductId = new Guid("5d01611b-e2ad-4abd-b40e-0450f50d2ec9"),
        //     UnitId = new Guid("4b703409-a32a-49cf-9943-d5a68dbedd6f"),
        //     Quantity = 100
        // },
        // new ProductUnit
        // {
        //     ProductId = new Guid("5d01611b-e2ad-4abd-b40e-0450f50d2ec9"),
        //     UnitId = new Guid("54cb56aa-a710-47e0-a386-4cc493d46747"),
        //     Quantity = 100
        // },
        // new ProductUnit
        // {
        //     ProductId = new Guid("c861791b-bce9-47a9-84c7-1abdb578d88b"),
        //     UnitId = new Guid("bb7b0969-4383-446f-8ab9-6d766df08359"),
        //     Quantity = 100
        // }
        //);
    }

    //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) => optionsBuilder.UseLazyLoadingProxies();
}
