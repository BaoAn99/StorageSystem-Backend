using Microsoft.EntityFrameworkCore;
using StorageSystem.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StorageSystem.Persistence.Contracts;

public interface IApplicationDbContext : IDisposable
{
    public DbSet<Product> Products { get; set; }

    public DbSet<Category> Categories { get; set; }

    public DbSet<ProductImage> ProductImages { get; set; }

    public DbSet<T> Set<T>() where T : class;

    int SaveChanges();

    Task<int> SaveChangeAsync(CancellationToken cancellationToken = default);
}
