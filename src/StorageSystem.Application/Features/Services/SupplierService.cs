using StorageSystem.Application.Contracts.Repositories;
using StorageSystem.Application.Contracts.Services;
using StorageSystem.Application.Models.Supplier;
using StorageSystem.Domain.Entities.Suppliers;

namespace StorageSystem.Application.Features.Services
{
    public class SupplierService : ISupplierService
    {
        private readonly ISupplierRepository<Supplier, Guid> _supplierRepository;

        public SupplierService(ISupplierRepository<Supplier, Guid> supplierRepository)
        {
            _supplierRepository = supplierRepository;
        }

        public async Task<Guid> CreateSupplierAsync(SupplierCreateDto model)
        {
            try
            {
                var supplier = new Supplier()
                {
                    Address = model.Address,
                    ContactName = model.ContactName,
                    Description = model.Description,
                    Name = model.Name,
                    Email = model.Email,
                    Phone = model.Phone,
                    CreatedByName = "Khuong",
                    CreatedByUserId = "Khuong",
                    UpdatedByName = "Khuong",
                    UpdatedByUserId = "Khuong",
                    
                };
                await _supplierRepository.CreateAsync(supplier);
                await _supplierRepository.SaveChangesAsync();
                return supplier.Id;
            }
            catch (Exception ex)
            {

                throw;
            }
            
        }

        public bool DeleteSupplierAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public IQueryable<SupplierForView> GetAllSuppliers()
        {
            throw new NotImplementedException();
        }

        public Task<SupplierForView> GetSupplierByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<Guid> UpdateSupplierAsync(SupplierUpdateDto model)
        {
            throw new NotImplementedException();
        }
    }
}
