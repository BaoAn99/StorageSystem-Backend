using StorageSystem.Application.Contracts.Services;
using StorageSystem.Application.Models.Product;

namespace StorageSystem.Application.Features.Services
{
    public class ProductService : IProductService
    {
        public Guid Create(ProductCreateDto model)
        {
            throw new NotImplementedException();
        }

        public bool Delete(Guid id)
        {
            throw new NotImplementedException();
        }

        public List<ProductForView> GetAll()
        {
            throw new NotImplementedException();
        }

        public ProductForView GetById(Guid id)
        {
            throw new NotImplementedException();
        }

        public Guid Update(ProductUpdateDto model)
        {
            throw new NotImplementedException();
        }
    }
}
