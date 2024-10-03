namespace StorageSystem.Domain.Commons.Interfaces
{
    public interface IHaveUpdatedAt
    {
        DateTimeOffset? UpdatedAt { get; set;}
    }
}
