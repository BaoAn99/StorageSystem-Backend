using AutoMapper;
using StorageSystem.Application.Contracts.Repositories;
using StorageSystem.Application.Contracts.Repositories.Base;
using StorageSystem.Application.Contracts.Services;
using StorageSystem.Application.Models.Invoices;
using StorageSystem.Domain.Commons.Interfaces;
using StorageSystem.Domain.Entities.Customers;
using StorageSystem.Domain.Entities.Invoices;
using StorageSystem.Domain.Enums;

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
                if (model.Status != InvoiceStatus.Unpaid ||
                    model.Status != InvoiceStatus.Paid ||
                    model.Status != InvoiceStatus.Debited)
                    throw new ArgumentException("Trạng thái phiếu thu không hợp lệ!");

                if (model.Status == InvoiceStatus.Paid && model.Deposit != null && model.Deposit > 0 ||
                    model.Status == InvoiceStatus.Unpaid && model.Deposit != null && model.Deposit > 0)
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
                        var customer = _mapper.Map<Customer>(model);
                        _customerManager.SetCreating(customer);
                        await _customerRepository.CreateAsync(customer);

                        customerId = customer.Id;
                    }
                }

                double amount = 0;
                double netAmount = 0;

                var invoice = _mapper.Map<Invoice>(model);
                _invoiceManager.SetCreating(invoice);

                foreach (var item in model.Items)
                {
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

                    var invoiceLine = _mapper.Map<InvoiceLine>(model);
                    _invoiceLineManager.SetCreating(invoiceLine);
                    invoice.Lines.Add(invoiceLine);
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

        public async Task<bool> UpdateInvoiceAsync(InvoiceUpdateDto model, Guid id)
        {
            try
            {
                if (model == null || !model.Items.Any())
                    throw new ArgumentException("Thông tin hóa đơn không hợp lệ!");

                if (model.Status != InvoiceStatus.Unpaid ||
                    model.Status != InvoiceStatus.Paid ||
                    model.Status != InvoiceStatus.Debited)
                    throw new ArgumentException("Trạng thái phiếu thu không hợp lệ!");

                var invoice = _invoiceRepository.FindByCondition(i => i.Id.Equals(id)).FirstOrDefault();

                if (invoice == null)
                    throw new ArgumentException("Không tìm thấy hóa đơn!");

                if (invoice.Status == InvoiceStatus.Unpaid && model.Status == InvoiceStatus.Refunded ||
                    invoice.Status == InvoiceStatus.Paid && model.Status != InvoiceStatus.Refunded ||
                    invoice.Status == InvoiceStatus.Debited && (model.Status == InvoiceStatus.Unpaid || model.Status == InvoiceStatus.Refunded))
                    throw new ArgumentException("Trạng thái phiếu thu không hợp lệ!");

                var updateInvoice = _mapper.Map<Invoice>(model);

                foreach (var item in model.Items)
                {
                    var invoiceLine = invoice.Lines.FirstOrDefault(l => l.ProductId == item.ProductId);
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

                        invoice.Lines.Add(newInoiveLine);

                        _invoiceLineManager.SetCreating(newInoiveLine);
                    }
                }

                _invoiceManager.SetUpdating(invoice);
                await _unitOfWork.CommitAsync();

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return false;
        }
        public async Task<bool> CancelInvoiceAsync(Guid id)
        {
            try
            {
                var invoice = _invoiceRepository.FindByCondition(i => i.Id.Equals(id)).FirstOrDefault();

                if (invoice == null)
                    throw new ArgumentException("Không tìm thấy hóa đơn!");

                if (invoice.Status != InvoiceStatus.Unpaid ||
                    invoice.Status != InvoiceStatus.Debited)
                    throw new ArgumentException("Không thể hủy hóa đơn");

                if (invoice.Status == InvoiceStatus.Unpaid)
                {
                    invoice.Status = InvoiceStatus.Cancelled;

                    foreach (var item in invoice.Lines)
                    {
                        item.Status = InvoiceLineStatus.Cancelled;

                        _invoiceLineManager.SetUpdating(item);
                    }

                    _invoiceManager.SetUpdating(invoice);
                    await _invoiceRepository.UpdateAsync(invoice);
                }

                if (invoice.Status == InvoiceStatus.Debited)
                {
                    var cancelInvoice = _mapper.Map<Invoice>(invoice);
                    invoice.Status = InvoiceStatus.Cancelled;

                    foreach (var item in invoice.Lines)
                    {
                        var cancelInvoiceLine = _mapper.Map<InvoiceLine>(item);
                        cancelInvoiceLine.Status = InvoiceLineStatus.Cancelled;

                        _invoiceLineManager.SetCreating(cancelInvoiceLine);
                    }

                    _invoiceManager.SetCreating(cancelInvoice);
                    await _invoiceRepository.CreateAsync(cancelInvoice);
                }

                await _unitOfWork.CommitAsync();
                return true;

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return false;
        }

        public async Task<bool> CancelInvoiceLineAsync(Guid id, Guid idLine)
        {
            try
            {
                var invoice = _invoiceRepository.FindByCondition(i => i.Id.Equals(id), false, i => i.Lines).FirstOrDefault();

                if (invoice == null)
                    throw new ArgumentException("Không tìm thấy hóa đơn!");

                var invoiceLine = invoice.Lines.FirstOrDefault(il => il.Id == idLine);

                if (invoiceLine == null)
                    throw new ArgumentException("Không tìm thấy thông tin chi tiết hóa đơn");

                if (invoice.Status != InvoiceStatus.Unpaid ||
                    invoice.Status != InvoiceStatus.Debited)
                    throw new ArgumentException("Không thể hủy hóa đơn");


                if (invoice.Status == InvoiceStatus.Unpaid)
                {
                    invoice.Lines.Remove(invoiceLine);

                    if (!invoice.Lines.Any())
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
                    double currentInvoiceValue = invoice.Lines.Sum(il => il.NetPrice);

                    if (currentInvoiceValue < invoice.Deposit)
                    {
                        var refundAmount = invoice.Deposit - currentInvoiceValue;
                    }   

                    var cancelInvoice = _invoiceRepository.FindByCondition(i => i.Id == invoice.Id && i.Status == InvoiceStatus.Cancelled).FirstOrDefault();

                    if (cancelInvoice == null)
                    {
                        cancelInvoice = _mapper.Map<Invoice>(invoice);
                        cancelInvoice.Status = InvoiceStatus.Cancelled;

                        _invoiceManager.SetCreating(cancelInvoice);

                        var cancelInvoiceLine = _mapper.Map<InvoiceLine>(invoiceLine);
                        cancelInvoiceLine.Status = InvoiceLineStatus.Cancelled;

                        cancelInvoice.Lines.Add(cancelInvoiceLine);
                        _invoiceLineManager.SetCreating(cancelInvoiceLine);

                        await _invoiceRepository.CreateAsync(cancelInvoice);
                    }
                    else
                    {
                        var cancelInvoiceLine = _mapper.Map<InvoiceLine>(invoiceLine);
                        cancelInvoiceLine.Status = InvoiceLineStatus.Cancelled;

                        cancelInvoice.Lines.Add(cancelInvoiceLine);
                        _invoiceLineManager.SetCreating(cancelInvoiceLine);

                        await _invoiceRepository.UpdateAsync(cancelInvoice);
                    }
                }

                await _unitOfWork.CommitAsync();
                return true;

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return false;
        }

        public async Task<bool> RefundInvoiceAsync(Guid id)
        {
            try
            {
                var invoice = _invoiceRepository.FindByCondition(i => i.Id.Equals(id)).FirstOrDefault();

                if (invoice == null)
                    throw new ArgumentException("Không tìm thấy hóa đơn!");

                if (invoice.Status != InvoiceStatus.Paid)
                    throw new ArgumentException("Không thể hoàn trả hóa đơn");

                var refundInvoice = _mapper.Map<Invoice>(invoice);
                refundInvoice.Status = InvoiceStatus.Refunded;

                foreach (var item in invoice.Lines)
                {
                    var refundInvoiceLine = _mapper.Map<InvoiceLine>(item);
                    refundInvoiceLine.Status = InvoiceLineStatus.Refunded;

                    _invoiceLineManager.SetCreating(refundInvoiceLine);
                }

                _invoiceManager.SetCreating(refundInvoice);
                await _invoiceRepository.CreateAsync(refundInvoice);
                await _unitOfWork.CommitAsync();

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return false;
        }

        public async Task<bool> RefundInvoiceLineAsync(Guid id, Guid idLine)
        {
            try
            {
                var invoice = _invoiceRepository.FindByCondition(i => i.Id.Equals(id), false, i => i.Lines).FirstOrDefault();

                if (invoice == null)
                    throw new ArgumentException("Không tìm thấy hóa đơn!");

                if (invoice.Status != InvoiceStatus.Paid)
                    throw new ArgumentException("Không thể hoàn trả hóa đơn");

                var invoiceLine = invoice.Lines.FirstOrDefault(il => il.Id == idLine);
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
                    refundInvoice.Lines.Add(refundInvoiceLine);

                    await _invoiceRepository.CreateAsync(refundInvoice);
                }
                else
                {
                    var refundInvoiceLine = _mapper.Map<InvoiceLine>(invoiceLine);
                    refundInvoice.Lines.Add(refundInvoiceLine);

                    await _invoiceRepository.UpdateAsync(refundInvoice);
                }

                await _invoiceRepository.UpdateAsync(invoice);
                await _unitOfWork.CommitAsync();

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return false;
        }
    }
}
