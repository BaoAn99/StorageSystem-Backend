namespace StorageSystem.Order.Domain.Commons.Interfaces
{
    public interface IHaveCreatedBy
    {
        string CreatedByUserId { get; set; }
        string CreatedByName { get; set; }
    }
}
