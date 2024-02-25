using AutoMapper;
using StorageSystem.Application.Models.Supplier.Ins;
using StorageSystem.Application.Models.Supplier.Outs;
using StorageSystem.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static NPOI.HSSF.Util.HSSFColor;

namespace StorageSystem.Application.MapperProfiles
{
    public class SupplierProfile : Profile
    {
        public SupplierProfile()
        {
            Initialize();
        }

        private void Initialize()
        {
            CreateSupplierMapper();
        }

        private void CreateSupplierMapper()
        {
            CreateMap<CreateSupplierInsDto, Supplier>()
                .ForMember(d => d.Name, opt => opt.MapFrom(s => s.Name))
                .ForMember(d => d.Address, opt => opt.MapFrom(s => s.Address))
                .ForMember(d => d.Phone, opt => opt.MapFrom(s => s.Phone))
                .ForMember(d => d.Email, opt => opt.MapFrom(s => s.Email));
            CreateMap<ProductListItemInsDto, Supplier>()
                .ForMember(d => d.ProductId, opt => opt.MapFrom(s => s.ProductId))
                .ForMember(d => d.Price, opt => opt.MapFrom(s => s.Price));
            CreateMap<CreateSupplierInsDto, IEnumerable<Supplier>>()
                .ConvertUsing<SupplierDtoToSupplierListConverter>();

            CreateMap<UpdateSupplierInsDto, Supplier>().ReverseMap();
            CreateMap<Supplier, GetSupplierForView>().ReverseMap();
            CreateMap<Supplier, SupplierList>().ReverseMap();
        }
    }
}
