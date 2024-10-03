using StorageSystem.Application.Contracts.Services;
using StorageSystem.Application.Models.Product;

namespace StorageSystem.Application.Features.Services
{
    public class ProductService : IProductService
    {
        public Task<Guid> CreateProductAsync(ProductCreateDto model)
        {
            throw new NotImplementedException();
        }

        public bool DeleteProductAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public IQueryable<ProductForView> GetAllProducts()
        {
            throw new NotImplementedException();
        }

        public Task<ProductForView> GetProductByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<Guid> UpdateProductAsync(ProductUpdateDto model)
        {
            throw new NotImplementedException();
        }
    }
}
