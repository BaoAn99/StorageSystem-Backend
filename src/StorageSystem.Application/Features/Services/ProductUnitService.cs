using AutoMapper;
using StorageSystem.Application.Contracts.Repositories;
using StorageSystem.Application.Contracts.Repositories.Base;
using StorageSystem.Application.Contracts.Services;
using StorageSystem.Application.Models.ProductUnits;
using StorageSystem.Domain.Commons.Interfaces;
using StorageSystem.Domain.Entities.Products;

namespace StorageSystem.Application.Features.Services
{
    public class ProductUnitService : IProductUnitService
    {
        private readonly IProductUnitRepository<ProductUnit, Guid> _productUnitRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IEntityManager<ProductUnit> _entityManager;
        private readonly IMapper _mapper;
        public ProductUnitService(IProductUnitRepository<ProductUnit, Guid> productUnitRepository, IUnitOfWork unitOfWork, IEntityManager<ProductUnit> entityManager, IMapper mapper)
        {
            _productUnitRepository = productUnitRepository;
            _unitOfWork = unitOfWork;
            _entityManager = entityManager;
            _mapper = mapper;
        }
        public async Task<Guid> CreateProductUnitAsync(ProductUnitCreateDto model)
        {
            try
            {
                var productUnit = _mapper.Map<ProductUnit>(model);
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
                var productUnit = await _productUnitRepository.GetByIdAsync(id);
                if (productUnit != null)
                {
                    await _productUnitRepository.DeleteAsync(productUnit);
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
                var productUnit = await _productUnitRepository.GetByIdAsync(id);
                if (productUnit != null)
                {
                    productUnit.IsDeleted = true;
                    productUnit.IsPublished = false;
                    await _productUnitRepository.UpdateAsync(productUnit);
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
