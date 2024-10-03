namespace StorageSystem.Application.Contracts.Repositories.Base
{
    public interface IUnitOfWork
    {
        void BeginTransaction();

        void Commit();

        void RollBack();
    }
}
