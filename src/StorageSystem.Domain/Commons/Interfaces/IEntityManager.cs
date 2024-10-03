namespace StorageSystem.Domain.Commons.Interfaces
{
    public interface IEntityManager<TEntity> where TEntity : EntityAuditBase
    {
        void SetCreating(TEntity entity);
        void SetUpdating(TEntity entity);
    }
}
