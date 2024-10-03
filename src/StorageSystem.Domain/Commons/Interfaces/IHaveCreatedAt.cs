namespace StorageSystem.Domain.Commons.Interfaces
{
    public interface IHaveCreatedAt
    {
        DateTimeOffset CreatedAt { get; set; }
    }
}
