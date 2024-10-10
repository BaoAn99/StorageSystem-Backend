    using AutoMapper;
using StorageSystem.Application.Models.Invoices;
using StorageSystem.Domain.Entities.Invoices;

namespace StorageSystem.Application.MappingProfiles.Invoices
{
    public class InvoiceMapping : Profile
    {
        public InvoiceMapping()
        {
            Init();
        }

        private void Init()
        {
            CreateMap<InvoiceCreateDto, Invoice>();
            CreateMap<InvoiceUpdateDto, Invoice>();
            CreateMap<Invoice, InvoiceForView>()
                .ForMember(
                    dest => dest.Items,
                    opt => opt.MapFrom(src => src.Lines));
            CreateMap<InvoiceLine, InvoiceLineForView>();
        }
    }
}
