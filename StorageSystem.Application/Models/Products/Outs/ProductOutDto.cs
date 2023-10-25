using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StorageSystem.Application.Models.Products.Out
{
    public class ProductOutDto
    {
        public int Id { set; get; }

        public string Name { set; get; }
        
        public decimal Price { set; get; }
        
        public int Quantity { set; get; }
        
        public decimal OriginalPrice { set; get; }
        
        public int Stock { set; get; }
        
        public DateTimeOffset DateCreated { set; get; }
        
        public string Description { set; get; }
        
        public string ThumbnailImage { get; set; }
        
        public List<string> Categories { get; set; }
    }
}
