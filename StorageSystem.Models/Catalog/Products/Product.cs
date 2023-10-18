using StorageSystem.Models.Catalog.Categories;
using StorageSystem.Models.Catalog.ProductImages;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StorageSystem.Models.Catalog.Products
{
    public class Product
    {
        public int Id { set; get; }

        public string Name { set; get; } = null!;

        public int Price { set; get; }
        public int Quantity { set; get; }

        public int OriginalPrice { set; get; }

        public int Stock { set; get; }

        public string? Description { set; get; }

        public int CategoryId { set; get; }

        public bool IsDeleted { get; set; }

        public DateTime DateCreated { set; get; }

        public virtual Category Category { get; set; } = null!;

        public virtual List<ProductImage> ProductImages { get; set; }
    }
}
