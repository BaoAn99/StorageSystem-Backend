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

        public IEnumerable<Product> GetAll()
        {
            try
            {
                return _dbContext.Products.Where(x => x.IsDeleted == false).ToList();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<Product> Create(Product _object)
        {
            var obj = await _dbContext.Products.AddAsync(_object);
            _dbContext.SaveChanges();
            return obj.Entity;
        }

        public void Update(Product _object)
        {
            _dbContext.Products.Update(_object);
            _dbContext.SaveChanges();
        }

        public void Delete(Product _object)
        {
            _dbContext.Remove(_object);
            _dbContext.SaveChanges();
        }

        public Product GetById(int Id)
        {
            return _dbContext.Products.Where(x => x.IsDeleted == false && x.Id == Id).FirstOrDefault();
        }

    }
}
