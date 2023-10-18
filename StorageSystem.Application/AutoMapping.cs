using AutoMapper;
using StorageSystem.Application.ProductAppService.Dtos;
using StorageSystem.Application.ProductImageAppService.Dtos;
using StorageSystem.Models.Catalog.ProductImages;
using StorageSystem.Models.Catalog.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StorageSystem.Application
{
    public class AutoMapping :Profile
    {
        public AutoMapping()
        {
            CreateMap<Product, ProductDto>().ReverseMap();
            CreateMap<CreateProductDto, Product>().ForMember(des => des.ProductImages, act => act.MapFrom(src => src.ProductImages));
            CreateMap<UpdateProductDto, Product>().ReverseMap();
            CreateMap<CreateProductImageDto, ProductImage>().ReverseMap();
            CreateMap<UpdateProductImageDto, ProductImage>().ReverseMap();

        }
    }
}
