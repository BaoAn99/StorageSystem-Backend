using StorageSystem.Application.Contracts.Repositories.Base;
using StorageSystem.Domain.Commons.Interfaces;

namespace StorageSystem.Application.Contracts.Repositories
{
    public interface IProductRepository<TEntity, TKey> : IRepositoryBaseAsync<TEntity, TKey> where TEntity : IEntity<TKey>
    {
        Task<TKey> CreateProductAsync(TEntity entity);
        Task<TKey> UpdateProductAsync(TEntity entity);
        Task DeleteProductAsync(TEntity entity);
        Task<TEntity?> FindProductByIdAsync(TKey id);
        IQueryable<TEntity> GetAllProducts();
    }
}
