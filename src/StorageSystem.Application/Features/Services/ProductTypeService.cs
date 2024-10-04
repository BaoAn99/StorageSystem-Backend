using StorageSystem.Application.Contracts.Repositories;
using StorageSystem.Application.Contracts.Repositories.Base;
using StorageSystem.Application.Contracts.Services;
using StorageSystem.Application.Models.ProductType;
using StorageSystem.Domain.Commons.Interfaces;
using StorageSystem.Domain.Entities.Products;

namespace StorageSystem.Application.Features.Services
{
    public class ProductTypeService : IProductTypeService
    {
        private readonly IProductTypeRepository<ProductType, Guid> _productTypeRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IEntityManager<ProductType> _entityManager;

        public ProductTypeService(IProductTypeRepository<ProductType, Guid> productTypeRepository, IEntityManager<ProductType> entityManager, IUnitOfWork unitOfWork)
        {
            _productTypeRepository = productTypeRepository;
            _entityManager = entityManager;
            _unitOfWork = unitOfWork;
        }

        public async Task<Guid> CreateProductTypeAsync(ProductTypeCreateDto model)
        {
            try
            {
                var productType = new ProductType()
                {
                    Name = model.Name,
                    Description = model.Description,
                };
                _entityManager.SetCreating(productType);
                await _productTypeRepository.CreateAsync(productType);

                await _unitOfWork.CommitAsync();
                return productType.Id;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }

        public async Task<bool> DeleteProductTypeAsync(Guid id)
        {
            try
            {
                var productType = await _productTypeRepository.FindProductTypeByIdAsync(id);
                if (productType != null)
                {
                    await _productTypeRepository.DeleteProductTypeAsync(productType);
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

        public IEnumerable<ProductTypeForView> AllProductTypes => throw new NotImplementedException();

        public Task<ProductTypeForView> GetProductTypeByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<Guid> UpdateProductTypeAsync(ProductTypeUpdateDto model)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> SoftDeleteProductTypeAsync(Guid id)
        {
            try
            {
                var productType = await _productTypeRepository.FindProductTypeByIdAsync(id);
                if (productType != null)
                {
                    productType.IsDeleted = true;
                    productType.IsPublished = false;
                    await _productTypeRepository.UpdateProductTypeAsync(productType);
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
    }
}
