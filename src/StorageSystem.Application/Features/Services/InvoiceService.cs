using AutoMapper;
using StorageSystem.Application.Contracts.Repositories;
using StorageSystem.Application.Contracts.Services;
using StorageSystem.Application.Models.Invoices;
using StorageSystem.Domain.Entities.Invoices;
using StorageSystem.Domain.Enums;

namespace StorageSystem.Application.Features.Services
{
    public class InvoiceService : IInvoiceService
    {
        private readonly IInvoiceRepository<Invoice,Guid> _invoiceRepository;
        private readonly IMapper _mapper;
        public InvoiceService(IInvoiceRepository<Invoice, Guid> invoiceRepository, IMapper mapper)
        {
            _invoiceRepository = invoiceRepository;
            _mapper = mapper;
        }
        public Task<Guid> CreateInvoiceAsync(InvoiceCreateDto model)
        {
            throw new NotImplementedException();
        }
        public Task<InvoiceForView> PrintInvoiceAsync(Guid id)
        {
            var invoice = _invoiceRepository.FindByCondition(i => i.Id.Equals(id),false,i => i.Lines).FirstOrDefault();
            if (invoice == null) 
            {
                throw new NotImplementedException("Invoice id not found");
            }
            var invoiceForView = _mapper.Map<InvoiceForView>(invoice);
            invoiceForView.StatusName = Enum.GetName(typeof(InvoiceStatus), invoiceForView.Status)!;
            foreach (var line in invoiceForView.Items)
            {
                line.StatusName = Enum.GetName(typeof(InvoiceLineStatus), line.Status)!;
            }
            return Task.FromResult(invoiceForView);
        }
        public Task<Guid> UpdateInvoiceAsync(InvoiceUpdateDto model)
        {
            throw new NotImplementedException();
        }
    }
}
