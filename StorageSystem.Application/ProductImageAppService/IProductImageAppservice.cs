using StorageSystem.Application.ProductAppService.Dtos;
using StorageSystem.Application.ProductImageAppService.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StorageSystem.Application.ProductImageAppService
{
    public interface IProductImageAppservice
    {
        Task<List<GetProductImageForView>> GetAll();
        //Task<GetProductForEditOutput> GetProductForEdit(int id);

        Task<List<GetProducImagetByProduct>> GetImageProductByProductId(int productId);
        Task CreateProductImage(CreateProductImageDto input);
        Task UpdateProductImage(int id, UpdateProductImageDto input);
        Task Delete(int id);
    }
}
