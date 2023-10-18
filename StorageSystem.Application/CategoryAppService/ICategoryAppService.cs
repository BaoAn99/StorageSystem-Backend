using StorageSystem.Application.CategoryAppService.Dtos;
using StorageSystem.Application.ProductAppService.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StorageSystem.Application.CategoryAppService
{
    public interface ICategoryAppService
    {
        Task<List<GetCategoryForView>> GetAll();
        Task<GetCategoryForEditOutput> GetCategoryForEdit(int id);
        Task CreateCategory(CreateOrEditCategoryDto input);
        Task EditCategory(int id, CreateOrEditCategoryDto input);
        Task DeleteCategory(int id);
    }
}
