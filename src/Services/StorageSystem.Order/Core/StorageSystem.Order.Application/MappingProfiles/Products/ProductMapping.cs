using AutoMapper;
using StorageSystem.Order.Application.Models.Products;
using StorageSystem.Order.Domain.Entities.Products;

namespace StorageSystem.Order.Application.MappingProfiles.Products
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
