using Microsoft.EntityFrameworkCore;
using StorageSystem.DataAccess.IRepository;
using StorageSystem.EntityFrameworkCore.EntityFrameworkCore;
using StorageSystem.Models.Catalog.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StorageSystem.DataAccess.ProductRepository
{
    public class ProductRepository : Irepository<Product>
    {
        private readonly ApplicationDbContext _dbContext;
        public ProductRepository(ApplicationDbContext applicationDbContext)
        {
            _dbContext = applicationDbContext;
        }

        public async Task<IEnumerable<Product>> GetAll()
        {
            try
            {
                return await _dbContext.Products.Where(x => x.IsDeleted == false).ToListAsync();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task Create(Product _object)
        {
            _dbContext.Products.Add(_object);
            await _dbContext.SaveChangesAsync();
        }

        public async Task Update(Product _object)
        {
            _dbContext.Products.Update(_object);
            await _dbContext.SaveChangesAsync();
        }

        public async Task Delete(Product _object)
        {
            _dbContext.Remove(_object);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<Product> GetById(int Id)
        {
            return await _dbContext.Products.Where(x => x.IsDeleted == false && x.Id == Id).FirstOrDefaultAsync();
        }

    }
}
