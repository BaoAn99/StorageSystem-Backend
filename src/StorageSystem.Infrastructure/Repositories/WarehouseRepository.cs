using StorageSystem.Application.Contracts.Repositories;
using StorageSystem.Domain.Commons.Interfaces;
using StorageSystem.Infrastructure.Persistence.Contracts.Interfaces;
using StorageSystem.Infrastructure.Repositories.Base;

namespace StorageSystem.Infrastructure.Repositories
{
    public class WarehouseRepository<TEntity, TKey> : RepositoryBaseAsync<TEntity, TKey>, IWarehouseRepository<TEntity, TKey> where TEntity : class, IEntity<TKey>
    {
        public WarehouseRepository(IDbContextFactory dbContextFactory) : base(dbContextFactory)
        {
        }
    }
}
