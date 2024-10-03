using StorageSystem.Application.Contracts.Repositories;
using StorageSystem.Application.Contracts.Repositories.Base;
using StorageSystem.Domain.Commons.Interfaces;
using StorageSystem.Infrastructure.Persistence.Contracts.Interfaces;
using StorageSystem.Infrastructure.Repositories.Base;

namespace StorageSystem.Infrastructure.Repositories
{
    public class ProductRepository<TEntity, TKey> : RepositoryBaseAsync<TEntity, TKey>, IProductRepository<TEntity, TKey> where TEntity : class, IEntity<TKey>
    {
        public ProductRepository(IDbContextFactory dbContextFactory, IUnitOfWork unitOfWork) : base(dbContextFactory, unitOfWork)
        {
        }

        public async Task<TKey> CreateProductAsync(TEntity entity)
        {
            return await CreateAsync(entity);
        }   

        public async Task DeleteProductAsync(TEntity entity)
        {
            await DeleteAsync(entity);
        }

        public async Task<TEntity?> FindProductByIdAsync(TKey id)
        {
            return await GetByIdAsync(id);
        }

        public IQueryable<TEntity> GetAllProducts()
        {
            return GetAll();
        }

        public async Task<TKey> UpdateProductAsync(TEntity entity)
        {
            return await UpdateAsync(entity);
        }
    }
}
