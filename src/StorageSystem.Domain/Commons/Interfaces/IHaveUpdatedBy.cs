namespace StorageSystem.Domain.Commons.Interfaces
{
    public interface IHaveUpdatedBy
    {
        string UpdatedByUserId { get; set; }
        string UpdatedByName { get; set; }
    }
}
