using Microsoft.EntityFrameworkCore;
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

        public async Task<IEnumerable<Category>> GetAll()
        {
            try
            {
                return await _dbContext.Categories.Where(x => x.IsDeleted == false).ToListAsync();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task Create(Category _object)
        {
            _dbContext.Categories.Add(_object);
            await _dbContext.SaveChangesAsync();
        }

        public async Task Update(Category _object)
        {
            _dbContext.Categories.Update(_object);
            await _dbContext.SaveChangesAsync();
        }

        public async Task Delete(Category _object)
        {
            _dbContext.Remove(_object);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<Category> GetById(int Id)
        {
            return await _dbContext.Categories.Where(x => x.IsDeleted == false && x.Id == Id).FirstOrDefaultAsync();
        }

        public Task<int> CreateAndReturnId(Category _object)
        {
            throw new NotImplementedException();
        }
    }
}
