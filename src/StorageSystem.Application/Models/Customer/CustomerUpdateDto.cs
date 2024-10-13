using StorageSystem.Domain.Enums;

namespace StorageSystem.Application.Models.Customer
{
    public class CustomerUpdateDto
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string Description { get; set; }
        public Gender? Gender { get; set; }
        public DateTimeOffset? Birthdate { get; set; }
    }
}
