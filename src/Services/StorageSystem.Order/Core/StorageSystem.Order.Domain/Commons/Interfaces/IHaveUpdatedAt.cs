namespace StorageSystem.Order.Domain.Commons.Interfaces
{
    public interface IHaveUpdatedAt
    {
        DateTimeOffset? UpdatedAt { get; set;}
    }
}
