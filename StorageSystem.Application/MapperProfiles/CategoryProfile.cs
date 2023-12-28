using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StorageSystem.Application.MapperProfiles
{
    public class CategoryProfile : Profile
    {
        public CategoryProfile() 
        {
            Initialize();
        }

        private void Initialize()
        {
            CreateCategoryMapper();
        }

        private void CreateCategoryMapper()
        {
            
            //CreateMap<CreateProductDto, Product>().ForMember(des => des.ProductImages, act => act.MapFrom(src => src.ProductImages));
            //CreateMap<UpdateProductDto, Product>().ReverseMap();
            //CreateMap<CreateProductImageDto, ProductImage>().ReverseMap();
            //CreateMap<UpdateProductImageDto, ProductImage>().ReverseMap();
        }
    }
}
