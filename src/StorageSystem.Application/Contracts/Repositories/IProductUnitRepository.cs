using StorageSystem.Application.Contracts.Repositories.Base;
using StorageSystem.Domain.Commons.Interfaces;

namespace StorageSystem.Application.Contracts.Repositories
{
    public interface IProductUnitRepository<TEntity, TKey> : IRepositoryBaseAsync<TEntity, TKey> where TEntity : IEntity<TKey>
    {
        Task<TKey> CreateProductUnitAsync(TEntity entity);
        Task<TKey> UpdateProductUnitAsync(TEntity entity);
        Task DeleteProductUnitAsync(TEntity entity);
        Task<TEntity?> FindProductUnitByIdAsync(TKey id);
        IQueryable<TEntity> GetAllProductUnits();
    }
}
