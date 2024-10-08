using StorageSystem.Application.Models.ProductTypes;

namespace StorageSystem.Application.Contracts.Services
{
    public interface IProductTypeService
    {
        Task<Guid> CreateProductTypeAsync(ProductTypeCreateDto model);
        Task<Guid> UpdateProductTypeAsync(ProductTypeUpdateDto model);
        Task<bool> DeleteProductTypeAsync(Guid id);
        Task<bool> SoftDeleteProductTypeAsync(Guid id);
        Task<ProductTypeForView> GetProductTypeByIdAsync(Guid id);
        IEnumerable<ProductTypeForView> GetAllProductTypes();
    }
}
