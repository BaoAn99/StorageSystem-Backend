using AutoMapper;
using StorageSystem.Application.Models.ProductTypes;
using StorageSystem.Domain.Entities.Products;

namespace StorageSystem.Application.MappingProfiles.Products
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
