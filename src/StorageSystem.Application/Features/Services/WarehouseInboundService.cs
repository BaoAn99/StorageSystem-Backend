using StorageSystem.Application.Contracts.Repositories;
using StorageSystem.Application.Contracts.Repositories.Base;
using StorageSystem.Application.Contracts.Services;
using StorageSystem.Application.Models.WarehouseInbounds;
using StorageSystem.Domain.Commons.Interfaces;
using StorageSystem.Domain.Entities.PackageSpecs;
using StorageSystem.Domain.Entities.Products;
using StorageSystem.Domain.Entities.Warehouses;

namespace StorageSystem.Application.Features.Services
{
    public class WarehouseInboundService : IWarehouseInboundService
    {
        private readonly IWarehouseInboundRepository<WarehouseInbound, Guid> _warehouseInboundRepository;
        private readonly IProductRepository<Product, Guid> _productRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IEntityManager<WarehouseInbound> _warehouseInboundManager;
        private readonly IEntityManager<WarehouseInboundLine> _warehouseInboundLineManager;
        private readonly IRepositoryBaseAsync<ProductSupplier, Guid> _productSupplierRepository;
        private readonly IRepositoryBaseAsync<ConversionSpecProduct, Guid> _conversionSpecProductRepository;

        public WarehouseInboundService(IWarehouseInboundRepository<WarehouseInbound, Guid> warehouseInboundRepository, IUnitOfWork unitOfWork, IEntityManager<WarehouseInbound> warehouseInboundManager, IRepositoryBaseAsync<ProductSupplier, Guid> productSupplierRepository, IEntityManager<WarehouseInboundLine> warehouseInboundLineManager, IRepositoryBaseAsync<ConversionSpecProduct, Guid> conversionSpecProductRepository, IProductRepository<Product, Guid> productRepository)
        {
            _warehouseInboundRepository = warehouseInboundRepository;
            _unitOfWork = unitOfWork;
            _warehouseInboundManager = warehouseInboundManager;
            _productSupplierRepository = productSupplierRepository;
            _warehouseInboundLineManager = warehouseInboundLineManager;
            _conversionSpecProductRepository = conversionSpecProductRepository;
            _productRepository = productRepository;
        }

        public async Task<Guid> CreateWarehouseInboundAsync(WarehouseInboundCreateDto model)
        {
            var amount = 0;
            var netAmount = 0;

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
                if (line.DiscountPercent.HasValue && line.DiscountPercent > 0 && line.DiscountPercent <= 100)
                {
                    //inboundLine.
                }
                if (line.DiscountAmount.HasValue)
                {

                }
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

            if (warehouseInbound.DiscountPercent.HasValue && warehouseInbound.DiscountPercent > 0 && warehouseInbound.DiscountPercent <= 100)
            {
                //inboundLine.
            }
            if (warehouseInbound.DiscountAmount.HasValue)
            {

            }
            if (amount != model.Amount) throw new ArgumentException("");
            if (netAmount != model.NetAmount) throw new ArgumentException("");
            await _warehouseInboundRepository.CreateAsync(warehouseInbound);

            await _unitOfWork.CommitAsync();
            return warehouseInbound.Id;
        }
    }
}
