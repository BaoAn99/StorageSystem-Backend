using Microsoft.EntityFrameworkCore;
using StorageSystem.Application.Contracts.DataAccess.Base;
using StorageSystem.Persistence;
using StorageSystem.Persistence.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace StorageSystem.DataAccess.UOW.Base;

public abstract class GenericDataAccess<TEntity> : IGenericDataAccess<TEntity> where TEntity : class
{
    protected readonly IApplicationDbContext _context;

    protected GenericDataAccess(
        IApplicationDbContext context
        )
    {
        _context = context;
    }

    //public virtual IEnumerable<TEntity> Get(
    //    Expression<Func<TEntity, bool>> filter = null,
    //    Func<IQueryable<TEntity>, IOrderedEnumerable<TEntity>> orderBy = null,
    //    string includeProperties = "")
    //{
    //    IQueryable<TEntity> query = dbSet;
    //    if (filter != null)
    //    {
    //        query = query.Where(filter);
    //    }

    //    foreach (var includeProperty in includeProperties.Split
    //        (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
    //    {
    //        query = query.Include(includeProperty);
    //    }

    //    if (orderBy != null)
    //    {
    //        return orderBy(query).ToList();
    //    }
    //    else
    //    {
    //        return query.ToList();
    //    }
    //}

    //public virtual TEntity GetByID(object id)
    //{
    //    return dbSet.Find(id);
    //}

    //public virtual void Insert(TEntity entity)
    //{
    //    dbSet.Add(entity);
    //}

    //public virtual void Delete(object id)
    //{
    //    TEntity entityToDelete = dbSet.Find(id);
    //    Delete(entityToDelete);
    //}

    //public virtual void Delete(TEntity entityToDelete)
    //{
    //    if (_context.Entry(entityToDelete).State == EntityState.Detached)
    //    {
    //        dbSet.Attach(entityToDelete);
    //    }
    //    dbSet.Remove(entityToDelete);
    //}

    //public virtual void Update(TEntity entityToUpdate)
    //{
    //    dbSet.Attach(entityToUpdate);
    //    _context.Entry(entityToUpdate).State = EntityState.Modified;
    //}

    public async Task<List<TEntity>> GetsAsync(Expression<Func<TEntity, bool>>? predicate = null)
    {
        var result = _context.Set<TEntity>().AsNoTracking();
        if (predicate != null)
        {
            result = result.Where(predicate);
        }
        return await result.ToListAsync();
    }

    public int SaveChanges()
    {
        return _context.SaveChanges();
    }

    public Task<int> SaveChangesAsync()
    {
        return _context.SaveChangeAsync();
    }
}
