using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using NPOI.SS.Formula.Functions;
using StorageSystem.Application.Contracts.DataAccess;
using StorageSystem.DataAccess.UOW.Base;
using StorageSystem.Domain.Entities;
using StorageSystem.Persistence;
using StorageSystem.Persistence.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace StorageSystem.DataAccess.UOW;

public class ProductDataAccess : GenericDataAccess<Product>, IProductDataAccess
{
    public ProductDataAccess(IApplicationDbContext context) : base(context)
    {
    }

    public async Task CreateProductRangeAsync(List<Product> products, CancellationToken cancellationToken = default)
    {
        await _context.Products.AddRangeAsync(products, cancellationToken);
    }

    public Task<Product> FindProductById(Guid Id)
    {
        throw new NotImplementedException();
    }

    public async Task<Product> FirstAsync(Guid Id)
    {
        return await _context.Products.FirstAsync(x => x.Id == Id);
    }

    public async Task<Product> FirstOrDefaultAsync(Guid Id, CancellationToken cancellationToken = default)
    {
        return await _context.Products.FirstOrDefaultAsync(x => x.Id == Id, cancellationToken);
    }

    public async Task<IEnumerable<Product>> GetAllProducts(CancellationToken cancellationToken = default)
    {
        return await _context.Products.ToListAsync(cancellationToken);
    }

    public async Task<List<Product>> GetAllProducts1(CancellationToken cancellationToken = default)
    {
        return await _context.Products.ToListAsync();
    }

    public Task<IEnumerable<Product>> GetProductsByCategoryId(Guid CategoryId, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
}
