using StorageSystem.Application.Contracts.Repositories;
using StorageSystem.Domain.Commons.Interfaces;
using StorageSystem.Infrastructure.Persistence;
using StorageSystem.Infrastructure.Persistence.Contracts.Interfaces;
using StorageSystem.Infrastructure.Repositories.Base;

namespace StorageSystem.Infrastructure.Repositories
{
    public class WarehouseInboundRepository<TEntity, TKey> : RepositoryBaseAsync<TEntity, TKey>, IWarehouseInboundRepository<TEntity, TKey> where TEntity : class, IEntity<TKey>
    {
        public WarehouseInboundRepository(IDbContextFactory dbContextFactory, ApplicationDbContext context) : base(dbContextFactory, context)
        {
        }
    }
}
