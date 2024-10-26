using AutoMapper;
using StorageSystem.Application.Models.Products;
using StorageSystem.Domain.Entities.PackageSpecs;

namespace StorageSystem.Application.MappingProfiles.ConversionSpecs
{
    public class ConversionSpecProductMapping : Profile
    {
        public ConversionSpecProductMapping()
        {
            Init();
        }

        private void Init()
        {
            CreateMap<ConversionSpecProduct, ConvertUnitProductForView>();
        }
    }
}
