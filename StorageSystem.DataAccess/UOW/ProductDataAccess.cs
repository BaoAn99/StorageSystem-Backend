using Microsoft.EntityFrameworkCore.ChangeTracking;
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

namespace StorageSystem.DataAccess.UOW
{
    public class ProductDataAccess : GenericDataAccess<Product>, IProductDataAccess
    {
        public ProductDataAccess(IApplicationDbContext context) : base(context)
        {
        }

        public Task<EntityEntry<Product>> CreateProduct(Product product)
        {
            throw new NotImplementedException();
        }

        public Task CreateProductRangeAsync(List<Product> products, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task DeleteProduct(Guid Id)
        {
            throw new NotImplementedException();
        }

        public Task<Product> FindProductById(Guid Id)
        {
            throw new NotImplementedException();
        }

        public Task<Product> FirstAsync(Guid Id)
        {
            throw new NotImplementedException();
        }

        public Task<Product> FirstOrDefaultAsync(Guid Id, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Product>> GetAllProducts(CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Product>> GetProductsByCategoryId(Guid CategoryId, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task UpdateProduct(Product product)
        {
            throw new NotImplementedException();
        }
    }
}
