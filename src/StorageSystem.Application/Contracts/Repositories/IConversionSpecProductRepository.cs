using StorageSystem.Application.Contracts.Repositories.Base;
using StorageSystem.Domain.Commons.Interfaces;

namespace StorageSystem.Application.Contracts.Repositories
{
    public interface IConversionSpecProductRepository<TEntity, TKey> : IRepositoryBaseAsync<TEntity, TKey> where TEntity : IEntity<TKey>
    {
        Task<TKey> CreateConversionSpecProductAsync(TEntity entity);
        Task<TKey> UpdateConversionSpecProductAsync(TEntity entity);
        Task DeleteConversionSpecProductAsync(TEntity entity);
        Task<TEntity?> FindConversionSpecProductByIdAsync(TKey id);
        IQueryable<TEntity> GetAllConversionSpecProducts();
    }
}
