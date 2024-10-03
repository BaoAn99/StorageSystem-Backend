using Microsoft.EntityFrameworkCore;

namespace StorageSystem.Infrastructure.Persistence.Contracts.Interfaces
{
    public interface IDbContextFactory : IDisposable
    {
        DbContext Create();
    }

    public interface IDbContextFactory<TDbContext> : IDbContextFactory, IDisposable
    {
    }
}
