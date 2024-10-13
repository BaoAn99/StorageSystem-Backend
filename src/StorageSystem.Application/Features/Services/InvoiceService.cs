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
        private readonly IInvoiceRepository<InvoiceLine, Guid> _invoiceLineRepository;

        public InvoiceService(
            IInvoiceRepository<Invoice, Guid> invoiceRepository, ICustomerRepository<Customer, Guid> customerRepository, IEntityManager<Invoice> invoiceManager, IEntityManager<InvoiceLine> invoiceLineManager, IEntityManager<Customer> customerManager, IUnitOfWork unitOfWork, IMapper mapper, IInvoiceRepository<InvoiceLine, Guid> invoiceLineRepository)
        {
            _invoiceRepository = invoiceRepository;
            _customerRepository = customerRepository;
            _invoiceManager = invoiceManager;
            _invoiceLineManager = invoiceLineManager;
            _customerManager = customerManager;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _invoiceLineRepository = invoiceLineRepository;
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
                invoice.Lines = new List<InvoiceLine>();
                invoice.CustomerId = customerId;
                _invoiceManager.SetCreating(invoice);

                foreach (var item in model.Items)
                {
                    if ((InvoiceLineStatus)model.Status != item.Status)
                        throw new ArgumentException("Trạng thái phiếu thu không hợp lệ");

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

        public async Task<Guid> UpdateInvoiceAsync(InvoiceUpdateDto model, Guid id)
        {
            try
            {
                if (!model.Items.Any())
                    throw new ArgumentException("Thông tin hóa đơn không hợp lệ!");

                var invoice = await _invoiceRepository.GetByIdAsync(id);

                if (invoice == null)
                    throw new ArgumentException("Không tìm thấy hóa đơn!");

                if (!(invoice.Status == InvoiceStatus.Unpaid ||
                     invoice.Status == InvoiceStatus.Debited))
                    throw new ArgumentException("Trạng thái phiếu thu không hợp lệ!");

                if (invoice.Status == InvoiceStatus.Unpaid && model.Status == InvoiceStatus.Refunded ||
                    invoice.Status == InvoiceStatus.Debited && (model.Status == InvoiceStatus.Unpaid || model.Status == InvoiceStatus.Refunded))
                    throw new ArgumentException("Trạng thái phiếu thu không hợp lệ!");

                var updateInvoice = _mapper.Map<Invoice>(model);
                double amount = 0;
                double netAmount = 0;

                foreach (var item in model.Items)
                {
                    if ((InvoiceLineStatus)model.Status != item.Status)
                        throw new ArgumentException("Trạng thái phiếu thu không hợp lệ");

                    var invoiceLine = invoice.Lines.FirstOrDefault(l => l.ProductId == item.ProductId);

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

                    if (invoiceLine != null)
                    {
                        if (item.ProductId == invoiceLine.ProductId)
                        {
                            if (invoiceLine.DiscountPercent != item.DiscountPercent && invoiceLine.DiscountAmount != item.DiscountAmount)
                                throw new ArgumentException("Không được cập nhật giảm tiền hoặc giảm % trong chi tiết hóa đơn");
                        }

                        invoiceLine.Price = price;
                        invoiceLine.NetPrice = netPrice;
                        invoiceLine.UnitPrice = item.UnitPrice;
                        invoiceLine.Quantity = item.Quantity;
                        invoiceLine.DiscountAmount = item.DiscountAmount;
                        invoice.DiscountPercent = item.DiscountPercent;
                        invoiceLine.Description = item.Description;
                        invoiceLine.ProductId = item.ProductId;
                        invoiceLine.Status = item.Status;
                        _invoiceLineManager.SetUpdating(invoiceLine);
                    }
                    else
                    {
                        var newInoiveLine = new InvoiceLine
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

                        invoice.Lines.Add(newInoiveLine);

                        _invoiceLineManager.SetCreating(newInoiveLine);
                    }

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
                invoice.Status = model.Status;

                _invoiceManager.SetUpdating(invoice);
                await _invoiceRepository.UpdateAsync(invoice);
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
                    var canceledInvoice = _mapper.Map<Invoice>(invoice);
                    canceledInvoice.Lines = new List<InvoiceLine>();
                    canceledInvoice.Status = InvoiceStatus.Cancelled;

                    foreach (var item in invoice.Lines)
                    {
                        var canceledInvoiceLine = new InvoiceLine
                        {
                            Price = item.Price,
                            NetPrice = item.NetPrice,
                            UnitPrice = item.UnitPrice,
                            Quantity = item.Quantity,
                            DiscountAmount = item.DiscountAmount,
                            DiscountPercent = item.DiscountPercent,
                            Description = item.Description,
                            ProductId = item.ProductId,
                            Status = InvoiceLineStatus.Cancelled,
                        };

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

        public async Task<List<Guid>> CanceledInvoiceLineAsync(Guid id, List<Guid> idLines)
        {
            try
            {
                var invoice = await _invoiceRepository.GetByIdAsync(id);

                if (invoice == null)
                    throw new ArgumentException("Không tìm thấy hóa đơn!");
                var invoiceLines = invoice.Lines.Where(il => idLines.Contains(il.Id)).ToList();

                if (!invoiceLines.Any())
                    throw new ArgumentException("Không tìm thấy thông tin chi tiết hóa đơn");

                if (!(invoice.Status == InvoiceStatus.Unpaid ||
                    invoice.Status == InvoiceStatus.Debited))
                    throw new ArgumentException("Không thể hủy hóa đơn");

                double amount = invoice.Amount;
                double netAmount = 0;
                double amountCanceled = 0;

                var checkExitsCanceledInvoice = _invoiceRepository.FindByCondition(i => i.OldInvoiceId == invoice.Id && i.Status == InvoiceStatus.Cancelled).FirstOrDefault();

                if (checkExitsCanceledInvoice != null)
                {
                    amountCanceled = checkExitsCanceledInvoice.Amount;
                }

                var canceledInvoice = checkExitsCanceledInvoice;

                if (canceledInvoice == null)
                {
                    canceledInvoice = new Invoice()
                    {
                        CustomerId = invoice.CustomerId,
                        CustomerName = invoice.CustomerName,
                        CustomerPhone = invoice.CustomerPhone,
                        CustomerAddress = invoice.CustomerAddress,
                        CashierId = invoice.CashierId,
                        Time = DateTime.Now,
                        Deposit = 0,
                        DiscountAmount = 0,
                        DiscountPercent = 0,
                        Status = InvoiceStatus.Cancelled,
                        Amount = 0,
                        NetAmount = 0,
                        IsPaid = invoice.IsPaid,
                        OldInvoiceId = invoice.Id,
                        Lines = new List<InvoiceLine>()
                    };

                    _invoiceManager.SetCreating(canceledInvoice);

                    await _invoiceRepository.CreateAsync(canceledInvoice);

                }

                foreach (var invoiceLine in invoiceLines)
                {
                    if (invoiceLine.Status == InvoiceLineStatus.Cancelled)
                        throw new ArgumentException("Thông tin chi tiết hóa đơn đã được hủy");

                    double netPrice = invoiceLine.NetPrice;

                    if (invoice.Status == InvoiceStatus.Unpaid)
                    {
                        invoice.Lines.Remove(invoiceLine);

                        if (!invoice.Lines.Any())
                        {
                            await _invoiceRepository.DeleteAsync(invoice);
                            await _unitOfWork.CommitAsync();

                            return idLines;
                        }

                        await _invoiceLineRepository.DeleteAsync(invoiceLine);
                    }

                    if (invoice.Status == InvoiceStatus.Debited)
                    {
                        invoiceLine.Status = InvoiceLineStatus.Cancelled;
                        _invoiceLineManager.SetUpdating(invoiceLine);

                        double currentInvoiceValue = invoice.Lines.Sum(il => il.NetPrice);

                        if (currentInvoiceValue < invoice.Deposit)
                        {
                            var refundAmount = invoice.Deposit - currentInvoiceValue;
                        }

                        amountCanceled += netPrice;

                        var canceledInvoiceLine = new InvoiceLine
                        {
                            Price = invoiceLine.Price,
                            NetPrice = invoiceLine.NetPrice,
                            UnitPrice = invoiceLine.UnitPrice,
                            Quantity = invoiceLine.Quantity,
                            DiscountAmount = invoiceLine.DiscountAmount,
                            DiscountPercent = invoiceLine.DiscountPercent,
                            Description = invoiceLine.Description,
                            ProductId = invoiceLine.ProductId,
                            Status = InvoiceLineStatus.Cancelled,
                        };

                        canceledInvoice.Amount = amountCanceled;
                        canceledInvoice.NetAmount = amountCanceled;

                        canceledInvoice.Lines.Add(canceledInvoiceLine);
                        _invoiceLineManager.SetCreating(canceledInvoiceLine);

                        if (checkExitsCanceledInvoice != null)
                        {
                            await _invoiceRepository.UpdateAsync(canceledInvoice);
                        }
                    }

                    amount -= netPrice;
                }

                netAmount = amount;

                if (invoice.DiscountPercent.HasValue)
                {
                    netAmount -= netAmount * (invoice.DiscountPercent.Value / 100);
                }

                if (invoice.DiscountAmount.HasValue)
                {
                    netAmount -= invoice.DiscountAmount.Value;
                }

                if (netAmount < 0)
                    netAmount = 0;

                invoice.Amount = amount;
                invoice.NetAmount = netAmount;

                _invoiceManager.SetUpdating(invoice);
                await _invoiceRepository.UpdateAsync(invoice);
                await _unitOfWork.CommitAsync();

                return idLines;
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
                refundInvoice.Lines = new List<InvoiceLine>();
                refundInvoice.Status = InvoiceStatus.Refunded;

                foreach (var item in invoice.Lines)
                {
                    var refundInvoiceLine = new InvoiceLine
                    {
                        Price = item.Price,
                        NetPrice = item.NetPrice,
                        UnitPrice = item.UnitPrice,
                        Quantity = item.Quantity,
                        DiscountAmount = item.DiscountAmount,
                        DiscountPercent = item.DiscountPercent,
                        Description = item.Description,
                        ProductId = item.ProductId,
                        Status = InvoiceLineStatus.Refunded,
                    };

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

        public async Task<List<Guid>> RefundInvoiceLineAsync(Guid id, List<Guid> idLines)
        {
            try
            {
                var invoice = await _invoiceRepository.GetByIdAsync(id);

                if (invoice == null)
                    throw new ArgumentException("Không tìm thấy hóa đơn!");

                if (invoice.Status != InvoiceStatus.Paid)
                    throw new ArgumentException("Không thể hoàn trả hóa đơn");

                var invoiceLines = invoice.Lines.Where(il => idLines.Contains(il.Id)).ToList();

                if (!invoiceLines.Any())
                    throw new ArgumentException("Không tìm thấy thông tin chi tiết hóa đơn");

                double amount = invoice.Amount;
                double netAmount = 0;
                double amountRefund = 0;

                var checkExitsRefundInvoice = _invoiceRepository.FindByCondition(i => i.OldInvoiceId == invoice.Id && i.Status == InvoiceStatus.Refunded).FirstOrDefault();

                if (checkExitsRefundInvoice != null)
                {
                    amountRefund = checkExitsRefundInvoice.Amount;
                }

                var refundInvoice = checkExitsRefundInvoice;

                if (refundInvoice == null)
                {
                    refundInvoice = new Invoice()
                    {
                        CustomerId = invoice.CustomerId,
                        CustomerName = invoice.CustomerName,
                        CustomerPhone = invoice.CustomerPhone,
                        CustomerAddress = invoice.CustomerAddress,
                        CashierId = invoice.CashierId,
                        Time = DateTime.Now,
                        Deposit = 0,
                        DiscountAmount = 0,
                        DiscountPercent = 0,
                        Status = InvoiceStatus.Refunded,
                        Amount = amountRefund,
                        NetAmount = amountRefund,
                        IsPaid = invoice.IsPaid,
                        OldInvoiceId = invoice.Id,
                        Lines = new List<InvoiceLine>()
                    };

                    _invoiceManager.SetCreating(refundInvoice);
                    await _invoiceRepository.CreateAsync(refundInvoice);
                }

                foreach (var invoiceLine in invoiceLines)
                {
                    if (invoiceLine.Status == InvoiceLineStatus.Refunded)
                        throw new ArgumentException("Thông tin chi tiết hóa đơn đã được hoàn trả");

                    double netPrice = invoiceLine.NetPrice;

                    if (invoice.Status == InvoiceStatus.Paid)
                    {
                        invoiceLine.Status = InvoiceLineStatus.Refunded;
                        _invoiceLineManager.SetUpdating(invoiceLine);

                        amountRefund += netPrice;

                        var refundInvoiceLine = new InvoiceLine
                        {
                            Price = invoiceLine.Price,
                            NetPrice = invoiceLine.NetPrice,
                            UnitPrice = invoiceLine.UnitPrice,
                            Quantity = invoiceLine.Quantity,
                            DiscountAmount = invoiceLine.DiscountAmount,
                            DiscountPercent = invoiceLine.DiscountPercent,
                            Description = invoiceLine.Description,
                            ProductId = invoiceLine.ProductId,
                            Status = InvoiceLineStatus.Refunded,
                        };

                        refundInvoice.Amount = amountRefund;
                        refundInvoice.NetAmount = amountRefund;

                        refundInvoice.Lines.Add(refundInvoiceLine);
                        _invoiceLineManager.SetCreating(refundInvoiceLine);

                        if (checkExitsRefundInvoice != null)
                        {
                            await _invoiceRepository.UpdateAsync(refundInvoice);
                        }
                    }

                    amount -= netPrice;
                }

                netAmount = amount;

                if (invoice.DiscountPercent.HasValue)
                {
                    netAmount -= netAmount * (invoice.DiscountPercent.Value / 100);
                }

                if (invoice.DiscountAmount.HasValue)
                {
                    netAmount -= invoice.DiscountAmount.Value;
                }

                if (netAmount < 0)
                    netAmount = 0;

                invoice.Amount = amount;
                invoice.NetAmount = netAmount;

                _invoiceManager.SetUpdating(invoice);
                await _invoiceRepository.UpdateAsync(invoice);
                await _unitOfWork.CommitAsync();

                return idLines;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }
    }
}