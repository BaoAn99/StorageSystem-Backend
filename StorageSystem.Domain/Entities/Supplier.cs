using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StorageSystem.Domain.Entities
{
    public class Supplier
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required]
        public string Name { get; set; }

        [Required]
        public string Address { get; set; }

        [Required]
        public string Phone { get; set; }

        public string Email { get; set; }

        public DateTimeOffset DateCreated { get; set; } = DateTimeOffset.Now;

        public bool IsDeleted { get; set; } = false;

        public SupplierProduct SupplierProduct { get; set; }

        //[Column(TypeName = "decimal(18,4)")]
        //public decimal Price { set; get; }

        //public Guid SuplierId { get; set; } = Guid.NewGuid();

        //public Guid ProductId { get; set; }

        //public ICollection<Guid> ProductIds { get; } = new List<Guid>(); 

        //public virtual ICollection<Product>? Products { get; set; }

        //[ForeignKey("ProductId")]
        //public Guid ProductId { set; get; }
        //public virtual Product Product { get; set; }


    }
}
