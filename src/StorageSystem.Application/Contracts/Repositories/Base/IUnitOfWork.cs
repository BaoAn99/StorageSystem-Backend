namespace StorageSystem.Application.Contracts.Repositories.Base
{
    public interface IUnitOfWork
    {
        Task BeginTransactionAsync();
        Task EndTransactionAsync();
        int Commit();
        Task<int> CommitAsync();
        Task RollBackTransactionAsync();
    }

    public interface IUnitOfWork<TDbContext>
    {
        Task BeginTransactionAsync();
        Task EndTransactionAsync();
        int Commit();
        Task<int> CommitAsync();
        Task RollBackTransactionAsync();
    }
}
