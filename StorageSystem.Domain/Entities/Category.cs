using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StorageSystem.Domain.Entities
{
    public class Category
    {
        public Guid Id { get; set; }

        public string Name { get; set; } = null!;

        public bool IsDeleted { get; set; }

        public DateTimeOffset DateCreated { set; get; }

        public virtual List<Product> Products { get; set; } = null!;

    }
}
