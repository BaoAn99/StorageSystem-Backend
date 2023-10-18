using AutoMapper;
using StorageSystem.Application.CategoryAppService.Dtos;
using StorageSystem.DataAccess.IRepository;
using StorageSystem.DataAccess.ProductRepository;
using StorageSystem.Models.Catalog.Categories;
using StorageSystem.Models.Catalog.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StorageSystem.Application.CategoryAppService
{
    public class CategoryAppService : ICategoryAppService
    {
        private readonly Irepository<Category> _categoryRepository;
        private readonly IMapper _mapper;

        public CategoryAppService (Irepository<Category> categoryRepository, IMapper mapper)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }

        public Task<List<GetCategoryForView>> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<GetCategoryForEditOutput> GetCategoryForEdit(int id)
        {
            throw new NotImplementedException();
        }
        public Task DeleteCategory(int id)
        {
            throw new NotImplementedException();
        }
        public async Task CreateCategory(CreateOrEditCategoryDto input)
        {
            var category = _mapper.Map<Category>(input);
            await _categoryRepository.Create(category);
        }

        public Task EditCategory(int id, CreateOrEditCategoryDto input)
        {
            throw new NotImplementedException();
        }
    }
}
