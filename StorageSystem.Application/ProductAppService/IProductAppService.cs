using StorageSystem.Application.ProductAppService.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StorageSystem.Application.ProductAppService
{
    public interface IProductAppService
    {
        Task<List<GetProductForView>> GetAll();
        Task<GetProductForEditOutput> GetDefImportFileForEdit(int id);
        Task CreateProduct(CreateOrUpdateProductDto input);
        Task UpdateProduct(int id, CreateOrUpdateProductDto input);
        Task Delete(int id);
    }
}
