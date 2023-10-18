using Castle.Components.DictionaryAdapter;
using StorageSystem.Models.Catalog.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StorageSystem.Models.Catalog.Categories
{
    public class Category
    {
        public int Id { get; set; }

        public string Name { get; set; } = null!;

        public bool IsDeleted { get; set; }

        public DateTime DateCreated { set; get; }

        public virtual List<Product> Products { get; set; } = null!;

    }
}
