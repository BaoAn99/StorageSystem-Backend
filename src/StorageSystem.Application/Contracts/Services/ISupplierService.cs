using StorageSystem.Application.Models.Supplier;

namespace StorageSystem.Application.Contracts.Services
{
    public interface ISupplierService
    {
        Task<Guid> CreateSupplierAsync(SupplierCreateDto model);
        Task<Guid> UpdateSupplierAsync(SupplierUpdateDto model);
        bool DeleteSupplierAsync(Guid id);
        Task<SupplierForView> GetSupplierByIdAsync(Guid id);
        IQueryable<SupplierForView> GetAllSuppliers();
    }
}
