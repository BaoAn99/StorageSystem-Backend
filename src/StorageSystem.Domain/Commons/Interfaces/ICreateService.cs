namespace StorageSystem.Domain.Commons.Interfaces
{
    public interface ICreateService<TEntity, TKey> where TEntity : class, IEntity<TKey>
    {
        TEntity Create<T>(T input);
    }
}
