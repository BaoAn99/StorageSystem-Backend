using StorageSystem.Application.Contracts.Repositories.Base;
using StorageSystem.Domain.Commons.Interfaces;

namespace StorageSystem.Application.Contracts.Repositories
{
    public interface IProductTypeRepository<TEntity, TKey> : IRepositoryBaseAsync<TEntity, TKey> where TEntity : IEntity<TKey>
    {
        Task<TKey> CreateProductTypeAsync(TEntity entity);
        Task<TKey> UpdateProductTypeAsync(TEntity entity);
        Task DeleteProductTypeAsync(TEntity entity);
        Task<TEntity?> FindProductTypeByIdAsync(TKey id);
        IQueryable<TEntity> GetAllProductTypes();
    }
}
