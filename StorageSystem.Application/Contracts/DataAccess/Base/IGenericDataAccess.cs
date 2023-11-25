using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace StorageSystem.Application.Contracts.DataAccess.Base;

public interface IGenericDataAccess<TEntity> where TEntity : class
{
    Task<List<TEntity>> GetsAsync(Expression<Func<TEntity, bool>>? predicate = null);

    void Insert(TEntity entity);

    void Update(TEntity entityToUpdate);

    void Delete(object id);
}