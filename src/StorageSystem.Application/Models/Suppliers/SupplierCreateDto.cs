namespace StorageSystem.Application.Models.Suppliers
{
    public class SupplierCreateDto
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string ContactName { get; set; }
        public string? Description { get; set; }
        public List<SupplierProductCreateDto> Products { get; set; }
    }

    public class SupplierProductCreateDto
    {
        public Guid ProductId { get; set; }
    }
}
