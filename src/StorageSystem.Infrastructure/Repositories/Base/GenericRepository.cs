using Microsoft.EntityFrameworkCore;
using StorageSystem.Application.Contracts.Repositories.Base;
using StorageSystem.Domain.Commons.Interfaces;
using StorageSystem.Infrastructure.Persistence.Contracts.Interfaces;
using System.Linq.Expressions;

namespace StorageSystem.Infrastructure.Repositories.Base
{
    public abstract class RepositoryBaseAsync<TEntity, TKey, TContext> : IRepositoryBaseAsync<TEntity, TKey, TContext> where TEntity : class, IEntity<TKey> where TContext : DbContext
    {
        private readonly TContext _dbContext;
        //private readonly IUnitOfWork<TContext> _unitOfWork;

        public RepositoryBaseAsync(TContext dbContext)
        {
            _dbContext = _dbContext ?? throw new ArgumentNullException(nameof(dbContext));
            //_unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }

        public IQueryable<TEntity> GetAll(bool trackChanges = false) =>
            !trackChanges ? _dbContext.Set<TEntity>().AsNoTracking() : _dbContext.Set<TEntity>();

        public IQueryable<TEntity> GetAll(bool trackChanges = false, params Expression<Func<TEntity, object>>[] includeProperties)
        {
            var items = GetAll(trackChanges);
            items = includeProperties.Aggregate(items, (current, includeProperty) => current.Include(includeProperty));
            return items;
        }

        public IQueryable<TEntity> FindByCondition(Expression<Func<TEntity, bool>> expression, bool trackChanges = false) =>
            !trackChanges ? _dbContext.Set<TEntity>().Where(expression).AsNoTracking() : _dbContext.Set<TEntity>().Where(expression);

        public IQueryable<TEntity> FindByCondition(Expression<Func<TEntity, bool>> expression, bool trackChanges = false,
            params Expression<Func<TEntity, object>>[] includeProperties)
        {
            var items = FindByCondition(expression, trackChanges);
            items = includeProperties.Aggregate(items, (current, includeProperty) => current.Include(includeProperty));
            return items;
        }

        public async Task<TEntity?> GetByIdAsync(TKey id) => await FindByCondition(x => x.Id!.Equals(id)).FirstOrDefaultAsync();

        public async Task<TEntity?> GetByIdAsync(TKey id, params Expression<Func<TEntity, object>>[] includeProperties) =>
            await FindByCondition(x => x.Id!.Equals(id), trackChanges: false, includeProperties).FirstOrDefaultAsync();

        public async Task<TKey> CreateAsync(TEntity entity)
        {
            await _dbContext.Set<TEntity>().AddAsync(entity);
            return entity.Id;
        }

        public async Task CreateListAsync(IEnumerable<TEntity> entities)
        {
            await _dbContext.Set<TEntity>().AddRangeAsync(entities);
        }

        public Task<TKey> UpdateAsync(TEntity entity)
        {
            //if (_dbContext.Entry(entity).State == EntityState.Unchanged) return Task.CompletedTask;

            //TEntity exist = _dbContext.Set<TEntity>().Find(entity.Id);
            //_dbContext.Entry(exist).CurrentValues.SetValues(entity);
            _dbContext.Set<TEntity>().Update(entity);
            return Task.FromResult(entity.Id);
        }

        public Task UpdateListAsync(IEnumerable<TEntity> entities)
        {
            _dbContext.Set<TEntity>().UpdateRange(entities);
            return Task.CompletedTask;
        }

        public Task DeleteAsync(TEntity entity)
        {
            _dbContext.Set<TEntity>().Remove(entity);
            return Task.CompletedTask;
        }

        public Task DeleteListAsync(IEnumerable<TEntity> entities)
        {
            _dbContext.Set<TEntity>().RemoveRange(entities);
            return Task.CompletedTask;
        }

        //public int SaveChanges() => _unitOfWork.Commit();
        //public Task<int> SaveChangesAsync() => _unitOfWork.CommitAsync();

        //public async Task<IDbContextTransaction> BeginTransactionAsync() => await _dbContext.Database.BeginTransactionAsync();

        //public async Task EndTransactionAsync() => await _unitOfWork.EndTransactionAsync();

        //public async Task RollbackTransactionAsync() => await _unitOfWork.RollBackTransactionAsync();
    }

