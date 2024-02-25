using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StorageSystem.Domain.Entities
{
    public class SupplierProduct
    {
        [Key]
        public Guid Id { get; set;}

        [ForeignKey("SupplierId")]
        public Guid SupplierId { get; set;}
        public Supplier Supplier { get; set;}

        [ForeignKey("ProductId")]
        public Guid ProductId { get; set; }
        public Product Product { get; set; }

        [Column(TypeName = "decimal(18,4)")]
        public decimal Price { set; get; }
    }
}
