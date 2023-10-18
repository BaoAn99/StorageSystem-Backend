using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StorageSystem.DataAccess.IRepository
{
    public interface Irepository<T>
    {
        public Task Create(T _object);

        public Task<int> CreateAndReturnId(T _object);

        public Task Update(T _object);

        public Task<IEnumerable<T>> GetAll();

        public Task<T> GetById(int Id);

        public Task Delete(T _object);
    }
}
