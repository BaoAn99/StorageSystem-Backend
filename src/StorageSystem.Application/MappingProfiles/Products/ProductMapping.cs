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
            CreateMap<ProductUpdateDto, Product>().ForMember(x => x.Id, opt => opt.Ignore());
            CreateMap<Product, ProductForView>();
            //CreateMap<Product, ProductUpdateDto>();
            //CreateMap<ProductImage,ProductImageUpdateDto>();
            CreateMap<ProductImageCreateDto, ProductImage>();
            CreateMap<ProductImageUpdateDto, ProductImage>();
            CreateMap<ProductImage, ProductImageForView>();
        }
    }
}
