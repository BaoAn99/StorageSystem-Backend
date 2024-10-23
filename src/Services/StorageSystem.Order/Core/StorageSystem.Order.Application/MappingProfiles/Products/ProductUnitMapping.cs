using AutoMapper;
using StorageSystem.Order.Application.Models.ProductUnits;
using StorageSystem.Order.Domain.Entities.Products;

namespace StorageSystem.Order.Application.MappingProfiles.Products
{
    public class ProductUnitMapping : Profile
    {
        public ProductUnitMapping()
        {
            Init();
        }

        private void Init()
        {
            CreateMap<ProductUnitCreateDto, ProductUnit>();
            CreateMap<ProductUnitUpdateDto, ProductUnit>();
            CreateMap<ProductUnit, ProductUnitForView>();
        }
    }
}
