using Microsoft.EntityFrameworkCore.ChangeTracking;
using OneOf;
using StorageSystem.Application.Contracts.DataAccess.Base;
using StorageSystem.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StorageSystem.Application.Contracts.DataAccess
{
    public interface IProductDataAccess : IGenericDataAccess<Product>
    {
        Task<Product> FirstOrDefaultAsync(Guid Id, CancellationToken cancellationToken = default);

        Task<Product> FirstAsync(Guid Id);

        Task<bool> CreateProductAsync(Product product, CancellationToken cancellationToken = default);

        Task CreateProductRangeAsync(List<Product> products, CancellationToken cancellationToken = default);

        Task<bool> DeleteProduct(Guid productId, CancellationToken cancellationToken = default); 

        Task<IEnumerable<Product>> GetAllProducts(CancellationToken cancellationToken = default);

        Task<IEnumerable<Product>> GetProductsByCategoryId(Guid CategoryId, CancellationToken cancellationToken = default);

        Task<Product> FindProductById(Guid Id);
    }
}
