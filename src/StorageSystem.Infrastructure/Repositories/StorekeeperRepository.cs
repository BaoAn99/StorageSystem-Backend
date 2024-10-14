using StorageSystem.Application.Contracts.Repositories;
using StorageSystem.Domain.Commons.Interfaces;
using StorageSystem.Infrastructure.Persistence.Contracts.Interfaces;
using StorageSystem.Infrastructure.Repositories.Base;

namespace StorageSystem.Infrastructure.Repositories
{
    public class StorekeeperRepository<TEntity, TKey> : RepositoryBaseAsync<TEntity, TKey>, IStorekeeperRepository<TEntity, TKey> where TEntity : class, IEntity<TKey>
    {
        public StorekeeperRepository(IDbContextFactory dbContextFactory) : base(dbContextFactory)
        {
        }
    }
}
