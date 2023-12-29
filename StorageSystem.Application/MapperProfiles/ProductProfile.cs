using AutoMapper;
using StorageSystem.Application.Models.Product.Ins;
using StorageSystem.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StorageSystem.Application.MapperProfiles
{
    public class ProductProfile : Profile
    {
        public ProductProfile() 
        {
            CreateMap<CreateProductInsDto, Product>().ReverseMap();
        }
    }
}
