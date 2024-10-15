using AutoMapper;
using StorageSystem.Application.Contracts.Repositories;
using StorageSystem.Application.Contracts.Repositories.Base;
using StorageSystem.Application.Contracts.Services;
using StorageSystem.Application.Models.WarehouseInbounds;
using StorageSystem.Domain.Commons.Interfaces;
using StorageSystem.Domain.Entities.Storekeepers;
using StorageSystem.Domain.Entities.PackageSpecs;
using StorageSystem.Domain.Entities.Products;
using StorageSystem.Domain.Entities.Warehouses;
using StorageSystem.Domain.Entities.Suppliers;

namespace StorageSystem.Application.Features.Services
{
    public class WarehouseInboundService : IWarehouseInboundService
    {
        private readonly IWarehouseInboundRepository<WarehouseInbound, Guid> _warehouseInboundRepository;
        private readonly IProductRepository<Product, Guid> _productRepository;
        private readonly IStorekeeperRepository<Storekeeper, Guid> _storekeeperRepository;
        private readonly ISupplierRepository<Supplier, Guid> _supplierRepository;
        private readonly IWarehouseRepository<Warehouse, Guid> _warehouseRepository;
        private readonly IEntityManager<WarehouseInbound> _warehouseInboundManager;
        private readonly IEntityManager<WarehouseInboundLine> _warehouseInboundLineManager;
        private readonly IRepositoryBaseAsync<ProductSupplier, Guid> _productSupplierRepository;
        private readonly IRepositoryBaseAsync<ConversionSpecProduct, Guid> _conversionSpecProductRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public WarehouseInboundService(
            IWarehouseInboundRepository<WarehouseInbound, Guid> warehouseInboundRepository, 
            IStorekeeperRepository<Storekeeper ,Guid> storekeeperRepository, 
            ISupplierRepository<Supplier ,Guid> supplierRepository,
            IRepositoryBaseAsync<ProductSupplier, Guid> productSupplierRepository,
            IWarehouseRepository<Warehouse, Guid> warehouseRepository,
            IEntityManager<WarehouseInbound> warehouseInboundManager,
            IEntityManager<WarehouseInboundLine> warehouseInboundLineManager, 
            IRepositoryBaseAsync<ConversionSpecProduct, Guid> conversionSpecProductRepository, 
            IProductRepository<Product, Guid> productRepository,
            IUnitOfWork unitOfWork,
            IMapper mapper
        )
        {
            _warehouseInboundRepository = warehouseInboundRepository;
            _unitOfWork = unitOfWork;
            _storekeeperRepository = storekeeperRepository;
            _supplierRepository = supplierRepository;
            _warehouseRepository = warehouseRepository;
            _warehouseInboundManager = warehouseInboundManager;
            _productSupplierRepository = productSupplierRepository;
            _warehouseInboundLineManager = warehouseInboundLineManager;
            _conversionSpecProductRepository = conversionSpecProductRepository;
            _productRepository = productRepository;
            _mapper = mapper;
        }
        
