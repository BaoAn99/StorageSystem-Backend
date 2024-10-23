namespace StorageSystem.Order.Domain.Commons.Interfaces
{
    public interface IEntity
    {
    }

    public interface IEntity<TKey> : IEntity
    {
        TKey Id { get; set; }
    }
}
