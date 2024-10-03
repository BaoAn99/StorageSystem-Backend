namespace StorageSystem.Domain.Commons.Interfaces
{
    public interface IEntityAuditBase : IEntity<Guid>
    {
    }

    public interface IEntityAuditBase<TKey> : IEntity<TKey>
    {
    }
}
