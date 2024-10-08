namespace StorageSystem.Application.Models.Warehouses
{
    public class WarehouseCreateDto
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string ContactName { get; set; }
        public string? Description { get; set; }
    }
}
