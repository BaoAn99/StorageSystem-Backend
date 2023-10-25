using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace StorageSystem.Application.Contracts.DataAccess.Base
{
    public interface IGenericDataAccess<TEntity> where TEntity : class
    {
        Task<List<TEntity>> GetsAsync(Expression<Func<TEntity, bool>>? predicate = null);

        int SaveChanges();

        Task<int> SaveChangesAsync();
    }
}
