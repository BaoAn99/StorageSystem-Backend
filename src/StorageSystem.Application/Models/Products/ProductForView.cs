namespace StorageSystem.Application.Models.Products
{
    public class ProductForView
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public string? Description { get; set; }
        public string ThumbnailImage { get; set; }

        #region Relationship
        // public Guid TypeId { set; get; }
        public Guid SmallestUnitId { get; set;}
        public string SmallestUnitName { get; set; }
        public List<ConvertUnitProductForView> Units { get; set; }
        public List<ProductImageForView> Images { get; set; }
        #endregion
    }

    public class ProductImageForView
    {
        public Guid Id { get; set; }
        public string ImagePath { get; set; }
        public string Caption { get; set; }
        public bool IsImageFeature { get; set; }
        public string? Description { get; set; }
    }

    public class ConvertUnitProductForView
    {
        public Guid UnitId { get; set; }
        public string UnitName { get; set; }
    }
}
