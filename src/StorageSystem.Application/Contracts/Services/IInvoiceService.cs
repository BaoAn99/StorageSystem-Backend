using StorageSystem.Application.Models.Invoices;

namespace StorageSystem.Application.Contracts.Services
{
    public interface IInvoiceService
    {
        Task<Guid> CreateInvoiceAsync(InvoiceCreateDto model);
        Task<Guid> UpdateInvoiceAsync(InvoiceUpdateDto model);
        //bool DeleteInvoiceAsync(Guid id);
        Task<InvoiceForView> PrintInvoiceAsync(Guid id);
        //IEnumerable<InvoiceForView> GetAllInvoices();
    }
}
