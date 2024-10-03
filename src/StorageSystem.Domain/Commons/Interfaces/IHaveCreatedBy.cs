namespace StorageSystem.Domain.Commons.Interfaces
{
    public interface IHaveCreatedBy
    {
        string CreatedByUserId { get; set; }
        string CreatedByName { get; set; }
    }
}
