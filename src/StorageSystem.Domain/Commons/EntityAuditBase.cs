using StorageSystem.Domain.Commons.Interfaces;

namespace StorageSystem.Domain.Commons
{
    public abstract class EntityAuditBase : EntityBase, IEntityAuditBase, IHaveCreatedAt, IHaveCreatedBy, IHaveUpdatedAt, IHaveUpdatedBy, ISoftDelete, IHavePublish
    {
        public DateTimeOffset CreatedAt { get; set; }
        public string CreatedByUserId { get; set; }
        public string CreatedByName { get; set; }
        public DateTimeOffset? UpdatedAt { get; set; }
        public string UpdatedByUserId { get; set; }
        public string UpdatedByName { get; set; }
        public bool IsDeleted { get; set; }
        public bool IsPublished { get; set; }
    }

    public abstract class EntityAuditBase<TKey> : EntityBase<TKey>, IEntityAuditBase<TKey>, IHaveCreatedAt, IHaveCreatedBy, IHaveUpdatedAt, IHaveUpdatedBy, ISoftDelete, IHavePublish
    {
        public DateTimeOffset CreatedAt { get; set; }
        public string CreatedByUserId { get; set; }
        public string CreatedByName { get; set; }
        public DateTimeOffset? UpdatedAt { get; set; }
        public string UpdatedByUserId { get; set; }
        public string UpdatedByName { get; set; }
        public bool IsDeleted { get; set; }
        public bool IsPublished { get; set; }
    }
}
