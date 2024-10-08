using AutoMapper;
using StorageSystem.Application.Models.ProductUnits;
using StorageSystem.Domain.Entities.Products;

namespace StorageSystem.Application.MappingProfiles.Products
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
