using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using StorageSystem.Domain.Entities;
using StorageSystem.Persistence.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace StorageSystem.Persistence
{
    public class ApplicationDbContext : IdentityDbContext<IdentityUser, IdentityRole, string>, IApplicationDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        public DbSet<Category> Categories => Set<Category>();
        public DbSet<Product> Products => Set<Product>();
        public DbSet<ProductImage> ProductImages => Set<ProductImage>();

        DbSet<Product> IApplicationDbContext.Products { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        DbSet<Category> IApplicationDbContext.Categories { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        DbSet<ProductImage> IApplicationDbContext.ProductImages { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

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
            builder.Entity<Category>()
            .Property(f => f.Id)
            .ValueGeneratedOnAdd();

            builder.Entity<Product>()
            .Property(f => f.Id)
            .ValueGeneratedOnAdd();

            builder.Entity<ProductImage>()
            .Property(f => f.Id)
            .ValueGeneratedOnAdd();
        }

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) => optionsBuilder.UseLazyLoadingProxies();

        public Task<int> SaveChangeAsync(CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }
    }
}
