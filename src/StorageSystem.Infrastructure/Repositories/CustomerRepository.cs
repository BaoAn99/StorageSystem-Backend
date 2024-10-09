using StorageSystem.Application.Contracts.Repositories;
using StorageSystem.Domain.Commons.Interfaces;
using StorageSystem.Infrastructure.Persistence.Contracts.Interfaces;
using StorageSystem.Infrastructure.Repositories.Base;

namespace StorageSystem.Infrastructure.Repositories
{
    public class CustomerRepository<TEntity, TKey> : RepositoryBaseAsync<TEntity, TKey>, ICustomerRepository<TEntity, TKey> where TEntity : class, IEntity<TKey>
    {
        public CustomerRepository(IDbContextFactory dbContextFactory) : base(dbContextFactory)
        {
        }
    }
}