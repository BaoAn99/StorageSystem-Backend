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
        return await _context.Products.ToListAsync(cancellationToken);
    }

    public async Task<IEnumerable<Product>> GetAllProducts(bool trackingReference, CancellationToken cancellationToken = default)
    {
        return await _context.Products.Include(p => p.ProductImages).Include(p => p.Category).ToListAsync(cancellationToken);
    }

    public async Task<IEnumerable<Product>> GetProductsByCategoryId(Guid CategoryId, CancellationToken cancellationToken = default)
    {
        return await _context.Products.Include(p => p.ProductImages).Where(p => p.CategoryId == CategoryId).ToListAsync(cancellationToken);
    }

    public async void UpdateProduct(Product product, CancellationToken cancellationToken = default)
    {
        Update(product);
    }

    public void UpdateProductRange(List<Product> products, CancellationToken cancellationToken = default)
    {
        _context.Products.UpdateRange(products);
    }

    public async void DeleteProduct(Product product, CancellationToken cancellationToken = default)
    {
        Delete(product);
    }

    public void DeleteProductRange(List<Product> products, CancellationToken cancellationToken = default)
    {
        _context.Products.RemoveRange(products);
    }
}
