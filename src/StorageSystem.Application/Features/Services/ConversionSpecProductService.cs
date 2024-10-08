using AutoMapper;
using StorageSystem.Application.Contracts.Repositories;
using StorageSystem.Application.Contracts.Repositories.Base;
using StorageSystem.Application.Contracts.Services;
using StorageSystem.Application.Models.ConversionSpecs;
using StorageSystem.Domain.Commons.Interfaces;
using StorageSystem.Domain.Entities.PackageSpecs;
using StorageSystem.Domain.Entities.Products;

namespace StorageSystem.Application.Features.Services
{
    public class ConversionSpecProductService : IConversionSpecProductService
    {
        private readonly IConversionSpecProductRepository<ConversionSpecProduct, Guid> _repository;
        private readonly IProductRepository<Product, Guid> _productRepository;
        private readonly IEntityManager<ConversionSpecProduct> _entityManager;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public ConversionSpecProductService(IConversionSpecProductRepository<ConversionSpecProduct, Guid> repository, IEntityManager<ConversionSpecProduct> entityManager, IUnitOfWork unitOfWork, IProductRepository<Product, Guid> productRepository, IMapper mapper)
        {
            _repository = repository;
            _entityManager = entityManager;
            _unitOfWork = unitOfWork;
            _productRepository = productRepository;
            _mapper = mapper;
        }
        public async Task<Guid> CreateConversionSpecProductAsync(ConversionSpecProductCreateDto model)
        {
            if (model.UnitId == model.ConvertUnitId) throw new ArgumentException("");

            var product = await _productRepository.GetByIdAsync(model.ProductId);
            if (product != null)
            {
                var productUnit = product.SmallestUnitId;
                if (model.UnitId == productUnit) throw new Exception();
                if (_repository.FindByCondition(x => x.ProductId == product.Id).Any(
                    x => (x.UnitId == model.UnitId && x.ConvertUnitId == model.ConvertUnitId)
                        || (x.UnitId == model.ConvertUnitId && x.ConvertUnitId == model.UnitId))) throw new ArgumentException("");

                var conversionSpecProduct = _mapper.Map<ConversionSpecProduct>(product);
                _entityManager.SetCreating(conversionSpecProduct);
                await _repository.CreateAsync(conversionSpecProduct);

                await _unitOfWork.CommitAsync();
                return conversionSpecProduct.Id;
            }
            throw new ArgumentException("Không tồn tại sản phẩm");
        }

        public async Task<bool> DeleteConversionSpecProductAsync(Guid id)
        {
            try
            {
                var conversionSpecProduct = await _repository.GetByIdAsync(id);
                if (conversionSpecProduct != null)
                {
                    await _repository.DeleteAsync(conversionSpecProduct);
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
                var conversionSpecProduct = await _repository.GetByIdAsync(id);
                if (conversionSpecProduct != null)
                {
                    conversionSpecProduct.IsDeleted = true;
                    conversionSpecProduct.IsPublished = false;
                    await _repository.UpdateAsync(conversionSpecProduct);
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
