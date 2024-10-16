using Microsoft.EntityFrameworkCore;
using storagesystem.domain.commons;
using StorageSystem.Application.Contracts.Repositories.Base;
using StorageSystem.Application.Extensions;
using StorageSystem.Domain.Commons;
using StorageSystem.Domain.Commons.Interfaces;
using StorageSystem.Domain.Entities.Products;
using StorageSystem.Infrastructure.Persistence;
using StorageSystem.Infrastructure.Persistence.Contracts.Interfaces;
using System.Linq.Expressions;
using System.Transactions;

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
        private DbContext _dbContext;
        private ApplicationDbContext _context;

        public RepositoryBaseAsync(IDbContextFactory dbContextFactory, ApplicationDbContext context)
        {
            _dbContextFactory = dbContextFactory;
            _dbContext = _dbContextFactory.Create();
            _context = context;
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
            Console.WriteLine("3: " + Environment.CurrentManagedThreadId);
            Console.WriteLine("");
            await _dbContext.Set<TEntity>().AddAsync(entity);
            Console.WriteLine("4: " + Environment.CurrentManagedThreadId);
            Console.WriteLine("");
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

        public IQueryable<TEntity> GetAll(QueryParams queryParams)
        {
            var query = GetAll();
            query = query.Build(queryParams);
            return query;
        }

        public IQueryable<TEntity> GetAllWithoutPaging(QueryParamsWithoutPaging queryParams)
        {
            throw new NotImplementedException();
        }

        public void Test()
        {
            using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required,
                                   new System.TimeSpan(0, 60, 0)))
            {
                try
                {
                    //_context.ChangeTracker.AutoDetectChangesEnabled = false;

                    int count = 0;
                    foreach (var entityToInsert in CsvFileExtension.Import<TEntity>("D:\\Freelancer\\hai-thuan-store\\StorageSystem-Backend\\src\\StorageSystem.Api\\ProductType.csv"))
                    {
                        ++count;
                        _dbContext = AddToContext(_dbContext, entityToInsert, count, 4900, true);
                        //if (count % 500 == 0)
                        //{
                        //    _dbContext.Dispose();
                        //    _dbContext = _context;
                        //}
                    }

                    _dbContext.SaveChanges();
                }
                finally
                {
                    if (_dbContext != null)
                        _dbContext.Dispose();
                }

                scope.Complete();
            }
        }

        private DbContext AddToContext(DbContext context, TEntity entity, int count, int commitCount, bool recreateContext)
        {
            context.Set<TEntity>().Add(entity);
            //context.SaveChanges();
            if (count % commitCount == 0)
            {
                try
                {
                    context.SaveChanges();
                    if (recreateContext)
                    {
                        //_context.ChangeTracker.AutoDetectChangesEnabled = false;
                    }
                }
                catch (Exception ex)
                {
                    var a = ex.Message;
                    var b = ex.StackTrace;
                    var c = count;
                    var d = commitCount;
                    throw;
                }
            }

            return context;
        }

        //public int SaveChanges() => _unitOfWork.Commit();
        //public Task<int> SaveChangesAsync() => _unitOfWork.CommitAsync();

        //public async Task<IDbContextTransaction> BeginTransactionAsync() => await _dbContext.Database.BeginTransactionAsync();

        //public async Task EndTransactionAsync() => await _unitOfWork.EndTransactionAsync();

        //public async Task RollbackTransactionAsync() => await _unitOfWork.RollBackTransactionAsync();
    }
}
