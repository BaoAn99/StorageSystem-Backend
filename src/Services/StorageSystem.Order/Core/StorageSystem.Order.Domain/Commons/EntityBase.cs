using StorageSystem.Order.Domain.Commons.Interfaces;

namespace StorageSystem.Order.Domain.Commons
{
    public abstract class EntityBase : IEntityBase
    {
        public Guid Id { get; set; }
    }

    public abstract class EntityBase<TKey> : IEntityBase<TKey>
    {
        public TKey Id { get; set; }
    }
}
