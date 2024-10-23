namespace StorageSystem.Order.Domain.Commons.Interfaces
{
    public interface ISoftDelete
    {
        bool IsDeleted { get; set; }
    }
}
