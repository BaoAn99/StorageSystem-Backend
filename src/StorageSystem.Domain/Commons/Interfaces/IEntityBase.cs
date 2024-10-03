namespace StorageSystem.Domain.Commons.Interfaces
{
    public interface IEntityBase : IEntity<Guid>
    {
    }

    public interface IEntityBase<TKey> : IEntity<TKey>
    {
    }
}
