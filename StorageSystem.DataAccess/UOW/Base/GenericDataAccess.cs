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
    internal DbSet<TEntity> dbSet;

    protected GenericDataAccess(
        IApplicationDbContext context
        )
    {
        _context = context;
        dbSet = context.Set<TEntity>();
    }

    public void Delete(object id)
    {
        TEntity entityToDelete = dbSet.Find(id);
        Delete(entityToDelete);
    }

    public void Delete(TEntity entityToDelete)
    {
        dbSet.Remove(entityToDelete);
    }

    public async Task<List<TEntity>> GetsAsync(Expression<Func<TEntity, bool>>? predicate = null)
    {
        var result = _context.Set<TEntity>().AsNoTracking();
        if (predicate != null)
        {
            result = result.Where(predicate);
        }
        return await result.ToListAsync();
    }

    public void Insert(TEntity entity)
    {
        dbSet.Add(entity);
    }

    //public int SaveChanges()
    //{
    //    return _context.SaveChanges();
    //}

    //public Task<int> SaveChangesAsync()
    //{
    //    return _context.SaveChangeAsync();
    //}

    public void Update(TEntity entityToUpdate)
    {
        dbSet.Update(entityToUpdate);
    }
}
