using AutoMapper;
using StorageSystem.Application.Contracts.Repositories;
using StorageSystem.Application.Contracts.Repositories.Base;
using StorageSystem.Application.Contracts.Services;
using StorageSystem.Application.Models.Products;
using StorageSystem.Application.Models.ProductTypes;
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
        public ProductService(IEntityManager<Product> productManager, IEntityManager<ProductImage> productImageManager, IUnitOfWork unitOfWork, IProductRepository<Product, Guid> productRepository, IMapper mapper, IRepositoryBaseAsync<ConversionSpecProduct, Guid> conversionRepository)
        {
            _productManager = productManager;
            _productImageManager = productImageManager;
            _unitOfWork = unitOfWork;
            _productRepository = productRepository;
            _mapper = mapper;
            _conversionRepository = conversionRepository;
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
            foreach (var item in productForView)
            {
                var a = new ConvertUnitProductForView();
                var b = products.FirstOrDefault(x => x.Id == item.Id);
                if (b != null && b.ConversionSpecProducts.Any())
                {
                    a.UnitId = b.ConversionSpecProducts[b.ConversionSpecProducts.Count - 1].ConvertUnitId;
                    a.UnitName = b.ConversionSpecProducts[b.ConversionSpecProducts.Count - 1].ConvertUnitName;
                    item.Units.Add(a);
                }
            }
            return productForView;
        }

        public IEnumerable<ProductForView> GetAllProductsWithoutPaging(QueryParamsWithoutPaging queryParams)
        {
            var products = _productRepository.GetAllWithoutPaging(queryParams).ToList();
            IEnumerable<ProductForView> productForView = _mapper.Map<IEnumerable<ProductForView>>(products);
            foreach (var item in productForView)
            {
                var a = new ConvertUnitProductForView();
                var b = products.FirstOrDefault(x => x.Id == item.Id);
                if (b != null && b.ConversionSpecProducts.Any())
                {
                    a.UnitId = b.ConversionSpecProducts[b.ConversionSpecProducts.Count - 1].ConvertUnitId;
                    a.UnitName = b.ConversionSpecProducts[b.ConversionSpecProducts.Count - 1].ConvertUnitName;
                    item.Units.Add(a);
                }
            }
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
