using StorageSystem.Order.Application.Models.WarehouseInbounds;

namespace StorageSystem.Order.Application.Contracts.Services
{
    public interface IWarehouseInboundService
    {
        Task<Guid> CreateWarehouseInboundAsync(WarehouseInboundCreateDto model);
        //Task<Guid> UpdateWarehouseInboundAsync(WarehouseInboundUpdateDto model);
        //Task<bool> DeleteWarehouseInboundAsync(Guid id);
        //Task<bool> SoftDeleteWarehouseInboundAsync(Guid id);
        //Task<WarehouseInboundForView> GetWarehouseInboundByIdAsync(Guid id);
        //IEnumerable<WarehouseInboundForView> GetAllWarehouseInbounds();
    }
}
