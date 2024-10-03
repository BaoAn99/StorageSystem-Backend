using StorageSystem.Application.Contracts.Repositories.Base;
using StorageSystem.Domain.Commons.Interfaces;

namespace StorageSystem.Application.Contracts.Repositories
{
    public interface ISupplierRepository<TEntity, TKey> : IRepositoryBaseAsync<TEntity, TKey> where TEntity : IEntity<TKey>
    {
        Task<TKey> CreateSupplierAsync(TEntity entity);
        Task<TKey> UpdateSupplierAsync(TEntity entity);
        Task DeleteSupplierAsync(TEntity entity);
        Task<TEntity?> FindSupplierByIdAsync(TKey id);
        IQueryable<TEntity> GetAllSuppliers();
    }
}
