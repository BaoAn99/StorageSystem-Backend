using StorageSystem.Order.Domain.Commons;
using StorageSystem.Order.Domain.Entities.Employees;
using StorageSystem.Order.Domain.Enums;

namespace StorageSystem.Order.Domain.Entities.Cashiers
{
    public class Cashier : EntityAuditBase
    {
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public DateTimeOffset Birthdate { get; set; }
        public string IDCard { get; set; }
        public Gender Gender { get; set; }
        public string Description { get; set; }

        #region Relationship
        public Guid EmployeeId { get; set; }
        public virtual Employee Employee { get; set; }
        #endregion
    }
}
