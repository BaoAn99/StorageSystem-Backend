using StorageSystem.Application.Contracts.Repositories;
using StorageSystem.Domain.Commons.Interfaces;
using StorageSystem.Infrastructure.Persistence.Contracts.Interfaces;
using StorageSystem.Infrastructure.Repositories.Base;

namespace StorageSystem.Infrastructure.Repositories
{
    public class ConversionSpecProductRepository<TEntity, TKey> : RepositoryBaseAsync<TEntity, TKey>, IConversionSpecProductRepository<TEntity, TKey> where TEntity : class, IEntity<TKey>
    {
        public ConversionSpecProductRepository(IDbContextFactory dbContextFactory) : base(dbContextFactory)
        {
        }
    }
}