        public async Task<Guid> CreateWarehouseInboundAsync(WarehouseInboundCreateDto model)
        {
            double amount = 0;
            double netAmount = 0;
            double netAmountLine = 0;
            string messageErrorAmount = "Tổng tiền không đúng";
            string messageErrorStorekeeper = "Thông tin thủ kho không hợp lệ";
            string messageErrorSupplier = "Thông tin nhà cung cấp không hợp lệ";
            string messageErrorWarehouse = "Thông tin kho không hợp lệ";
            string messageErrorProduct = "Thông tin sản phẩm không hợp lệ";
            string messageErrorQuality = "Thông tin số lượng không hợp lệ";

            var supplierId = model.SupplierId;
            var warehouseInbound = new WarehouseInbound
            {
                Batch = model.Batch,
                StorekeeperId = model.StorekeeperId,
                Status = model.Status,
                DiscountPercent = model.DiscountPercent,
                DiscountAmount = model.DiscountAmount,
                NetAmount = model.NetAmount,
                Amount = model.Amount,
                WarehouseId = model.WarehouseId,
                Lines = new List<WarehouseInboundLine>()
            };
             if (netAmount != warehouseInbound.NetAmount || amount != warehouseInbound.Amount) throw new ArgumentException(messageErrorAmount);
            _warehouseInboundManager.SetCreating(warehouseInbound);

            if (warehouseInbound.StorekeeperId.HasValue)
                {
                var storekeeper = await _storekeeperRepository.GetByIdAsync(warehouseInbound.StorekeeperId.Value);

                if (storekeeper is null)
                {
                    throw new ArgumentException(messageErrorStorekeeper);
                }
            }

            if (supplierId.HasValue)
            {
                var supplier = await _supplierRepository.GetByIdAsync(supplierId.Value);
                if (supplier is null)
                {
                    throw new ArgumentException(messageErrorSupplier);
                }
            }

            if (warehouseInbound.WarehouseId.HasValue)
            {
                var warehouse = await _warehouseRepository.GetByIdAsync(warehouseInbound.WarehouseId.Value);
                if (warehouse is null)
                {
                    throw new ArgumentException(messageErrorWarehouse);
                }
            }


            foreach (var line in model.Lines)
            {
                if (!_productSupplierRepository.GetAll().Any(x => x.SupplierId == model.SupplierId && x.ProductId == line.ProductId)) 
                    throw new ArgumentException(messageErrorProduct);
                var inboundLine = new WarehouseInboundLine
                {
                    ProductId = line.ProductId,
                    ProductName = line.ProductName,
                    UnitId = line.UnitId,
                    UnitName = line.UnitName,
                    Quantity = line.Quantity,
                    UnitPrice = line.UnitPrice,
                    Status = line.Status,
                    DiscountAmount = line.DiscountAmount,
                    DiscountPercent = line.DiscountPercent,
                };

                if (inboundLine.Quantity <= 0) 
                {
                    throw new ArgumentException(messageErrorQuality);
                }

                double totalPricePerProduct = inboundLine.UnitPrice * inboundLine.Quantity;

                double discountAmount = inboundLine.DiscountAmount!.Value;

                double discountPercent = inboundLine.DiscountPercent!.Value;

                double discountPerProduct = discountAmount + ((discountPercent / 100) * totalPricePerProduct);
               
                double netPricePerProduct = totalPricePerProduct - discountPerProduct; 

                netAmountLine += netPricePerProduct; // total value per product line

                var unitIdReq = inboundLine.UnitId;
                var quantityReq = inboundLine.Quantity;
                var product = _productRepository.GetAll()
                    .First(x => x.Id == inboundLine.ProductId);
                if (product.SmallestUnitId != inboundLine.UnitId)
                {
                    do
                    {
                        var packageSpecConsumable = _conversionSpecProductRepository.GetAll()
                            .FirstOrDefault(x => x.ProductId == inboundLine.ProductId && x.UnitId == unitIdReq);
                        if (packageSpecConsumable == null) break;

                        unitIdReq = packageSpecConsumable.ConvertUnitId;
                        inboundLine.ConvertQuantity = quantityReq * packageSpecConsumable.Quantity;
                        inboundLine.SmallestUnitName = packageSpecConsumable.ConvertUnitName;
                        quantityReq = inboundLine.ConvertQuantity;
                    } while (unitIdReq != product.SmallestUnitId);
                }
                else
                {
                    inboundLine.ConvertQuantity = inboundLine.Quantity;
                    inboundLine.SmallestUnitName = product.SmallestUnit.Name;
                }

                _warehouseInboundLineManager.SetCreating(inboundLine);
                warehouseInbound.Lines.Add(inboundLine);
                //warehouseInbound.Amount
            }


            bool isValidDiscountPercent = warehouseInbound.DiscountPercent.HasValue && warehouseInbound.DiscountPercent > 0 && warehouseInbound.DiscountPercent <= 100;

            double discount = warehouseInbound.DiscountAmount!.Value + ((warehouseInbound.DiscountPercent!.Value / 100) * netAmountLine);
            warehouseInbound.Amount = netAmountLine;
            warehouseInbound.NetAmount = netAmountLine - discount;
     
            await _warehouseInboundRepository.CreateAsync(warehouseInbound);

            await _unitOfWork.CommitAsync();
            return warehouseInbound.Id;
        }
    }
}
