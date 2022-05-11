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
        IEnumerable<GetProductForView> GetAll();
        Task<GetProductForEditOutput> GetDefImportFileForEdit(int id);
        Task CreateOrEdit(CreateOrEditProductDto input);
        Task Delete(int id);
    }
}
