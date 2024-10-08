using StorageSystem.Application.Models.Warehouses;

namespace StorageSystem.Application.Contracts.Services
{
    public interface IWarehouseService
    {
        Task<Guid> CreateWarehouseAsync(WarehouseCreateDto model);
        Task<Guid> UpdateWarehouseAsync(WarehouseUpdateDto model);
        Task<bool> DeleteWarehouseAsync(Guid id);
        Task<bool> SoftDeleteWarehouseAsync(Guid id);
        Task<WarehouseForView> GetWarehouseByIdAsync(Guid id);
        IEnumerable<WarehouseForView> GetAllWarehouses();
    }
}
