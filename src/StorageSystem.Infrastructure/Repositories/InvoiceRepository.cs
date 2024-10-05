using StorageSystem.Application.Contracts.Repositories;
using StorageSystem.Application.Contracts.Repositories.Base;
using StorageSystem.Domain.Commons.Interfaces;
using StorageSystem.Infrastructure.Persistence.Contracts.Interfaces;
using StorageSystem.Infrastructure.Repositories.Base;

namespace StorageSystem.Infrastructure.Repositories
{
    public class InvoiceRepository<TEntity, TKey> : RepositoryBaseAsync<TEntity, TKey>, IInvoiceRepository<TEntity, TKey> where TEntity : class, IEntity<TKey>
    {
        public InvoiceRepository(IDbContextFactory dbContextFactory, IUnitOfWork unitOfWork) : base(dbContextFactory, unitOfWork)
        {
        }

        public async Task<TKey> CreateInvoiceAsync(TEntity entity)
        {
            return await CreateAsync(entity);
        }

        public async Task DeleteInvoiceAsync(TEntity entity)
        {
            await DeleteAsync(entity);
        }

        public async Task<TEntity?> FindInvoiceByIdAsync(TKey id)
        {
            return await GetByIdAsync(id);
        }

        public IQueryable<TEntity> GetAllInvoices()
        {
            throw new NotImplementedException();
        }

        public async Task<TKey> UpdateInvoiceAsync(TEntity entity)
        {
            return await UpdateAsync(entity);
        }
    }
}
