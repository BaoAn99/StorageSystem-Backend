using StorageSystem.Application.Contracts.DataAccess;
using StorageSystem.Application.Contracts.DataAccess.Base;
using StorageSystem.Persistence.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StorageSystem.DataAccess.UOW.Base
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly IApplicationDbContext _context;

        public IProductDataAccess ProductDataAccess { get; }

        public ICategoryDataAccess CategoryDataAccess { get; }

        public UnitOfWork(
            IProductDataAccess productDataAccess,
            ICategoryDataAccess categoryDataAccess) 
        {
            ProductDataAccess = productDataAccess;
            CategoryDataAccess = categoryDataAccess;
        }

        public int SaveChanges()
        {
            return _context.SaveChanges();
        }

        public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return await _context.SaveChangeAsync(cancellationToken);
        }
    }
}
