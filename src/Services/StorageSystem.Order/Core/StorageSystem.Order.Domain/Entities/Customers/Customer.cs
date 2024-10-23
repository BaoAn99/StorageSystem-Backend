using StorageSystem.Order.Domain.Commons;
using StorageSystem.Order.Domain.Enums;

namespace StorageSystem.Order.Domain.Entities.Customers
{
    public class Customer : EntityAuditBase
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string Description { get; set; }
        public Gender? Gender { get; set; }
        public DateTimeOffset? Birthdate { get; set; }

        #region Relationship
        //public Guid? OrderId { get; set; }
        #endregion
    }
}
