using StorageSystem.Order.Application.Contracts.Repositories;
using StorageSystem.Order.Domain.Commons.Interfaces;
using StorageSystem.Order.Persistence.Contracts.Interfaces;
using StorageSystem.Order.Persistence.Data;
using StorageSystem.Order.Repository.Repositories.Base;

namespace StorageSystem.Order.Repository.Repositories
{
    public class ProductUnitRepository<TEntity, TKey> : RepositoryBaseAsync<TEntity, TKey>, IProductUnitRepository<TEntity, TKey> where TEntity : class, IEntity<TKey>
    {
        public ProductUnitRepository(IDbContextFactory dbContextFactory, ApplicationDbContext context) : base(dbContextFactory, context)
        {
        }
    }
}
