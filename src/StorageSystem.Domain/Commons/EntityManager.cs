using StorageSystem.Domain.Commons.Interfaces;

namespace StorageSystem.Domain.Commons
{
    public class EntityManager<TEntity> : IEntityManager<TEntity> where TEntity : EntityAuditBase
    {
        private readonly ISessionStore _sessionStore;

        public EntityManager(ISessionStore sessionStore)
        {
            _sessionStore = sessionStore;
        }

        public void SetCreating(TEntity entity)
        {
            entity.CreatedAt = DateTimeOffset.Now;
            entity.CreatedByUserId = _sessionStore.GetUserId();
            entity.CreatedByName = _sessionStore.GetUserName();
            entity.IsDeleted = false;
            entity.IsPublished = true;
        }

        public void SetUpdating(TEntity entity)
        {
            entity.UpdatedAt = DateTimeOffset.Now;
            entity.UpdatedByUserId = _sessionStore.GetUserId();
            entity.UpdatedByName = _sessionStore.GetUserName();
        }
    }
}
