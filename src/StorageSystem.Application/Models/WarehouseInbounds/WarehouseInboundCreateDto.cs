using StorageSystem.Domain.Enums;

namespace StorageSystem.Application.Models.WarehouseInbounds
{
    public class WarehouseInboundCreateDto
    {
        public string Batch { get; set; }
        public double Amount { get; set; }
        public double NetAmount { get; set; }
        public double? DiscountAmount { get; set; }
        public double? DiscountPercent { get; set; }
        public WarehouseInboundStatus Status { get; set; }
        #region Relationship
        public Guid? StorekeeperId { get; set; }
        public Guid? SupplierId { get; set; }
        public Guid WarehouseId { get; set; }
        public List<WarehouseInboundLineCreateDto> Lines { get; set; }
        #endregion

    }

    public class WarehouseInboundLineCreateDto
    {
        public Guid ProductId { get; set; }
        public string ProductName { get; set; }
        public Guid UnitId { get; set; }
        public string UnitName { get; set; }
        //public string SmallestUnitName { get; set; }
        //public int ConvertQuantity { get; set; }
        public int Quantity { get; set; }
        public double UnitPrice { get; set; }
        public double? DiscountAmount { get; set; }
        public double? DiscountPercent { get; set; }
        public string? Description { get; set; }
        public WarehouseInboundLineStatus Status { get; set; }
        public Guid WarehouseInboundId { get; set; }
    }
}
