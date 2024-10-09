﻿using AutoMapper;
using StorageSystem.Application.Contracts.Repositories;
using StorageSystem.Application.Contracts.Repositories.Base;
using StorageSystem.Application.Contracts.Services;
using StorageSystem.Application.Models.ProductTypes;
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
                var productType = _mapper.Map<ProductType>(model);
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
    }
}