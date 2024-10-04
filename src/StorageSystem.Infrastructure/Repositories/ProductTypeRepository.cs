using StorageSystem.Application.Contracts.Repositories.Base;
using StorageSystem.Application.Contracts.Repositories;
using StorageSystem.Domain.Commons.Interfaces;
using StorageSystem.Infrastructure.Repositories.Base;
using StorageSystem.Infrastructure.Persistence.Contracts.Interfaces;

namespace StorageSystem.Infrastructure.Repositories
{
    public class ProductTypeRepository<TEntity, TKey> : RepositoryBaseAsync<TEntity, TKey>, IProductTypeRepository<TEntity, TKey> where TEntity : class, IEntity<TKey>
    {
        public ProductTypeRepository(IDbContextFactory dbContextFactory, IUnitOfWork unitOfWork) : base(dbContextFactory, unitOfWork)
        {
        }

        public async Task<TKey> CreateProductTypeAsync(TEntity entity)
        {
            return await CreateAsync(entity);
        }

        public async Task DeleteProductTypeAsync(TEntity entity)
        {
            await DeleteAsync(entity);
        }

        public async Task<TEntity?> FindProductTypeByIdAsync(TKey id)
        {
            return await GetByIdAsync(id);
        }

        public IQueryable<TEntity> GetAllProductTypes()
        {
            throw new NotImplementedException();
        }

        public async Task<TKey> UpdateProductTypeAsync(TEntity entity)
        {
            return await UpdateAsync(entity);
        }
    }
}
