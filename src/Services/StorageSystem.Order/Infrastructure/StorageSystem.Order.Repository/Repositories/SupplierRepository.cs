using StorageSystem.Order.Application.Contracts.Repositories;
using StorageSystem.Order.Domain.Commons.Interfaces;
using StorageSystem.Order.Persistence.Contracts.Interfaces;
using StorageSystem.Order.Persistence.Data;
using StorageSystem.Order.Repository.Repositories.Base;

namespace StorageSystem.Order.Repository.Repositories
{
    public class SupplierRepository<TEntity, TKey> : RepositoryBaseAsync<TEntity, TKey>, ISupplierRepository<TEntity, TKey> where TEntity : class, IEntity<TKey>
    {
        public SupplierRepository(IDbContextFactory dbContextFactory, ApplicationDbContext context) : base(dbContextFactory, context)
        {
        }
    }
}
