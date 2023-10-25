using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StorageSystem.Application.Contracts.DataAccess.Base
{
    public interface IUnitOfWork
    {
        IProductDataAccess ProductDataAccess { get; }

        ICategoryDataAccess CategoryDataAccess { get; }

        int SaveChanges();

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken));
    }
}
