using StorageSystem.Order.Application.Models.Customer;

namespace StorageSystem.Order.Application.Contracts.Services
{
    public interface ICustomerService
    {
        Task<Guid> CreateCustomerAsync(CustomerCreateDto model);
        Task<Guid> UpdateCustomerAsync(CustomerUpdateDto model);
        //bool DeleteCustomerAsync(Guid id);
        //Task<CustomerForView> GetCustomerLineByCustomerIdAsync(Guid id);
        //IEnumerable<CustomerForView> GetAllCustomers();
    }
}
