using StorageSystem.DataAccess.IRepository;
using StorageSystem.EntityFrameworkCore.EntityFrameworkCore;
using StorageSystem.Models.Catalog.Categories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StorageSystem.DataAccess.CategoryRepository
{
    public class CategoryRepository : Irepository<Category>
    {
        ApplicationDbContext _dbContext;
        public CategoryRepository(ApplicationDbContext applicationDbContext)
        {
            _dbContext = applicationDbContext;
        }

        public IEnumerable<Category> GetAll()
        {
            try
            {
                return _dbContext.Categories.Where(x => x.IsDeleted == false).ToList();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<Category> Create(Category _object)
        {
            var obj = await _dbContext.Categories.AddAsync(_object);
            _dbContext.SaveChanges();
            return obj.Entity;
        }

        public void Update(Category _object)
        {
            _dbContext.Categories.Update(_object);
            _dbContext.SaveChanges();
        }

        public void Delete(Category _object)
        {
            _dbContext.Remove(_object);
            _dbContext.SaveChanges();
        }

        public Category GetById(int Id)
        {
            return _dbContext.Categories.Where(x => x.IsDeleted == false && x.Id == Id).FirstOrDefault();
        }

    }
}
