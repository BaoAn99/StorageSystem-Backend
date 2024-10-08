using AutoMapper;
using StorageSystem.Application.Contracts.Repositories;
using StorageSystem.Application.Contracts.Repositories.Base;
using StorageSystem.Application.Contracts.Services;
using StorageSystem.Application.Models.Suppliers;
using StorageSystem.Domain.Commons.Interfaces;
using StorageSystem.Domain.Entities.Suppliers;

namespace StorageSystem.Application.Features.Services
{
    public class SupplierService : ISupplierService
    {
        private readonly ISupplierRepository<Supplier, Guid> _supplierRepository;
        private readonly IEntityManager<Supplier> _entityManager;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public SupplierService(ISupplierRepository<Supplier, Guid> supplierRepository, IEntityManager<Supplier> entityManager, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _supplierRepository = supplierRepository;
            _entityManager = entityManager;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Guid> CreateSupplierAsync(SupplierCreateDto model)
        {
            try
            {
                var supplier = _mapper.Map<Supplier>(model);
                _entityManager.SetCreating(supplier);
                await _supplierRepository.CreateAsync(supplier);
                await _unitOfWork.CommitAsync();
                return supplier.Id;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
            
        }

        public async Task<bool> DeleteSupplierAsync(Guid id)
        {
            try
            {
                var supplier = await _supplierRepository.GetByIdAsync(id);
                if (supplier != null)
                {
                    await _supplierRepository.DeleteAsync(supplier);
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

        public IEnumerable<SupplierForView> GetAllSuppliers()
        {
            throw new NotImplementedException();
        }

        public Task<SupplierForView> GetSupplierByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> SoftDeleteSupplierAsync(Guid id)
        {
            try
            {
                var supplier = await _supplierRepository.GetByIdAsync(id);
                if (supplier != null)
                {
                    supplier.IsDeleted = true;
                    supplier.IsPublished = false;
                    await _supplierRepository.UpdateAsync(supplier);
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

        public Task<Guid> UpdateSupplierAsync(SupplierUpdateDto model)
        {
            throw new NotImplementedException();
        }
    }
}
