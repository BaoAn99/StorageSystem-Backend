using AutoMapper;
using StorageSystem.Application.Contracts.Repositories;
using StorageSystem.Application.Contracts.Repositories.Base;
using StorageSystem.Application.Contracts.Services;
using StorageSystem.Application.Models.Invoices;
using StorageSystem.Domain.Commons.Interfaces;
using StorageSystem.Domain.Entities.Customers;
using StorageSystem.Domain.Entities.Invoices;
using StorageSystem.Domain.Enums;
using System.Text.Json.Serialization;

namespace StorageSystem.Application.Features.Services
{
    public class InvoiceService : IInvoiceService
    {
        private readonly IInvoiceRepository<Invoice, Guid> _invoiceRepository;
        private readonly ICustomerRepository<Customer, Guid> _customerRepository;
        private readonly IEntityManager<Invoice> _invoiceManager;
        private readonly IEntityManager<InvoiceLine> _invoiceLineManager;
        private readonly IEntityManager<Customer> _customerManager;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public InvoiceService(
            IInvoiceRepository<Invoice, Guid> invoiceRepository, ICustomerRepository<Customer, Guid> customerRepository, IEntityManager<Invoice> invoiceManager, IEntityManager<InvoiceLine> invoiceLineManager, IEntityManager<Customer> customerManager, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _invoiceRepository = invoiceRepository;
            _customerRepository = customerRepository;
            _invoiceManager = invoiceManager;
            _invoiceLineManager = invoiceLineManager;
            _customerManager = customerManager;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<Guid> CreateInvoiceAsync(InvoiceCreateDto model)
        {
            try
            {
                if (!(model.Status == InvoiceStatus.Unpaid ||
                      model.Status == InvoiceStatus.Paid ||
                      model.Status == InvoiceStatus.Debited))
                        throw new ArgumentException("Trạng thái phiếu thu không hợp lệ!");

                if (model.Status == InvoiceStatus.Paid && model.Deposit != null && model.Deposit > 0 ||
                    model.Status == InvoiceStatus.Unpaid && model.Deposit != null && model.Deposit > 0 ||
                    model.Status == InvoiceStatus.Debited && (model.Deposit == null || (model.Deposit != null && model.Deposit == 0)))
                        throw new ArgumentException("Trạng thái phiếu thu không hợp lệ!");

                if (model == null || !model.Items.Any())
                    throw new ArgumentException("Thông tin hóa đơn không hợp lệ!");

                var customerId = model.CustomerId;

                if (model.CustomerId.HasValue)
                {
                    var customer = _customerRepository.GetByIdAsync(model.CustomerId.Value);
                    if (customer == null)
                    {
                        throw new ArgumentException("Thông tin khách hàng không hợp lệ");
                    }
                }
                else
                {
                    var existingCustomer = _customerRepository.FindByCondition(c => c.Name == model.CustomerName && c.Phone == model.CustomerPhone).FirstOrDefault();
                    if (existingCustomer != null)
                    {
                        customerId = existingCustomer.Id;
                    }
                    else
                    {
                        var customer = new Customer
                        {
                            Name = model.CustomerName,
                            Phone = model.CustomerPhone,
                            Address = model.CustomerAddress
                        };
                        _customerManager.SetCreating(customer);
                        await _customerRepository.CreateAsync(customer);

                        customerId = customer.Id;
                    }
                }

                double amount = 0;
                double netAmount = 0;

                var invoice = _mapper.Map<Invoice>(model);
                invoice.InvoiceLines = new List<InvoiceLine>();
                invoice.CustomerId = customerId;
                _invoiceManager.SetCreating(invoice);

                foreach (var item in model.Items)
                {
                    //if (invoice.Status == InvoiceStatus.)

                    double price = item.UnitPrice * item.Quantity;
                    double netPrice = price;

                    if (item.DiscountPercent.HasValue)
                    {
                        netPrice -= netPrice * (item.DiscountPercent.Value / 100);
                    }

                    if (item.DiscountAmount.HasValue)
                    {
                        netPrice -= item.DiscountAmount.Value;
                    }

                    var invoiceLine = new InvoiceLine
                    {
                        Price = price,
                        NetPrice = netPrice,
                        UnitPrice = item.UnitPrice,
                        Quantity = item.Quantity,
                        DiscountAmount = item.DiscountAmount,
                        DiscountPercent = item.DiscountPercent,
                        Description = item.Description,
                        ProductId = item.ProductId,
                        Status = item.Status,
                    };
                    _invoiceLineManager.SetCreating(invoiceLine);
                    invoice.InvoiceLines.Add(invoiceLine);
                    amount += netPrice;
                }

                if (amount != model.Amount)
                    throw new ArgumentException("Tổng tiền không đúng");

                netAmount = amount;

                if (model.DiscountPercent.HasValue)
                {
                    netAmount -= netAmount * (model.DiscountPercent.Value / 100);
                }

                if (model.DiscountAmount.HasValue)
                {
                    netAmount -= model.DiscountAmount.Value;
                }

                if (netAmount != model.NetAmount)
                    throw new ArgumentException("Tổng tiền không đúng");

                invoice.Amount = amount;
                invoice.NetAmount = netAmount;


                await _invoiceRepository.CreateAsync(invoice);

                await _unitOfWork.CommitAsync();

                return invoice.Id;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }

        public async Task<Guid> UpdateInvoiceAsync(InvoiceUpdateDto model, Guid id)
        {
            try
            {
                if (!model.Items.Any())
                    throw new ArgumentException("Thông tin hóa đơn không hợp lệ!");

                if (!(model.Status == InvoiceStatus.Unpaid ||
                    model.Status == InvoiceStatus.Debited))
                    throw new ArgumentException("Trạng thái phiếu thu không hợp lệ!");

                var invoice = await _invoiceRepository.GetByIdAsync(id);

                if (invoice == null)
                    throw new ArgumentException("Không tìm thấy hóa đơn!");

                if (invoice.Status == InvoiceStatus.Unpaid && model.Status == InvoiceStatus.Refunded ||
                    invoice.Status == InvoiceStatus.Paid && model.Status != InvoiceStatus.Refunded ||
                    invoice.Status == InvoiceStatus.Debited && (model.Status == InvoiceStatus.Unpaid || model.Status == InvoiceStatus.Refunded))
                    throw new ArgumentException("Trạng thái phiếu thu không hợp lệ!");

                var updateInvoice = _mapper.Map<Invoice>(model);

                foreach (var item in model.Items)
                {
                    var invoiceLine = invoice.InvoiceLines.FirstOrDefault(l => l.ProductId == item.ProductId);
                    if (invoiceLine != null)
                    {
                        if(invoiceLine.DiscountPercent != item.DiscountPercent && invoiceLine.DiscountAmount != item.DiscountAmount)
                            throw new ArgumentException("Không được cập nhật giảm tiền hoặc giảm % trong chi tiết hóa đơn");

                        var updateInvoiceLine = _mapper.Map<InvoiceLine>(invoiceLine);

                        _invoiceLineManager.SetUpdating(updateInvoiceLine);
                    }
                    else
                    {
                        var newInoiveLine = _mapper.Map<InvoiceLine>(item);

                        invoice.InvoiceLines.Add(newInoiveLine);

                        _invoiceLineManager.SetCreating(newInoiveLine);
                    }
                }

                _invoiceManager.SetUpdating(invoice);
                await _unitOfWork.CommitAsync();

                return id;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }
        public async Task<Guid> CanceledInvoiceAsync(Guid id)
        {
            try
            {
                var invoice = await _invoiceRepository.GetByIdAsync(id);

                if (invoice == null)
                    throw new ArgumentException("Không tìm thấy hóa đơn!");

                if (!(invoice.Status == InvoiceStatus.Unpaid ||
                    invoice.Status == InvoiceStatus.Debited))
                    throw new ArgumentException("Không thể hủy hóa đơn");

                if (invoice.Status == InvoiceStatus.Unpaid)
                {
                    invoice.Status = InvoiceStatus.Cancelled;

                    foreach (var item in invoice.InvoiceLines)
                    {
                        item.Status = InvoiceLineStatus.Cancelled;

                        _invoiceLineManager.SetUpdating(item);
                    }

                    _invoiceManager.SetUpdating(invoice);
                    await _invoiceRepository.UpdateAsync(invoice);
                }

                if (invoice.Status == InvoiceStatus.Debited)
                {
                    var canceledInvoice = _mapper.Map<Invoice>(invoice);
                    canceledInvoice.Status = InvoiceStatus.Cancelled;

                    foreach (var item in invoice.InvoiceLines)
                    {
                        var canceledInvoiceLine = _mapper.Map<InvoiceLine>(item);
                        canceledInvoiceLine.Status = InvoiceLineStatus.Cancelled;

                        _invoiceLineManager.SetCreating(canceledInvoiceLine);
                    }

                    _invoiceManager.SetCreating(canceledInvoice);
                    await _invoiceRepository.CreateAsync(canceledInvoice);
                }

                await _unitOfWork.CommitAsync();
                return id;

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }

        }

        public async Task<Guid> CanceledInvoiceLineAsync(Guid id, Guid idLine)
        {
            try
            {
                var invoice = await _invoiceRepository.GetByIdAsync(id);

                if (invoice == null)
                    throw new ArgumentException("Không tìm thấy hóa đơn!");

                var invoiceLine = invoice.InvoiceLines.FirstOrDefault(il => il.Id == idLine);

                if (invoiceLine == null)
                    throw new ArgumentException("Không tìm thấy thông tin chi tiết hóa đơn");

                if (!(invoice.Status == InvoiceStatus.Unpaid ||
                    invoice.Status == InvoiceStatus.Debited))
                    throw new ArgumentException("Không thể hủy hóa đơn");

                if (!(invoiceLine.Status == InvoiceLineStatus.Unpaid ||
                    invoiceLine.Status == InvoiceLineStatus.Debited))
                    throw new ArgumentException("Không thể hủy hóa đơn");


                if (invoice.Status == InvoiceStatus.Unpaid)
                {
                    invoice.InvoiceLines.Remove(invoiceLine);

                    if (!invoice.InvoiceLines.Any())
                    {
                        await _invoiceRepository.DeleteAsync(invoice);
                    }
                    else
                    {
                        await _invoiceRepository.UpdateAsync(invoice);
                    }
                }

                if (invoice.Status == InvoiceStatus.Debited)
                {
                    invoiceLine.Status = InvoiceLineStatus.Cancelled;

                    double currentInvoiceValue = invoice.InvoiceLines.Sum(il => il.NetPrice);

                    if (currentInvoiceValue < invoice.Deposit)
                    {
                        var refundAmount = invoice.Deposit - currentInvoiceValue;
                    }   

                    var canceledInvoice = _invoiceRepository.FindByCondition(i => i.Id == invoice.Id && i.Status == InvoiceStatus.Cancelled).FirstOrDefault();

                    if (canceledInvoice == null)
                    {
                        canceledInvoice = _mapper.Map<Invoice>(invoice);
                        canceledInvoice.Status = InvoiceStatus.Cancelled;

                        _invoiceManager.SetCreating(canceledInvoice);

                        var CanceledInvoiceLine = _mapper.Map<InvoiceLine>(invoiceLine);
                        CanceledInvoiceLine.Status = InvoiceLineStatus.Cancelled;

                        canceledInvoice.InvoiceLines.Add(CanceledInvoiceLine);
                        _invoiceLineManager.SetCreating(CanceledInvoiceLine);

                        await _invoiceRepository.CreateAsync(canceledInvoice);
                    }
                    else
                    {
                        var canceledInvoiceLine = _mapper.Map<InvoiceLine>(invoiceLine);
                        canceledInvoiceLine.Status = InvoiceLineStatus.Cancelled;

                        canceledInvoice.InvoiceLines.Add(canceledInvoiceLine);
                        _invoiceLineManager.SetCreating(canceledInvoiceLine);

                        await _invoiceRepository.UpdateAsync(canceledInvoice);
                    }
                }

                await _unitOfWork.CommitAsync();
                return idLine;

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }

        }

        public async Task<Guid> RefundInvoiceAsync(Guid id)
        {
            try
            {
                var invoice = await _invoiceRepository.GetByIdAsync(id);

                if (invoice == null)
                    throw new ArgumentException("Không tìm thấy hóa đơn!");

                if (invoice.Status != InvoiceStatus.Paid)
                    throw new ArgumentException("Không thể hoàn trả hóa đơn");

                var refundInvoice = _mapper.Map<Invoice>(invoice);
                refundInvoice.Status = InvoiceStatus.Refunded;

                foreach (var item in invoice.InvoiceLines)
                {
                    if(item.Status == InvoiceLineStatus.Paid)
                        throw new ArgumentException("Không thể hoàn trả hóa đơn");

                    var refundInvoiceLine = _mapper.Map<InvoiceLine>(item);
                    refundInvoiceLine.Status = InvoiceLineStatus.Refunded;

                    _invoiceLineManager.SetCreating(refundInvoiceLine);
                }

                _invoiceManager.SetCreating(refundInvoice);
                await _invoiceRepository.CreateAsync(refundInvoice);
                await _unitOfWork.CommitAsync();

                return id;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }

        public async Task<Guid> RefundInvoiceLineAsync(Guid id, Guid idLine)
        {
            try
            {
                var invoice = await _invoiceRepository.GetByIdAsync(id);

                if (invoice == null)
                    throw new ArgumentException("Không tìm thấy hóa đơn!");

                if (invoice.Status != InvoiceStatus.Paid)
                    throw new ArgumentException("Không thể hoàn trả hóa đơn");

                var invoiceLine = invoice.InvoiceLines.FirstOrDefault(il => il.Id == idLine);
                if (invoiceLine == null)
                    throw new ArgumentException("Không tìm thấy thông tin chi tiết hóa đơn");

                if (invoiceLine.Status == InvoiceLineStatus.Refunded)
                    throw new InvalidOperationException("Thông tin chi tiết hóa đơn này đã được hoàn trả trước đó");

                invoiceLine.Status = InvoiceLineStatus.Refunded;
                _invoiceLineManager.SetUpdating(invoiceLine);

                var refundInvoice = _invoiceRepository.FindByCondition(i => i.Id == invoice.Id && i.Status == InvoiceStatus.Refunded).FirstOrDefault();

                if (refundInvoice == null)
                {
                    refundInvoice = _mapper.Map<Invoice>(invoice);
                    refundInvoice.Status = InvoiceStatus.Refunded;

                    _invoiceManager.SetCreating(refundInvoice);

                    var refundInvoiceLine = _mapper.Map<InvoiceLine>(invoiceLine);
                    refundInvoice.InvoiceLines.Add(refundInvoiceLine);

                    await _invoiceRepository.CreateAsync(refundInvoice);
                }
                else
                {
                    var RefundInvoiceLine = _mapper.Map<InvoiceLine>(invoiceLine);
                    refundInvoice.InvoiceLines.Add(RefundInvoiceLine);

                    await _invoiceRepository.UpdateAsync(refundInvoice);
                }

                await _invoiceRepository.UpdateAsync(invoice);
                await _unitOfWork.CommitAsync();

                return idLine;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }
    }
}
