using StorageSystem.Application.Contracts.Repositories;
using StorageSystem.Application.Contracts.Repositories.Base;
using StorageSystem.Domain.Commons.Interfaces;
using StorageSystem.Infrastructure.Persistence.Contracts.Interfaces;
using StorageSystem.Infrastructure.Repositories.Base;

namespace StorageSystem.Infrastructure.Repositories
{
    public class ConversionSpecProductRepository<TEntity, TKey> : RepositoryBaseAsync<TEntity, TKey>, IConversionSpecProductRepository<TEntity, TKey> where TEntity : class, IEntity<TKey>
    {
        public ConversionSpecProductRepository(IDbContextFactory dbContextFactory, IUnitOfWork unitOfWork) : base(dbContextFactory, unitOfWork)
        {
        }

        public async Task<TKey> CreateConversionSpecProductAsync(TEntity entity)
        {
            return await CreateAsync(entity);
        }

        public async Task DeleteConversionSpecProductAsync(TEntity entity)
        {
            await DeleteAsync(entity);
        }

        public async Task<TEntity?> FindConversionSpecProductByIdAsync(TKey id)
        {
            return await GetByIdAsync(id);
        }

        public IQueryable<TEntity> GetAllConversionSpecProducts()
        {
            throw new NotImplementedException();
        }

        public async Task<TKey> UpdateConversionSpecProductAsync(TEntity entity)
        {
            return await UpdateAsync(entity);
        }
    }
}
