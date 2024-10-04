using StorageSystem.Application.Contracts.Repositories;
using StorageSystem.Application.Contracts.Repositories.Base;
using StorageSystem.Application.Contracts.Services;
using StorageSystem.Application.Models.ProductUnit;
using StorageSystem.Domain.Commons.Interfaces;
using StorageSystem.Domain.Entities.Products;

namespace StorageSystem.Application.Features.Services
{
    public class ProductUnitService : IProductUnitService
    {
        private readonly IProductUnitRepository<ProductUnit, Guid> _productUnitRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IEntityManager<ProductUnit> _entityManager;
        public ProductUnitService(IProductUnitRepository<ProductUnit, Guid> productUnitRepository, IUnitOfWork unitOfWork, IEntityManager<ProductUnit> entityManager)
        {
            _productUnitRepository = productUnitRepository;
            _unitOfWork = unitOfWork;
            _entityManager = entityManager;
        }
        public async Task<Guid> CreateProductUnitAsync(ProductUnitCreateDto model)
        {
            try
            {
                var productUnit = new ProductUnit()
                {
                    Name = model.Name,
                    Description = model.Description,
                    
                };
                _entityManager.SetCreating(productUnit);
                await _productUnitRepository.CreateAsync(productUnit);

                await _unitOfWork.CommitAsync();
                return productUnit.Id;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }

        public async Task<bool> DeleteProductUnitAsync(Guid id)
        {
            try
            {
                var productUnit = await _productUnitRepository.FindProductUnitByIdAsync(id);
                if (productUnit != null)
                {
                    await _productUnitRepository.DeleteProductUnitAsync(productUnit);
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

        public IEnumerable<ProductUnitForView> GetAllProductUnits()
        {
            throw new NotImplementedException();
        }

        public Task<ProductUnitForView> GetProductUnitByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> SoftDeleteProductUnitAsync(Guid id)
        {
            try
            {
                var productUnit = await _productUnitRepository.FindProductUnitByIdAsync(id);
                if (productUnit != null)
                {
                    productUnit.IsDeleted = true;
                    productUnit.IsPublished = false;
                    await _productUnitRepository.UpdateProductUnitAsync(productUnit);
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

        public Task<Guid> UpdateProductUnitAsync(ProductUnitUpdateDto model)
        {
            throw new NotImplementedException();
        }
    }
}
