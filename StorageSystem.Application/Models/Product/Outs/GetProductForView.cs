using StorageSystem.Application.Models.Product.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StorageSystem.Application.Models.Product.Outs
{
    public class GetProductForView
    {
        public Guid Id { set; get; }
        public string Name { set; get; }
        public decimal Price { set; get; }
        public int Quantity { set; get; }
        public decimal OriginalPrice { set; get; }
        public int Stock { set; get; }
        public DateTimeOffset DateCreated { set; get; }
        public string? Description { set; get; }
        public string ThumbnailImage { get; set; }
        public Guid CategoryId { set; get; }
        //public List<string> Categories { get; set; }
        //public ProductDto Product { get; set; }
    }
}
