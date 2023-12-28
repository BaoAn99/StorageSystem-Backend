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
        public ProductDto Product { get; set; }
    }
}
