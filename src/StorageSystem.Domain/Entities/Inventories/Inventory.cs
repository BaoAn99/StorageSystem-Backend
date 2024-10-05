using StorageSystem.Domain.Commons;
using StorageSystem.Domain.Entities.Warehouses;

namespace StorageSystem.Domain.Entities.Inventories
{
    public class Inventory : EntityAuditBase
    {
        public string Batch { get; set; }
        public DateTimeOffset PeriodDate { get; set; }
        public Guid UnitId { get; set; }
        public string UnitName { get; set; }
        public Guid ProductId { get; set; }
        public string ProductName { get; set; }
        public int Stock { get; set; }
        public int? MaxStock { get; set; }
        public int MinStock { get; set; }
        public string WarehouseName { get; set; }
        #region Relationship
        public Guid WarehouseId { get; set; }
        public virtual Warehouse Warehouse { get; set; }
        #endregion
    }
}
