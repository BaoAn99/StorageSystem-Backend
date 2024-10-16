using AutoMapper;
using StorageSystem.Application.Contracts.Repositories;
using StorageSystem.Application.Contracts.Repositories.Base;
using StorageSystem.Application.Contracts.Services;
using StorageSystem.Application.Models.ProductTypes;
using StorageSystem.Domain.Commons;
using StorageSystem.Domain.Commons.Interfaces;
using StorageSystem.Domain.Entities.Products;

namespace StorageSystem.Application.Features.Services
{
    public class ProductTypeService : IProductTypeService
    {
        private readonly IProductTypeRepository<ProductType, Guid> _productTypeRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IEntityManager<ProductType> _entityManager;
        private readonly IMapper _mapper;

        public ProductTypeService(IProductTypeRepository<ProductType, Guid> productTypeRepository, IEntityManager<ProductType> entityManager, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _productTypeRepository = productTypeRepository;
            _entityManager = entityManager;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Guid> CreateProductTypeAsync(ProductTypeCreateDto model)
        {
            try
            {
                Console.WriteLine("2: " + Environment.CurrentManagedThreadId);
                Console.WriteLine("");
                var productType = _mapper.Map<ProductType>(model);
                _entityManager.SetCreating(productType);
                await _productTypeRepository.CreateAsync(productType);
                Console.WriteLine("5: " + Environment.CurrentManagedThreadId);
                Console.WriteLine("");
                await _unitOfWork.CommitAsync();
                Console.WriteLine("6: " + Environment.CurrentManagedThreadId);
                Console.WriteLine("");
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
                var productType = await _productTypeRepository.GetByIdAsync(id);
                if (productType != null)
                {
                    await _productTypeRepository.DeleteAsync(productType);
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

        public IEnumerable<ProductTypeForView> GetAllProductTypes() => throw new NotImplementedException();

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
                var productType = await _productTypeRepository.GetByIdAsync(id);
                if (productType != null)
                {
                    productType.IsDeleted = true;
                    productType.IsPublished = false;
                    await _productTypeRepository.UpdateAsync(productType);
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

        public IEnumerable<ProductTypeForView> GetAllProductTypes(QueryParams queryParams)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ProductTypeForView> GetAllProductTypesWithoutPaging(QueryParamsWithoutPaging queryParams)
        {
            throw new NotImplementedException();
        }

        public void Test()
        {
            try
            {
                _productTypeRepository.Test();
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public async Task Async()
        {
            Console.WriteLine("Thread Start 2: " + Environment.CurrentManagedThreadId);
            Console.WriteLine("");
            Async1();
            await Async1();
            Async2();
            await Async2();
            Console.WriteLine("");
            Console.WriteLine("Thread Start 2: " + Environment.CurrentManagedThreadId);
            Console.WriteLine("----------------------------------------------------------------------");
        }

        public async Task Async1()
        {
            Console.WriteLine("Async1: " + Environment.CurrentManagedThreadId);
            await Async2();
            Console.WriteLine("Async1: " + Environment.CurrentManagedThreadId);
        }

        public async Task Async2()
        {
            Console.WriteLine("Async2: " + Environment.CurrentManagedThreadId);
        }
    }
}
