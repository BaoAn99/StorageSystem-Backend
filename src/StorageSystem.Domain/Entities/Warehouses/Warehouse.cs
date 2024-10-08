using StorageSystem.Domain.Commons;
using StorageSystem.Domain.Entities.Storekeepers;
using StorageSystem.Domain.Enums;

namespace StorageSystem.Domain.Entities.Warehouses
{
    public class Warehouse : EntityAuditBase
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string ContactName { get; set; }
        public string Description { get; set; }
    }

    public class WarehouseInbound : EntityAuditBase
    {
        public string Batch { get; set; }
        public double Amount { get; set; }
        public double NetAmount { get; set; }
        public double? DiscountAmount { get; set; }
        public double? DiscountPercent { get; set; }
        public WarehouseInboundStatus Status { get; set; }
        #region Relationship
        public Guid StorekeeperId { get; set; }
        public virtual Storekeeper Storekeeper { get; set; }
        public Guid WarehouseId { get; set; }
        public virtual Warehouse Warehouse { get; set; }
        public virtual ICollection<WarehouseInboundLine> Lines { get; set; }
        #endregion

    }

    public class WarehouseInboundLine : EntityAuditBase
    {
        public Guid ProductId { get; set; }
        public string ProductName { get; set; }
        public Guid UnitId { get; set; }
        public string UnitName { get; set; }
        public string SmallestUnitName { get; set; }
        public int ConvertQuantity { get; set; }
        public int Quantity { get; set; }
        public double Price { get; set; }
        public double NetPrice { get; set; }
        public double UnitPrice { get; set; }
        public double? DiscountAmount { get; set; }
        public double? DiscountPercent { get; set; }
        public string Description { get; set; }
        public WarehouseInboundLineStatus Status { get; set; }
        public Guid WarehouseInboundId { get; set; }
        public virtual WarehouseInbound WarehouseInbound { get; set; }
    }
}
