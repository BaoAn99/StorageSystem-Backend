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
            string messageErrorAmount = "Tổng tiền không đúng";
            string messageErrorStorekeeper = "Thông tin thủ kho không hợp lệ";
            string messageErrorSupplier = "Thông tin nhà cung cấp không hợp lệ";

            var warehouseInbound = new WarehouseInbound
            {
                Batch = model.Batch,
                StorekeeperId = model.StorekeeperId,
                Status = model.Status,
                DiscountPercent = model.DiscountPercent,
                DiscountAmount = model.DiscountAmount,
                Lines = new List<WarehouseInboundLine>()
            };
            _warehouseInboundManager.SetCreating(warehouseInbound);

            var storekeeperId = model.StorekeeperId;

            if (storekeeperId.HasValue)
                {
                var storekeeper = _storekeeperRepository.GetByIdAsync(model.StorekeeperId.Value);

                if (storekeeper is null)
                {
                    throw new ArgumentException(messageErrorStorekeeper);
                }
            }

            var supplierId = model.SupplierId;

            if (supplierId.HasValue)
            {
                var supplier = _supplierRepository.GetByIdAsync(model.SupplierId.Value);

                if (supplier is null)
                {
                    throw new ArgumentException(messageErrorSupplier);
                }
            }

            foreach (var line in model.Lines)
            {
                if (!_productSupplierRepository.GetAll().Any(x => x.SupplierId == model.SupplierId && x.ProductId == line.ProductId)) throw new ArgumentException("");
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

                double price = inboundLine.UnitPrice * inboundLine.Quantity;

                double netPrice = price;

                bool isValidDiscountPercent = line.DiscountPercent.HasValue && line.DiscountPercent > 0 && line.DiscountPercent <= 100;

                if (isValidDiscountPercent)
                {
                    netPrice -= netPrice * (line.DiscountPercent.Value / 100);
                }

                if (line.DiscountAmount.HasValue)
                {
                     netPrice -= line.DiscountAmount.Value;
                }

                amount += netPrice;
                netAmount = amount;

                var unitIdReq = line.UnitId;
                var quantityReq = line.Quantity;
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


            if (amount != model.Amount) throw new ArgumentException(messageErrorAmount);

            netAmount = amount;

            if (warehouseInbound.DiscountPercent.HasValue && warehouseInbound.DiscountPercent > 0 && warehouseInbound.DiscountPercent <= 100)
            {
                //inboundLine.
                netAmount -= netAmount * (model.DiscountPercent.Value / 100);
            }
            if (warehouseInbound.DiscountAmount.HasValue)
            {
                netAmount -= model.DiscountAmount.Value;
            }
           
            if (netAmount != model.NetAmount) throw new ArgumentException(messageErrorAmount);
            await _warehouseInboundRepository.CreateAsync(warehouseInbound);

            await _unitOfWork.CommitAsync();
            return warehouseInbound.Id;
        }
    }
}
