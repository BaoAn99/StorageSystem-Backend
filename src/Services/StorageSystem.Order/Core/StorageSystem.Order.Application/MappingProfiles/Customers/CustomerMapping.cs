using AutoMapper;
using StorageSystem.Order.Application.Models.Customer;
using StorageSystem.Order.Domain.Entities.Customers;

namespace StorageSystem.Order.Application.MappingProfiles.Customers
{
    public class CustomerMapping : Profile
    {
        public CustomerMapping()
        {
            Init();
        }

        private void Init()
        {
            CreateMap<CustomerCreateDto, Customer>();
            CreateMap<CustomerUpdateDto, Customer>();
            //CreateMap<Customer, CustomerForView>();

        }
    }
}
