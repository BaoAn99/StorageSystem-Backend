using StorageSystem.Application.Contracts.Services;
using StorageSystem.Application.Models.Invoice;

namespace StorageSystem.Application.Features.Services
{
    public class InvoiceService : IInvoiceService
    {
        public Task<Guid> CreateInvoiceAsync(InvoiceCreateDto model)
        {
            throw new NotImplementedException();
        }

        public Task<Guid> UpdateInvoiceAsync(InvoiceUpdateDto model)
        {
            throw new NotImplementedException();
        }
    }
}
