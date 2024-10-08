using Microsoft.EntityFrameworkCore;
using StorageSystem.Application.Contracts.Repositories;
using StorageSystem.Domain.Commons.Interfaces;
using StorageSystem.Infrastructure.Persistence.Contracts.Interfaces;
using StorageSystem.Infrastructure.Repositories.Base;

namespace StorageSystem.Infrastructure.Repositories
{
    public class ProductTypeRepository<TEntity, TKey> : RepositoryBaseAsync<TEntity, TKey>, IProductTypeRepository<TEntity, TKey> where TEntity : class, IEntity<TKey>
    {
        public ProductTypeRepository(IDbContextFactory dbContextFactory) : base(dbContextFactory)
        {
        }
    }
}
