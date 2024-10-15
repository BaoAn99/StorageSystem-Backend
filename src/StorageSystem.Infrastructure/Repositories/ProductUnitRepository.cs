using StorageSystem.Application.Contracts.Repositories;
using StorageSystem.Domain.Commons.Interfaces;
using StorageSystem.Infrastructure.Persistence;
using StorageSystem.Infrastructure.Persistence.Contracts.Interfaces;
using StorageSystem.Infrastructure.Repositories.Base;

namespace StorageSystem.Infrastructure.Repositories
{
    public class ProductUnitRepository<TEntity, TKey> : RepositoryBaseAsync<TEntity, TKey>, IProductUnitRepository<TEntity, TKey> where TEntity : class, IEntity<TKey>
    {
        public ProductUnitRepository(IDbContextFactory dbContextFactory, ApplicationDbContext context) : base(dbContextFactory, context)
        {
        }
    }
}
