using StorageSystem.Application.Models.Invoices;

namespace StorageSystem.Application.Contracts.Services
{
    public interface IInvoiceService
    {
        Task<Guid> CreateInvoiceAsync(InvoiceCreateDto model);
        Task<bool> UpdateInvoiceAsync(InvoiceUpdateDto model, Guid id);
        Task<bool> CancelInvoiceAsync(Guid id);
        Task<bool> CancelInvoiceLineAsync(Guid id, Guid idLine);
        Task<bool> RefundInvoiceAsync(Guid id);
        Task<bool> RefundInvoiceLineAsync(Guid id, Guid idLine);
        Task<InvoiceForView> PrintInvoiceAsync(Guid id);

        //bool DeleteInvoiceAsync(Guid id);
        //Task<InvoiceForView> GetInvoiceLineByInvoiceIdAsync(Guid id);
        //IEnumerable<InvoiceForView> GetAllInvoices();
    }
}
