using StorageSystem.Application.Contracts.Repositories;
using StorageSystem.Application.Contracts.Repositories.Base;
using StorageSystem.Application.Contracts.Services;
using StorageSystem.Application.Models.ConversionSpec;
using StorageSystem.Domain.Commons.Interfaces;
using StorageSystem.Domain.Entities.PackageSpecs;

namespace StorageSystem.Application.Features.Services
{
    public class ConversionSpecProductService : IConversionSpecProductService
    {
        private readonly IConversionSpecProductRepository<ConversionSpecProduct, Guid> _repository;
        private readonly IEntityManager<ConversionSpecProduct> _entityManager;
        private readonly IUnitOfWork _unitOfWork;
        public ConversionSpecProductService(IConversionSpecProductRepository<ConversionSpecProduct, Guid> repository, IEntityManager<ConversionSpecProduct> entityManager, IUnitOfWork unitOfWork)
        {
            _repository = repository;
            _entityManager = entityManager;
            _unitOfWork = unitOfWork;
        }
        public Task<Guid> CreateConversionSpecProductAsync(ConversionSpecProductCreateDto model)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> DeleteConversionSpecProductAsync(Guid id)
        {
            try
            {
                var conversionSpecProduct = await _repository.FindConversionSpecProductByIdAsync(id);
                if (conversionSpecProduct != null)
                {
                    await _repository.DeleteConversionSpecProductAsync(conversionSpecProduct);
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

        public IEnumerable<ConversionSpecProductForView> GetAllConversionSpecProducts()
        {
            throw new NotImplementedException();
        }

        public Task<ConversionSpecProductForView> GetConversionSpecProductByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> SoftDeleteConversionSpecProductAsync(Guid id)
        {
            try
            {
                var conversionSpecProduct = await _repository.FindConversionSpecProductByIdAsync(id);
                if (conversionSpecProduct != null)
                {
                    conversionSpecProduct.IsDeleted = true;
                    conversionSpecProduct.IsPublished = false;
                    await _repository.UpdateConversionSpecProductAsync(conversionSpecProduct);
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

        public Task<Guid> UpdateConversionSpecProductAsync(ConversionSpecProductUpdateDto model)
        {
            throw new NotImplementedException();
        }
    }
}
