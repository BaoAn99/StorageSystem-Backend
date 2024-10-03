using StorageSystem.Domain.Commons;

namespace StorageSystem.Domain.Entities.Suppliers
{
    public class Supplier : EntityAuditBase
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string ContactName { get; set; }
        public string Description { get; set; }
    }
}
