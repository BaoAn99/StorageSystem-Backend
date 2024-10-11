using AutoMapper;
using StorageSystem.Application.Models.Products;
using StorageSystem.Domain.Entities.Products;

namespace StorageSystem.Application.MappingProfiles.Products
{
    public class ProductMapping : Profile
    {
        public ProductMapping()
        {
            Init();
        }

        private void Init()
        {
            CreateMap<ProductCreateDto, Product>();
            CreateMap<ProductUpdateDto, Product>();
            CreateMap<Product, ProductForView>();

            CreateMap<ProductImageCreateDto, ProductImage>();
            CreateMap<ProductImageUpdateDto, ProductImage>();
            CreateMap<ProductImage, ProductImageForView>();
        }
    }
}
