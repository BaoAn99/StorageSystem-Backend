using StorageSystem.Application.Models.ProductUnits;
using StorageSystem.Domain.Commons;

namespace StorageSystem.Application.Contracts.Services
{
    public interface IProductUnitService
    {
        Task<Guid> CreateProductUnitAsync(ProductUnitCreateDto model);
        Task<Guid> UpdateProductUnitAsync(ProductUnitUpdateDto model);
        Task<bool> DeleteProductUnitAsync(Guid id);
        Task<bool> SoftDeleteProductUnitAsync(Guid id);
        Task<ProductUnitForView> GetProductUnitByIdAsync(Guid id);
        IEnumerable<ProductUnitForView> GetAllProductUnits(QueryParams queryParams);
        IEnumerable<ProductUnitForView> GetAllProductUnitsWithoutPaging(QueryParamsWithoutPaging queryParams);
    }
}
