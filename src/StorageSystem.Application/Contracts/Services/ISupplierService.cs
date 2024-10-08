using StorageSystem.Application.Models.Suppliers;

namespace StorageSystem.Application.Contracts.Services
{
    public interface ISupplierService
    {
        Task<Guid> CreateSupplierAsync(SupplierCreateDto model);
        Task<Guid> UpdateSupplierAsync(SupplierUpdateDto model);
        Task<bool> DeleteSupplierAsync(Guid id);
        Task<bool> SoftDeleteSupplierAsync(Guid id);
        Task<SupplierForView> GetSupplierByIdAsync(Guid id);
        IEnumerable<SupplierForView> GetAllSuppliers();
    }
}
