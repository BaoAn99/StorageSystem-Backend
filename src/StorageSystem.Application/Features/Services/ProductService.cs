using AutoMapper;
using StorageSystem.Application.Contracts.Repositories;
using StorageSystem.Application.Contracts.Repositories.Base;
using StorageSystem.Application.Contracts.Services;
using StorageSystem.Application.Models.Products;
using StorageSystem.Domain.Commons;
using StorageSystem.Domain.Commons.Interfaces;
using StorageSystem.Domain.Entities.PackageSpecs;
using StorageSystem.Domain.Entities.Products;

namespace StorageSystem.Application.Features.Services
{
    public class ProductService : IProductService
    {
        private readonly IEntityManager<Product> _productManager;
        private readonly IEntityManager<ProductImage> _productImageManager;
        private readonly IProductRepository<Product, Guid> _productRepository;
        private readonly IRepositoryBaseAsync<ConversionSpecProduct, Guid> _conversionRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IRepositoryBaseAsync<ConversionSpecProduct, Guid> _conversionSpecProductRepository;
        public ProductService(IEntityManager<Product> productManager, IEntityManager<ProductImage> productImageManager, IUnitOfWork unitOfWork, IProductRepository<Product, Guid> productRepository, IMapper mapper, IRepositoryBaseAsync<ConversionSpecProduct, Guid> conversionRepository, IRepositoryBaseAsync<ConversionSpecProduct, Guid> conversionSpecProductRepository)
        {
            _productManager = productManager;
            _productImageManager = productImageManager;
            _unitOfWork = unitOfWork;
            _productRepository = productRepository;
            _mapper = mapper;
            _conversionRepository = conversionRepository;
            _conversionSpecProductRepository = conversionSpecProductRepository;
        }

        public async Task<double> CalculatePriceWithUnitConversion(CalculatePriceWithUnitConversionDto model)
        {
            var unitIdReq = model.UnitId;
            var quantityReq = model.Quantity;
            var product = await _productRepository.GetByIdAsync(model.ProductId);
            if (product != null)
            {
                if (product.SmallestUnitId != unitIdReq)
                {
                    do
                    {
                        var packageSpecConsumable = _conversionSpecProductRepository.FindByCondition(x => x.ProductId == model.ProductId && x.UnitId == unitIdReq).FirstOrDefault();
                        if (packageSpecConsumable == null) throw new AggregateException("Invalid UnitId!");

                        unitIdReq = packageSpecConsumable.ConvertUnitId;
                        quantityReq = quantityReq * packageSpecConsumable.Quantity;
                    } while (unitIdReq != product.SmallestUnitId);
                    return quantityReq * product.Price;
                }
                
                return product.Price;
            }

            return -1;
        }

        public async Task<Guid> CreateProductAsync(ProductCreateDto model)
        {
            try
            {
                var product = _mapper.Map<Product>(model);
                _productManager.SetCreating(product);
                //_productImageManager.SetCreating(product.Images);
                await _productRepository.CreateAsync(product);

                await _unitOfWork.CommitAsync();
                return product.Id;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }

        public async Task<bool> DeleteProductAsync(Guid id)
        {
            try
            {
                var product = await _productRepository.GetByIdAsync(id);
                if (product != null)
                {
                    await _productRepository.DeleteAsync(product);
                    await _unitOfWork.CommitAsync();
                    return true;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return false;
        }

        public IEnumerable<ProductForView> GetAllProducts(QueryParams queryParams)
        {
            var products = _productRepository.GetAll(queryParams).ToList();
            IEnumerable<ProductForView> productForView = _mapper.Map<IEnumerable<ProductForView>>(products);
            return productForView;
        }

        public IEnumerable<ProductForView> GetAllProductsWithoutPaging(QueryParamsWithoutPaging queryParams)
        {
            var products = _productRepository.GetAllWithoutPaging(queryParams).ToList();
            IEnumerable<ProductForView> productForView = _mapper.Map<IEnumerable<ProductForView>>(products);
            return productForView;
        }

        public Task<ProductForView> GetProductByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> SoftDeleteProductAsync(Guid id)
        {
            try
            {
                var product = _productRepository.FindByCondition(p => p.Id.Equals(id), false, p => p.Images).FirstOrDefault();
                if (product != null)
                {
                    product.IsDeleted = true;
                    product.IsPublished = false;
                    foreach(var image in product.Images)
                    {
                        image.IsDeleted = true;
                        image.IsPublished = false;
                    }
                    await _productRepository.UpdateAsync(product);
                    await _unitOfWork.CommitAsync();
                    return true;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return false;
        }

        public Task<Guid> UpdateProductAsync(ProductUpdateDto model)
        {
            throw new NotImplementedException();
        }
    }
}
