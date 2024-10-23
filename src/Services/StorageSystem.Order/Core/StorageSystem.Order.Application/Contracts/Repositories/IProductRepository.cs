using StorageSystem.Order.Application.Contracts.Repositories.Base;
using StorageSystem.Order.Domain.Commons.Interfaces;

namespace StorageSystem.Order.Application.Contracts.Repositories
{
    public interface IProductRepository<TEntity, TKey> : IRepositoryBaseAsync<TEntity, TKey> where TEntity : IEntity<TKey>
    {
    }
}
