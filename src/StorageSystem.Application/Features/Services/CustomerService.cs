using StorageSystem.Application.Contracts.Repositories;
using StorageSystem.Application.Contracts.Services;
using StorageSystem.Application.Models.Customer;
using StorageSystem.Domain.Entities.Customers;

namespace StorageSystem.Application.Features.Services
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
