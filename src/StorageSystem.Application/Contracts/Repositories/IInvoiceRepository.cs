using StorageSystem.Application.Contracts.Repositories.Base;
using StorageSystem.Domain.Commons.Interfaces;

namespace StorageSystem.Application.Contracts.Repositories
{
    public interface IInvoiceRepository<TEntity, TKey> : IRepositoryBaseAsync<TEntity, TKey> where TEntity : IEntity<TKey>
    {
        Task<TKey> CreateInvoiceAsync(TEntity entity);
        Task<TKey> UpdateInvoiceAsync(TEntity entity);
        Task DeleteInvoiceAsync(TEntity entity);
        Task<TEntity?> FindInvoiceByIdAsync(TKey id);
        IQueryable<TEntity> GetAllInvoices();
    }
}
