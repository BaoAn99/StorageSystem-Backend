using StorageSystem.Application.Contracts.DataAccess;
using StorageSystem.DataAccess.UOW.Base;
using StorageSystem.Domain.Entities;
using StorageSystem.Persistence.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StorageSystem.DataAccess.UOW
{
    public class CategoryDataAccess : GenericDataAccess<Category>, ICategoryDataAccess
    {
        public CategoryDataAccess(IApplicationDbContext context) : base(context)
        {
        }
    }
}
