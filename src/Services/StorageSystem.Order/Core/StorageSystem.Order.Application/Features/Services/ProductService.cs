using AutoMapper;
using StorageSystem.Order.Application.Contracts.Repositories;
using StorageSystem.Order.Application.Contracts.Repositories.Base;
using StorageSystem.Order.Application.Contracts.Services;
using StorageSystem.Order.Application.Models.Products;
using StorageSystem.Order.Domain.Commons;
using StorageSystem.Order.Domain.Commons.Interfaces;
using StorageSystem.Order.Domain.Entities.Products;

namespace StorageSystem.Order.Application.Features.Services
{
    public class ProductService : IProductService
    {
        private readonly IEntityManager<Product> _productManager;
        private readonly IEntityManager<ProductImage> _productImageManager;
        private readonly IProductRepository<Product, Guid> _productRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public ProductService(IEntityManager<Product> productManager, IEntityManager<ProductImage> productImageManager, IUnitOfWork unitOfWork, IProductRepository<Product, Guid> productRepository, IMapper mapper)
        {
            _productManager = productManager;
            _productImageManager = productImageManager;
            _unitOfWork = unitOfWork;
            _productRepository = productRepository;
            _mapper = mapper;
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
            IEnumerable<ProductForView> productForView = _mapper.Map<List<ProductForView>>(products);

            return productForView;
        }

        public IEnumerable<ProductForView> GetAllProductsWithoutPaging(QueryParamsWithoutPaging queryParams)
        {
            throw new NotImplementedException();
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
