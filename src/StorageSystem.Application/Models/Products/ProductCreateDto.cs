namespace StorageSystem.Application.Models.Products
{
    public class ProductCreateDto
    {
        public string Name { set; get; }
        public double Price { set; get; }
        public string? Description { set; get; }
        public string ThumbnailImage { get; set; }

        #region Relationship
        public Guid TypeId { set; get; }
        public Guid SmallestUnitId { set; get; }
        public List<ProductImageCreateDto> Images { set; get; }
        #endregion
    }

    public class ProductImageCreateDto
    {
        public string ImagePath { get; set; }
        public string Caption { get; set; }
        public bool IsImageFeature { get; set; }
        public string? Description { get; set; }

        #region Relationship
        //public Guid ProductId { get; set; }
        #endregion
    }
}
