using StorageSystem.Order.Application.Contracts.Repositories;
using StorageSystem.Order.Application.Contracts.Services;
using StorageSystem.Order.Application.Models.Customer;
using StorageSystem.Order.Domain.Entities.Customers;

namespace StorageSystem.Order.Application.Features.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository<Customer, Guid> _customerRepository;
        public Task<Guid> CreateCustomerAsync(CustomerCreateDto model)
        {
            throw new NotImplementedException();
        }

        public Task<Guid> UpdateCustomerAsync(CustomerUpdateDto model)
        {
            throw new NotImplementedException();
        }
    }
}