    public class RepositoryBaseAsync<TEntity, TKey> : IRepositoryBaseAsync<TEntity, TKey> where TEntity : class, IEntity<TKey>
    {
        private readonly IDbContextFactory _dbContextFactory;
        private readonly DbContext _dbContext;

        public RepositoryBaseAsync(IDbContextFactory dbContextFactory)
        {
            _dbContextFactory = dbContextFactory;
            _dbContext = _dbContextFactory.Create();
        }

        public IQueryable<TEntity> GetAll(bool trackChanges = false) =>
            !trackChanges ? _dbContext.Set<TEntity>().AsNoTracking() : _dbContext.Set<TEntity>();

        public IQueryable<TEntity> GetAll(bool trackChanges = false, params Expression<Func<TEntity, object>>[] includeProperties)
        {
            var items = GetAll(trackChanges);
            items = includeProperties.Aggregate(items, (current, includeProperty) => current.Include(includeProperty));
            return items;
        }

        public IQueryable<TEntity> FindByCondition(Expression<Func<TEntity, bool>> expression, bool trackChanges = false) =>
            !trackChanges ? _dbContext.Set<TEntity>().Where(expression).AsNoTracking() : _dbContext.Set<TEntity>().Where(expression);

        public IQueryable<TEntity> FindByCondition(Expression<Func<TEntity, bool>> expression, bool trackChanges = false,
            params Expression<Func<TEntity, object>>[] includeProperties)
        {
            var items = FindByCondition(expression, trackChanges);
            items = includeProperties.Aggregate(items, (current, includeProperty) => current.Include(includeProperty));
            return items;
        }

        public async Task<TEntity?> GetByIdAsync(TKey id) => await FindByCondition(x => x.Id!.Equals(id)).FirstOrDefaultAsync();

        public async Task<TEntity?> GetByIdAsync(TKey id, params Expression<Func<TEntity, object>>[] includeProperties) =>
            await FindByCondition(x => x.Id!.Equals(id), trackChanges: false, includeProperties).FirstOrDefaultAsync();

        public async Task<TKey> CreateAsync(TEntity entity)
        {
            await _dbContext.Set<TEntity>().AddAsync(entity);
            return entity.Id;
        }

        public async Task CreateListAsync(IEnumerable<TEntity> entities)
        {
            await _dbContext.Set<TEntity>().AddRangeAsync(entities);
        }

        public Task<TKey> UpdateAsync(TEntity entity)
        {
            //if (_dbContext.Entry(entity).State == EntityState.Unchanged) return Task.CompletedTask;

            //TEntity exist = _dbContext.Set<TEntity>().Find(entity.Id);
            //_dbContext.Entry(exist).CurrentValues.SetValues(entity);
            _dbContext.Set<TEntity>().Update(entity);
            return Task.FromResult(entity.Id);
        }

        public Task UpdateListAsync(IEnumerable<TEntity> entities)
        {
            _dbContext.Set<TEntity>().UpdateRange(entities);
            return Task.CompletedTask;
        }

        public Task DeleteAsync(TEntity entity)
        {
            _dbContext.Set<TEntity>().Remove(entity);
            return Task.CompletedTask;
        }

        public Task DeleteListAsync(IEnumerable<TEntity> entities)
        {
            _dbContext.Set<TEntity>().RemoveRange(entities);
            return Task.CompletedTask;
        }

        //public object GetAllLookup(QueryParams queryParams)
        //{
        //    IQueryable<TEntity> query = 

        //    return query;
        //}

        //public int SaveChanges() => _unitOfWork.Commit();
        //public Task<int> SaveChangesAsync() => _unitOfWork.CommitAsync();

        //public async Task<IDbContextTransaction> BeginTransactionAsync() => await _dbContext.Database.BeginTransactionAsync();

        //public async Task EndTransactionAsync() => await _unitOfWork.EndTransactionAsync();

        //public async Task RollbackTransactionAsync() => await _unitOfWork.RollBackTransactionAsync();
    }
}
