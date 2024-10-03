using StorageSystem.Application.Models.Product;
using System.Collections.Generic;

namespace StorageSystem.Application.Contracts.Services
{
    public interface IProductService
    {
        Guid Create(ProductCreateDto model);
        Guid Update(ProductUpdateDto model);
        bool Delete(Guid id);
        ProductForView GetById(Guid id);
        List<ProductForView> GetAll();
    }
}
