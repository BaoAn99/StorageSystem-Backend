using StorageSystem.Domain.Commons;
using StorageSystem.Domain.Enums;

namespace StorageSystem.Domain.Entities.Employees
{
    public class Employee : EntityAuditBase
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
        public Guid TypeId { get; set; }
        public virtual EmployeeType Type { get; set; }
        #endregion
    }

    public class EmployeeType : EntityAuditBase
    {
        public string Name { get; set; }
        public const string Storekeeper = "214c787c-9a51-42d5-ba36-b5b2a420d1af";
        public const string Cashier = "98d13611-3de0-4a43-b44f-700c27f8ea6b";
    }
}
