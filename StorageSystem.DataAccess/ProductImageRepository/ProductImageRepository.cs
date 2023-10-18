using Microsoft.EntityFrameworkCore;
using StorageSystem.DataAccess.IRepository;
using StorageSystem.EntityFrameworkCore.EntityFrameworkCore;
using StorageSystem.Models.Catalog.ProductImages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StorageSystem.DataAccess.ProductImageRepository
{
    public class ProductImageRepository : Irepository<ProductImage>
    {

        private readonly ApplicationDbContext _dbContext;
        public ProductImageRepository(ApplicationDbContext applicationDbContext)
        {
            _dbContext = applicationDbContext;
        }

        public async Task Create(ProductImage _object)
        {
            await _dbContext.ProductImages.AddAsync(_object);
            await _dbContext.SaveChangesAsync();
        }

        public Task<int> CreateAndReturnId(ProductImage _object)
        {
            throw new NotImplementedException();
        }

        public async Task Delete(ProductImage _object)
        {
            _dbContext.Remove(_object);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<ProductImage>> GetAll()
        {
            try
            {
                return await _dbContext.ProductImages.Where(x => x.IsDeleted == false).ToListAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<ProductImage> GetById(int Id)
        {
            return await _dbContext.ProductImages.Where(x => x.IsDeleted == false && x.Id == Id).FirstOrDefaultAsync();
        }

        public async Task Update(ProductImage _object)
        {
            _dbContext.ProductImages.Update(_object);
            await _dbContext.SaveChangesAsync();
        }
    }
}
