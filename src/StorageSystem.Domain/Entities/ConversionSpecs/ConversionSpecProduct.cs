using StorageSystem.Domain.Commons;
using StorageSystem.Domain.Entities.Products;

namespace StorageSystem.Domain.Entities.PackageSpecs
{
    public class ConversionSpecProduct : EntityAuditBase
    {
        public int Quantity { get; set; }
        public Guid ProductId { get; set; }
        public virtual Product Product { get; set; }
        public Guid UnitId { get; set; }
        public string UnitName { get; set; }
        public Guid ConvertUnitId { get; set; }
        public string ConvertUnitName { get; set; }
    }
}
