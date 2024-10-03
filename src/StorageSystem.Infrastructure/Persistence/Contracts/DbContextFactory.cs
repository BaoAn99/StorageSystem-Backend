using Microsoft.EntityFrameworkCore;
using StorageSystem.Infrastructure.Persistence.Contracts.Interfaces;

namespace StorageSystem.Infrastructure.Persistence.Contracts
{
    public class DbContextFactory<TDbContext> : Interfaces.IDbContextFactory<TDbContext>, IDbContextFactory, IDisposable where TDbContext : DbContext
    {
        private bool _disposedValue;

        private readonly TDbContext _context;

        public DbContextFactory(TDbContext context)
        {
            _context = context;
        }

        public DbContext Create()
        {
            return _context;
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposedValue)
            {
                _disposedValue = true;
            }
        }

        public void Dispose()
        {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}
