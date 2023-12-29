using MathNet.Numerics.Statistics.Mcmc;
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

    public async Task CreateProductAsync(Product product, CancellationToken cancellationToken = default)
    {
        await InsertAsync(product);
        //await _context.Products.AddAsync(product, cancellationToken);
    }

    public async Task CreateProductRangeAsync(List<Product> products, CancellationToken cancellationToken = default)
    {
        await _context.Products.AddRangeAsync(products, cancellationToken);
    }

    public async Task<bool> DeleteProduct(Guid productId, CancellationToken cancellationToken = default)
    {
        Product product = await FindProductById(productId);
        if(product != null)
        {
            //product.IsDeleted = true;
            //EntityEntry<Product> res = Update(product);
            Delete(product);
            return true;
        }
        return false;
    }

    public async Task<Product> FindProductById(Guid Id)
    {
        return await FirstOrDefaultAsync(Id);
    }

    public async Task<Product> FirstAsync(Guid Id)
    {
        return await _context.Products.FirstAsync(x => x.Id == Id);
    }

    public async Task<Product> FirstOrDefaultAsync(Guid Id, CancellationToken cancellationToken = default)
    {
        return await _context.Products.Include(p => p.ProductImages).FirstOrDefaultAsync(x => x.Id == Id, cancellationToken);
    }

    public async Task<IEnumerable<Product>> GetAllProducts(CancellationToken cancellationToken = default)
    {
        return await _context.Products.Include(p => p.ProductImages).ToListAsync(cancellationToken);
    }

    public Task<IEnumerable<Product>> GetProductsByCategoryId(Guid CategoryId, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public async Task<bool> UpdateProduct(Product product, CancellationToken cancellationToken = default)
    {
        EntityEntry<Product> res = Update(product);
        if (res != null)
        {
            return true;
        }
        return false;
    }
}
