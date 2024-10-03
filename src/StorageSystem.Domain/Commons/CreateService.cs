using StorageSystem.Domain.Commons.Interfaces;

namespace StorageSystem.Domain.Commons
{
    public abstract class CreateService<TEntity, TKey> : ICreateService<TEntity, TKey> where TEntity : class, IEntity<TKey>
    {
        public TEntity Create<T>(T input)
        {
            TEntity val = Activator.CreateInstance<TEntity>();
                
            throw new NotImplementedException();
        }
    }
}
