using StorageSystem.Application.Models.Products;
using StorageSystem.Domain.Commons;

namespace StorageSystem.Application.Contracts.Services
{
    public interface IProductService
    {
        Task<Guid> CreateProductAsync(ProductCreateDto model);
        Task<Guid> UpdateProductAsync(ProductUpdateDto model);
        Task<bool> DeleteProductAsync(Guid id);
        Task<bool> SoftDeleteProductAsync(Guid id);
        Task<ProductForView> GetProductByIdAsync(Guid id);
        IEnumerable<ProductForView> GetAllProducts(QueryParams queryParams);
        IEnumerable<ProductForView> GetAllProductsWithoutPaging(QueryParamsWithoutPaging queryParams);
    }
}
