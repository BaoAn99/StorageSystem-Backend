using StorageSystem.Application.Contracts.Repositories;
using StorageSystem.Domain.Commons.Interfaces;
using StorageSystem.Infrastructure.Persistence.Contracts.Interfaces;
using StorageSystem.Infrastructure.Repositories.Base;

namespace StorageSystem.Infrastructure.Repositories
{
    public class InvoiceRepository<TEntity, TKey> : RepositoryBaseAsync<TEntity, TKey>, IInvoiceRepository<TEntity, TKey> where TEntity : class, IEntity<TKey>
    {
        public InvoiceRepository(IDbContextFactory dbContextFactory) : base(dbContextFactory)
        {
        }
    }
}
