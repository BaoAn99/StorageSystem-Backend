namespace StorageSystem.Order.Domain.Commons.Interfaces
{
    public interface ISessionStore
    {
        string GetUserId();
        string GetUserName();
    }
}
