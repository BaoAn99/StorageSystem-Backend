using StorageSystem.Application.Models.Invoices;

namespace StorageSystem.Application.Contracts.Services
{
    public interface IInvoiceService
    {
        Task<Guid> CreateInvoiceAsync(InvoiceCreateDto model);
        Task<Guid> UpdateInvoiceAsync(InvoiceUpdateDto model, Guid id);
        Task<Guid> CanceledInvoiceAsync(Guid id);
        Task<Guid> CanceledInvoiceLineAsync(Guid id, Guid idLine);
        Task<Guid> RefundInvoiceAsync(Guid id);
        Task<Guid> RefundInvoiceLineAsync(Guid id, Guid idLine);

        //bool DeleteInvoiceAsync(Guid id);
        //Task<InvoiceForView> GetInvoiceLineByInvoiceIdAsync(Guid id);
        //IEnumerable<InvoiceForView> GetAllInvoices();
    }
}
