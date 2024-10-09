using AutoMapper;
using StorageSystem.Application.Models.Customer;
using StorageSystem.Domain.Entities.Customers;

namespace StorageSystem.Application.MappingProfiles.Customers
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
