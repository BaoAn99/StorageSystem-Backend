using AutoMapper;
using StorageSystem.Application.ProductAppService.Dtos;
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
            CreateMap<CreateOrUpdateProductDto, Product>().ReverseMap();

        }
    }
}
