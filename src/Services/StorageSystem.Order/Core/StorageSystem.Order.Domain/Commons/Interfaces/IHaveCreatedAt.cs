namespace StorageSystem.Order.Domain.Commons.Interfaces
{
    public interface IHaveCreatedAt
    {
        DateTimeOffset CreatedAt { get; set; }
    }
}
