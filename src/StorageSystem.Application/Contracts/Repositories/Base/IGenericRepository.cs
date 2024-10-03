using StorageSystem.Domain.Commons.Interfaces;
using System.Linq.Expressions;

namespace StorageSystem.Application.Contracts.Repositories.Base
{
    public interface IRepositoryQueryBase<TEntity, TKey, TContext> where TEntity : IEntity<TKey>
    {
        IQueryable<TEntity> GetAll(bool trackChanges = false);

        IQueryable<TEntity> GetAll(bool trackChanges = false, params Expression<Func<TEntity, object>>[] includeProperties);

        IQueryable<TEntity> FindByCondition(Expression<Func<TEntity, bool>> expression, bool trackChanges = false);

        IQueryable<TEntity> FindByCondition(Expression<Func<TEntity, bool>> expression, bool trackChanges = false, params Expression<Func<TEntity, object>>[] includeProperties);

        Task<TEntity?> GetByIdAsync(TKey id);

        Task<TEntity?> GetByIdAsync(TKey id, params Expression<Func<TEntity, object>>[] includeProperties);
    }

    public interface IRepositoryBaseAsync<TEntity, TKey, TContext> : IRepositoryQueryBase<TEntity, TKey, TContext>
        where TEntity : IEntity<TKey>
    {
        Task<TKey> CreateAsync(TEntity entity);

        Task CreateListAsync(IEnumerable<TEntity> entities);

        Task<TKey> UpdateAsync(TEntity entity);

        Task UpdateListAsync(IEnumerable<TEntity> entities);

        Task DeleteAsync(TEntity entity);

        Task DeleteListAsync(IEnumerable<TEntity> entities);

        Task<int> SaveChangesAsync();

        //Task<IDbContextTransaction> BeginTransactionAsync();

        Task EndTransactionAsync();

        Task RollbackTransactionAsync();
    }

    public interface IRepositoryQueryBase<TEntity, TKey> where TEntity : IEntity<TKey>
    {
        IQueryable<TEntity> GetAll(bool trackChanges = false);

        IQueryable<TEntity> GetAll(bool trackChanges = false, params Expression<Func<TEntity, object>>[] includeProperties);

        IQueryable<TEntity> FindByCondition(Expression<Func<TEntity, bool>> expression, bool trackChanges = false);

        IQueryable<TEntity> FindByCondition(Expression<Func<TEntity, bool>> expression, bool trackChanges = false, params Expression<Func<TEntity, object>>[] includeProperties);

        Task<TEntity?> GetByIdAsync(TKey id);

        Task<TEntity?> GetByIdAsync(TKey id, params Expression<Func<TEntity, object>>[] includeProperties);
    }

    public interface IRepositoryBaseAsync<TEntity, TKey> : IRepositoryQueryBase<TEntity, TKey>
        where TEntity : IEntity<TKey>
    {
        Task<TKey> CreateAsync(TEntity entity);

        Task CreateListAsync(IEnumerable<TEntity> entities);

        Task<TKey> UpdateAsync(TEntity entity);

        Task UpdateListAsync(IEnumerable<TEntity> entities);

        Task DeleteAsync(TEntity entity);

        Task DeleteListAsync(IEnumerable<TEntity> entities);

        Task<int> SaveChangesAsync();

        //Task<IDbContextTransaction> BeginTransactionAsync();

        Task EndTransactionAsync();

        Task RollbackTransactionAsync();
    }
    //public interface IRepositoryBase<T, K, TContext> : IRepositoryQueryBase<T, K, TContext>
    //    where T : EntityBase<K>
    //{
    //    Task<K> Create(T entity);

    //    Task<IList<K>> CreateList(IEnumerable<T> entities);

    //    Task Update(T entity);

    //    Task UpdateList(IEnumerable<T> entities);

    //    Task Delete(T entity);

    //    Task DeleteList(IEnumerable<T> entities);

    //    Task<int> SaveChanges();

    //    //Task<IDbContextTransaction> BeginTransaction();

    //    Task EndTransaction();

    //    Task RollbackTransaction();
    //}
}
