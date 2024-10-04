using StorageSystem.Application.Contracts.Repositories;
using StorageSystem.Application.Contracts.Repositories.Base;
using StorageSystem.Application.Contracts.Services;
using StorageSystem.Application.Models.Product;
using StorageSystem.Domain.Commons.Interfaces;
using StorageSystem.Domain.Entities.Products;

namespace StorageSystem.Application.Features.Services
{
    public class ProductService : IProductService
    {
        private readonly IEntityManager<Product> _productManager;
        private readonly IEntityManager<ProductImage> _productImageManager;
        private readonly IProductRepository<Product, Guid> _productRepository;
        private readonly IUnitOfWork _unitOfWork;
        public ProductService(IEntityManager<Product> productManager, IEntityManager<ProductImage> productImageManager, IUnitOfWork unitOfWork, IProductRepository<Product, Guid> productRepository)
        {
            _productManager = productManager;
            _productImageManager = productImageManager;
            _unitOfWork = unitOfWork;
            _productRepository = productRepository;
        }

        public async Task<Guid> CreateProductAsync(ProductCreateDto model)
        {
            try
            {
                var images = new List<ProductImage>();
                foreach(var image in model.Images)
                {
                    var productImage = new ProductImage
                    {
                        ImagePath = image.ImagePath,
                        Caption = image.Caption,
                        IsImageFeature = image.IsImageFeature,
                        Description = image.Description,
                    };
                    _productImageManager.SetCreating(productImage);
                    images.Add(productImage);
                }

                var product = new Product
                {
                    Name = model.Name,
                    Price = model.Price,
                    Description = model.Description,
                    ThumbnailImage = model.ThumbnailImage,
                    TypeId = model.TypeId,
                    SmallestUnitId = model.SmallestUnitId,
                    Images = images
                };
                _productManager.SetCreating(product);
                await _productRepository.CreateProductAsync(product);

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
                var product = await _productRepository.FindProductByIdAsync(id);
                if (product != null)
                {
                    await _productRepository.DeleteProductAsync(product);
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

        public IEnumerable<ProductForView> GetAllProducts()
        {
            var product = _productRepository.GetAllProducts();
            IEnumerable<ProductForView> productForView = new List<ProductForView>();

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
                    await _productRepository.UpdateProductAsync(product);
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
