using Microsoft.AspNetCore.Mvc;
using StorageSystem.Application.Contracts.Services;
using StorageSystem.Application.Models.Invoices;

namespace StorageSystem.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InvoiceController : ControllerBase
    {
        private readonly IInvoiceService _invoiceService;

        public InvoiceController(IInvoiceService invoiceService)
        {
            _invoiceService = invoiceService;
        }

        [HttpPost("Create")]
        public async Task<IActionResult> Create([FromBody] InvoiceCreateDto model)
        {
            var invoiceId = await _invoiceService.CreateInvoiceAsync(model);
            return Ok(invoiceId);
        }

        [HttpPost("Update")]
        public async Task<IActionResult> Update([FromBody] InvoiceUpdateDto model)
        {
            var invoiceId = await _invoiceService.UpdateInvoiceAsync(model);
            return Ok(invoiceId);
        }

        [HttpPost("Print/{id}")]
        public async Task<IActionResult> Print(Guid id)
        {
            var invoiceId = await _invoiceService.PrintInvoiceAsync(id);
            return Ok(invoiceId);
        }
    }
}
