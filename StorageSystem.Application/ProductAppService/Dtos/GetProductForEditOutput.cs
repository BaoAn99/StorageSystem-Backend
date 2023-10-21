using StorageSystem.Application.ProductImageAppService.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StorageSystem.Application.ProductAppService.Dtos
{
    public class GetProductForEditOutput
    {
        public int CategoryId { get; set; }
        public string Name { set; get; }
        public decimal Price { set; get; }
        public decimal OriginalPrice { set; get; }
        public int Stock { set; get; }
        public string Description { set; get; }

        public List<GetProducImagetByProduct> ProductImages { get; set;}
    }
}
