namespace StorageSystem.Application.Models.ConversionSpec
{
    public class ConversionSpecProductUpdateDto
    {
        public int Quantity { get; set; }
        public Guid ProductId { get; set; }
        public Guid UnitId { get; set; }
        public string UnitName { get; set; }
        public Guid ConvertUnitId { get; set; }
        public string ConvertUnitName { get; set; }
    }
}
