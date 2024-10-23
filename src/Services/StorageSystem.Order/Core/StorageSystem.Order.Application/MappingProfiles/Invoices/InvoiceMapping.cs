using AutoMapper;
using StorageSystem.Order.Application.Models.Invoices;
using StorageSystem.Order.Domain.Entities.Invoices;

namespace StorageSystem.Order.Application.MappingProfiles.Invoices
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
            //CreateMap<Invoice, InvoiceForView>();

        }
    }
}
