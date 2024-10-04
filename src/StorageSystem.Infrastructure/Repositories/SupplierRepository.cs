using StorageSystem.Application.Contracts.Repositories;
using StorageSystem.Application.Contracts.Repositories.Base;
using StorageSystem.Domain.Commons.Interfaces;
using StorageSystem.Infrastructure.Persistence.Contracts.Interfaces;
using StorageSystem.Infrastructure.Repositories.Base;

namespace StorageSystem.Infrastructure.Repositories
{
    public class SupplierRepository<TEntity, TKey> : RepositoryBaseAsync<TEntity, TKey>, ISupplierRepository<TEntity, TKey> where TEntity : class, IEntity<TKey>
    {
        public SupplierRepository(IDbContextFactory dbContextFactory, IUnitOfWork unitOfWork) : base(dbContextFactory, unitOfWork)
        {
        }

        public async Task<TKey> CreateSupplierAsync(TEntity entity)
        {
            return await CreateAsync(entity);
        }

        public async Task DeleteSupplierAsync(TEntity entity)
        {
            await DeleteAsync(entity);
        }

        public async Task<TEntity?> FindSupplierByIdAsync(TKey id)
        {
            return await GetByIdAsync(id);
        }

        public IQueryable<TEntity> GetAllSuppliers()
        {
            throw new NotImplementedException();
        }

        public async Task<TKey> UpdateSupplierAsync(TEntity entity)
        {
            return await UpdateAsync(entity);
        }
    }
}
