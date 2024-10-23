using StorageSystem.Order.Application.Contracts.Repositories;
using StorageSystem.Order.Domain.Commons.Interfaces;
using StorageSystem.Order.Persistence.Contracts.Interfaces;
using StorageSystem.Order.Persistence.Data;
using StorageSystem.Order.Repository.Repositories.Base;

namespace StorageSystem.Order.Repository.Repositories
{
    public class ConversionSpecProductRepository<TEntity, TKey> : RepositoryBaseAsync<TEntity, TKey>, IConversionSpecProductRepository<TEntity, TKey> where TEntity : class, IEntity<TKey>
    {
        public ConversionSpecProductRepository(IDbContextFactory dbContextFactory, ApplicationDbContext context) : base(dbContextFactory, context)
        {
        }
    }
}
