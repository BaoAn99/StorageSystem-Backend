using StorageSystem.Application.Models.Product;

namespace StorageSystem.Application.Contracts.Services
{
    public interface IProductService
    {
        Task<Guid> CreateProductAsync(ProductCreateDto model);
        Task<Guid> UpdateProductAsync(ProductUpdateDto model);
        bool DeleteProductAsync(Guid id);
        Task<ProductForView> GetProductByIdAsync(Guid id);
        IQueryable<ProductForView> GetAllProducts();
    }
}
