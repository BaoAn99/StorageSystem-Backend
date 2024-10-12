﻿using Microsoft.AspNetCore.Mvc;
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

        [HttpPut("Update")]
        public async Task<IActionResult> Update([FromBody] InvoiceUpdateDto model, Guid id)
        {
            var invoiceId = await _invoiceService.UpdateInvoiceAsync(model, id);
            return Ok(invoiceId);
        }

        [HttpPost("CanceledInvoice")]
        public async Task<IActionResult> CanceledInvoice(Guid id)
        {
            var invoiceId = await _invoiceService.CanceledInvoiceAsync(id);
            return Ok(invoiceId);
        }

        [HttpPost("CanceledInvoiceLine")]
        public async Task<IActionResult> CanceledInvoiceLine(Guid id, Guid idLine)
        {
            var invoiceId = await _invoiceService.CanceledInvoiceLineAsync(id, idLine);
            return Ok(invoiceId);
        }

        [HttpPost("RefundInvoice")]
        public async Task<IActionResult> RefundInvoice(Guid id)
        {
            var invoiceId = await _invoiceService.RefundInvoiceAsync(id);
            return Ok(invoiceId);
        }

        [HttpPost("RefundInvoiceLine")]
        public async Task<IActionResult> RefundInvoiceLine(Guid id, Guid idLine)
        {
            var invoiceId = await _invoiceService.RefundInvoiceLineAsync(id, idLine);
            return Ok(invoiceId);
        }

        //[HttpPost("Print")]
        //public async Task<IActionResult> Print()
        //{
        //    //var invoiceId = await _invoiceService.CreateInvoiceAsync(model);
        //    return Ok("");
        //}
    }
}
