﻿using StorageSystem.Application.Models.Invoice;

namespace StorageSystem.Application.Contracts.Services
{
    public interface IInvoiceService
    {
        Task<Guid> CreateInvoiceAsync(InvoiceCreateDto model);
        Task<Guid> UpdateInvoiceAsync(InvoiceUpdateDto model);
        Task PrintInvoiceAsync();
        //bool DeleteInvoiceAsync(Guid id);
        //Task<InvoiceForView> GetInvoiceLineByInvoiceIdAsync(Guid id);
        //IEnumerable<InvoiceForView> GetAllInvoices();
    }
}
