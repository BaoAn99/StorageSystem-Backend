using AutoMapper;
using StorageSystem.Order.Application.Models.ProductTypes;
using StorageSystem.Order.Domain.Entities.Products;

namespace StorageSystem.Order.Application.MappingProfiles.Products
{
    public class ProductTypeMapping : Profile
    {
        public ProductTypeMapping()
        {
            Init();
        }

        private void Init()
        {
            CreateMap<ProductTypeCreateDto, ProductType>();
            CreateMap<ProductTypeUpdateDto, ProductType>();
            CreateMap<ProductType, ProductTypeForView>();
        }
    }
}
