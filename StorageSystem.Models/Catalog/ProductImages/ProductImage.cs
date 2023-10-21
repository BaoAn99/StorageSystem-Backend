using Castle.Components.DictionaryAdapter;
using StorageSystem.Models.Catalog.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StorageSystem.Models.Catalog.ProductImages
{
    public class ProductImage
    {
        public int Id { get; set; }

        public string ImagePath { get; set; } = null!;

        public string? Caption { get; set; }

        public long FileSize { get; set; }

        public int ProductId { get; set; }

        public bool IsDeleted { get; set; }

        public DateTime DateCreated { get; set; }

        public bool IsImageFeature { get; set; }

        public virtual Product Product { get; set; } =  null!;
    }
}
