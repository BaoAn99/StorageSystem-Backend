using StorageSystem.Domain.Commons;
using StorageSystem.Domain.Entities.Suppliers;

namespace StorageSystem.Domain.Entities.Products
{
    public class Product : EntityAuditBase
    {
        public string Name { set; get; }
        public double Price { set; get; }
        // public double OriginalPrice { set; get; }
        public string Description { set; get; }

        //Thiếu field nhập số lượng cho phép thông báo khi sắp hết hàng (Inventory
        public string ThumbnailImage { get; set; }

        #region Relationship
        public Guid TypeId { set; get; }
        public virtual ProductType Type { get; set; }
        public Guid SmallestUnitId { set; get; }
        public virtual ProductUnit SmallestUnit { get; set; }
        public virtual ICollection<ProductImage> Images { get; set; }
        #endregion
    }

    public class ProductType : EntityAuditBase
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }

    public class ProductUnit : EntityAuditBase
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }

    public class ProductImage : EntityAuditBase
    {
        public string ImagePath { get; set; }
        public string Caption { get; set; }
        public bool IsImageFeature { get; set; }
        public string Description { get; set; }

        #region Relationship
        public Guid ProductId { get; set; }
        public virtual Product Product { get; set; }
        #endregion
    }

    public class ProductSupplier : EntityAuditBase
    {
        public Guid SupplierId { set; get; }
        public virtual Supplier Supplier { get; set; }                            
        public Guid ProductId { set; get; }
        public virtual Product Product { get; set; }
    }
}
