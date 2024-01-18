using AutoMapper;
using FluentValidation.Results;
using Microsoft.Extensions.Logging;
using OneOf;
using StorageSystem.Application.Contracts.DataAccess.Base;
using StorageSystem.Application.Contracts.Services;
using StorageSystem.Application.Models;
using StorageSystem.Application.Models.Bases;
using StorageSystem.Application.Models.Bill.Ins;
using StorageSystem.Application.Models.Order.Outs;
using StorageSystem.Application.Models.Order.Ins;
using StorageSystem.Application.Models.Product.Ins;
using StorageSystem.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StorageSystem.Domain.Enums;

namespace StorageSystem.Application.Features.Services
{
    public class OrderService : IOrderService
    {
        private readonly ILogger<OrderService> _logger;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public OrderService(ILogger<OrderService> logger, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<OneOf<bool, LocalizationErrorMessageOutDto, ValidationResult>> CancelledOrder(CancelledOrderInsDto orderDto)
        {
            List<Order> orders = (List<Order>)await _unitOfWork.OrderDataAccess.GetOrderUnpaidByCustomerId(orderDto.CustomerId);
            if (orders.Any())
            {
                try
                {
                    _logger.LogInformation($"Start cancel order");
                    orders.Select(order =>
                    {
                        order.StatusOrder = StatusOrder.CANCELLED;
                        return order;
                    }).ToList();
                    _unitOfWork.OrderDataAccess.UpdateOrderRange(orders);
                    List<UpdateQuantityProductDto> listItems = new List<UpdateQuantityProductDto>();
                    foreach (var obj in orders)
                    {
                        UpdateQuantityProductDto updateQuantityProductDto = new UpdateQuantityProductDto();
                        updateQuantityProductDto.ProductId = obj.ProductId;
                        updateQuantityProductDto.Quantity = obj.Quantity;
                        updateQuantityProductDto.UnitId = obj.UnitId;
                        listItems.Add(updateQuantityProductDto);
                    }
                    List<ProductUnit> list = (List<ProductUnit>)await _unitOfWork.ProductUnitDataAccess.GetProductsByProductIdsAndUnitIds(listItems);

                    if(list.Any() && list.Count() != listItems.Count())
                    {
                        return false;
                    }

                    foreach (var i in list)
                    {
                        foreach (var j in listItems)
                        {
                            if (j.ProductId == i.ProductId && j.UnitId == i.UnitId)
                            {
                                i.Quantity = i.Quantity + j.Quantity;
                            }
                        }
                    }
                    _unitOfWork.ProductUnitDataAccess.UpdateProductOfUnitRange(list);
                    await _unitOfWork.SaveChangesAsync();
                }
                catch (Exception ex)
                {
                    _logger.LogError($"Error when cancel order: {ex.Message}, {ex.InnerException}!");
                    return false;
                }
                return true;
            }
            return new ValidationResult(
                       new List<ValidationFailure>
                       {
                            new ValidationFailure ("Not exists order!", "400000")
                       }
                   );
        }

        public async Task<OneOf<bool, LocalizationErrorMessageOutDto, ValidationResult>> DeleteOrder(Guid id)
        {
            List<Order> orders = (List<Order>)await _unitOfWork.OrderDataAccess.FindAllOrdersById(id);
            if (orders.Any())
            {
                try
                {
                    _logger.LogInformation($"Start delete order");
                    _unitOfWork.OrderDataAccess.DeleteOrderRange(orders);
                    await _unitOfWork.SaveChangesAsync();
                }
                catch (Exception ex)
                {
                    _logger.LogError($"Error when delete order: {ex.Message}, {ex.InnerException}!");
                    return false;
                }
                return true;
            }
            return new ValidationResult(
                       new List<ValidationFailure>
                       {
                            new ValidationFailure ("Not exists order!", "400000")
                       }
                   );
        }

        public async Task<OneOf<List<GetOrderForView>, LocalizationErrorMessageOutDto, ValidationResult>> FindAllOrdersById(Guid id)
        {
            List<Order> orders = (List<Order>) await _unitOfWork.OrderDataAccess.FindAllOrdersById(id);
            if (orders.Any())
            {
                return _mapper.Map<List<GetOrderForView>>(orders);
            }
            return new ValidationResult(
                       new List<ValidationFailure>
                       {
                            new ValidationFailure ("Not exists order!", "400000")
                       }
                   );
        }

        public Task<OneOf<List<GetOrderForView>, LocalizationErrorMessageOutDto, ValidationResult>> GetAllOrderDetailsFromOrderId()
        {
            throw new NotImplementedException();
        }

        public Task<OneOf<List<GetOrderForView>, LocalizationErrorMessageOutDto, ValidationResult>> GetAllOrderDetailsFromPhone(string phone)
        {
            throw new NotImplementedException();
        }

        public Task<OneOf<List<GetOrderForView>, LocalizationErrorMessageOutDto, ValidationResult>> GetAllOrders(Paging filter)
        {
            throw new NotImplementedException();
        }

        public async Task<OneOf<bool, LocalizationErrorMessageOutDto, ValidationResult>> PrintInvoice(CreateOrderInsDto orderDto)
        {
            List<Order> orders = _mapper.Map<List<Order>>(orderDto.Orders);
            Guid customerId = Guid.NewGuid();
            if (orderDto.Phone != null)
            {
                _logger.LogInformation($"Check customer exists!");
                Customer customer = await _unitOfWork.CustomerDataAccess.FindCustomerByPhoneNumber(orderDto.Phone);
                Customer cus = new Customer();
                
                if (customer == null)
                {
                    cus.Address = orderDto.Address!;
                    cus.Phone = orderDto.Phone!;
                    cus.Name = orderDto.CustomerName!;

                    await _unitOfWork.CustomerDataAccess.CreateCustomerAsync(cus);
                    customerId = cus.Id;
                }
                else
                {
                    customerId = customer.Id;
                    //Get order unpaid of customerId
                    List<Order> orderList = (List<Order>)await _unitOfWork.OrderDataAccess.GetOrderUnpaidByCustomerId(customerId);
                    if (orderList.Any())
                    {
                        return new ValidationResult(
                           new List<ValidationFailure>
                           {
                                new ValidationFailure ("Customer Unpaid!", "400000")
                           }
                       );
                    }
                }
            }

            try
            {
                orders.Select(obj =>
                {
                    obj.CustomerId = customerId;
                    obj.StatusOrder = StatusOrder.UNPAID;
                    return obj;
                }).ToList();

                await _unitOfWork.OrderDataAccess.CreateOrderRangeAsync(orders);
                //update quantity product from ids
                List<UpdateQuantityProductDto> listItems = new List<UpdateQuantityProductDto>();
                foreach (var obj in orderDto.Orders)
                {
                    UpdateQuantityProductDto updateQuantityProductDto = new UpdateQuantityProductDto();
                    updateQuantityProductDto.ProductId = obj.ProductId;
                    updateQuantityProductDto.Quantity = obj.Quantity;
                    updateQuantityProductDto.UnitId = obj.UnitId;
                    listItems.Add(updateQuantityProductDto);
                }
                var res = await _unitOfWork.ProductUnitDataAccess.UpdateProductQuantity(listItems);
                if (res)
                {
                    await _unitOfWork.SaveChangesAsync();
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error when create order: {ex.Message}, {ex.InnerException}!");
                return false;
            }
        }

        public async Task<OneOf<bool, LocalizationErrorMessageOutDto, ValidationResult>> UpdateOrder(Guid customerId, UpdateOrderInsDto orderDto)
        {
            List<Order> orders = (List<Order>)await _unitOfWork.OrderDataAccess.GetOrderUnpaidByCustomerId(customerId);
            if (orders.Any())
            {
                try
                {
                    _logger.LogInformation($"Start update order");
                    _unitOfWork.OrderDataAccess.UpdateOrderRange(orders);

                    var dic1 = orderDto.Orders.ToDictionary(orders => (orders.ProductId, orders.UnitId));
                    var dic2 = orders.ToDictionary(orders => (orders.ProductId, orders.UnitId));
                    var a = dic1.Keys.ToList().Union(dic2.Keys.ToList()).ToList();
                    var b = dic1.Keys.ToList().Intersect(dic2.Keys.ToList());
                    var c = a.ToList().Except(b.ToList());
                    
                    List<UpdateQuantityProductDto> listItems = new List<UpdateQuantityProductDto>();
                    foreach (var obj in a)
                    {
                        UpdateQuantityProductDto updateQuantityProductDto = new UpdateQuantityProductDto();
                        updateQuantityProductDto.ProductId = obj.ProductId;
                        //updateQuantityProductDto.Quantity = obj;
                        updateQuantityProductDto.UnitId = obj.UnitId;
                        listItems.Add(updateQuantityProductDto);
                    }

                    //foreach (var obj in orderDto.Orders)
                    //{
                    //    UpdateQuantityProductDto updateQuantityProductDto = new UpdateQuantityProductDto();
                    //    updateQuantityProductDto.ProductId = obj.ProductId;
                    //    updateQuantityProductDto.Quantity = obj.Quantity;
                    //    updateQuantityProductDto.UnitId = obj.UnitId;
                    //    listItems.Add(updateQuantityProductDto);
                    //}
                    //listItems = listItems.Distinct().ToList();
                    List<ProductUnit> list = (List<ProductUnit>)await _unitOfWork.ProductUnitDataAccess.GetProductsByProductIdsAndUnitIds(listItems);

                    if (list.Any() && list.Count() != listItems.Count())
                    {
                        return false;
                    }

                    //product need update
                    foreach (var i in list)
                    {
                        //old list
                        foreach (var j in listItems)
                        {
                            //new list
                            foreach (var k in orderDto.Orders)
                            {
                                if (j.ProductId == k.ProductId && j.UnitId == k.UnitId)
                                {
                                    if(j.Quantity > k.Quantity)
                                    {
                                        i.Quantity = i.Quantity + (j.Quantity - k.Quantity);
                                    }else if (j.Quantity < k.Quantity)
                                    {
                                        i.Quantity = i.Quantity - (k.Quantity - j.Quantity);
                                    }
                                }else if((j.ProductId != k.ProductId && j.UnitId == k.UnitId) || (j.ProductId == k.ProductId && j.UnitId != k.UnitId) || (j.ProductId != k.ProductId && j.UnitId != k.UnitId))
                                {

                                }
                                //Nếu list mới không còn mua sp như list cũ ... hoặc list mới mua sp mới khác list cũ
                            }
                        }
                    }
                    _unitOfWork.ProductUnitDataAccess.UpdateProductOfUnitRange(list);
                    await _unitOfWork.SaveChangesAsync();
                }
                catch (Exception ex)
                {
                    _logger.LogError($"Error when update order: {ex.Message}, {ex.InnerException}!");
                    return false;
                }
                return true;
            }
            return new ValidationResult(
                       new List<ValidationFailure>
                       {
                            new ValidationFailure ("Not exists order!", "400000")
                       }
                   );
        }

        //public async Task<OneOf<bool, LocalizationErrorMessageOutDto, ValidationResult>> UpdateOrderStatus(Guid id, StatusOrder status)
        //{
        //    List<Order> orders = (List<Order>)await _unitOfWork.OrderDataAccess.FindAllOrdersById(id);
        //    if (orders.Any())
        //    {
        //        try
        //        {
        //            _logger.LogInformation($"Start update order status");
        //            orders.Select(o =>
        //            {
        //                o.StatusOrder = status;
        //                return o;
        //            });
        //            _unitOfWork.OrderDataAccess.UpdateOrderRange(orders);
        //            await _unitOfWork.SaveChangesAsync();
        //        }
        //        catch (Exception ex)
        //        {
        //            _logger.LogError($"Error when update order status: {ex.Message}, {ex.InnerException}!");
        //            return false;
        //        }
        //        return true;
        //    }
        //    return new ValidationResult(
        //               new List<ValidationFailure>
        //               {
        //                    new ValidationFailure ("Not exists order!", "400000")
        //               }
        //           );
        //}
    }
}
