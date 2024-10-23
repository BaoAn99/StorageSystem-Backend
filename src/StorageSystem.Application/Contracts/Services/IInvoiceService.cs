using StorageSystem.Application.Models.Invoices;

namespace StorageSystem.Application.Contracts.Services
{
    public interface IInvoiceService
    {
        Task<Guid> CreateInvoiceAsync(InvoiceCreateDto model);
        Task<Guid> UpdateInvoiceAsync(InvoiceUpdateDto model, Guid id);
        Task<Guid> CanceledInvoiceAsync(Guid id);
        Task<Guid> CanceledInvoiceLineAsync(Guid id, List<Guid> idLines);
        Task<Guid> RefundInvoiceAsync(Guid id);
        Task<Guid> RefundInvoiceLineAsync(Guid id, List<Guid> idLines);

        //bool DeleteInvoiceAsync(Guid id);
        //Task<InvoiceForView> GetInvoiceLineByInvoiceIdAsync(Guid id);
        //IEnumerable<InvoiceForView> GetAllInvoices();
    }
}
