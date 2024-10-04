using StorageSystem.Application.Contracts.Repositories;
using StorageSystem.Application.Contracts.Repositories.Base;
using StorageSystem.Domain.Commons.Interfaces;
using StorageSystem.Infrastructure.Persistence.Contracts.Interfaces;
using StorageSystem.Infrastructure.Repositories.Base;

namespace StorageSystem.Infrastructure.Repositories
{
    public class ProductUnitRepository<TEntity, TKey> : RepositoryBaseAsync<TEntity, TKey>, IProductUnitRepository<TEntity, TKey> where TEntity : class, IEntity<TKey>
    {
        public ProductUnitRepository(IDbContextFactory dbContextFactory, IUnitOfWork unitOfWork) : base(dbContextFactory, unitOfWork)
        {
        }

        public async Task<TKey> CreateProductUnitAsync(TEntity entity)
        {
            return await CreateAsync(entity);
        }

        public async Task DeleteProductUnitAsync(TEntity entity)
        {
            await DeleteAsync(entity);
        }

        public async Task<TEntity?> FindProductUnitByIdAsync(TKey id)
        {
            return await GetByIdAsync(id);
        }

        public IQueryable<TEntity> GetAllProductUnits()
        {
            throw new NotImplementedException();
        }

        public async Task<TKey> UpdateProductUnitAsync(TEntity entity)
        {
            return await UpdateAsync(entity);
        }
    }
}
