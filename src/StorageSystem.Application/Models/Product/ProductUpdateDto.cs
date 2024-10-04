namespace StorageSystem.Application.Models.Product
{
    public class ProductUpdateDto
    {
        public string Name { set; get; }
        public double Price { set; get; }
        public string Description { set; get; }
        public string ThumbnailImage { get; set; }

        #region Relationship
        public Guid TypeId { set; get; }
        public Guid SmallestUnitId { set; get; }
        public List<ProductImageUpdateDto> Images { set; get; }
        #endregion
    }

    public class ProductImageUpdateDto
    {
        public string ImagePath { get; set; }
        public string Caption { get; set; }
        public bool IsImageFeature { get; set; }
        public string Description { get; set; }
    }
}
