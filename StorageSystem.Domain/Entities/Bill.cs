using StorageSystem.Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StorageSystem.Domain.Entities
{
    public class Bill
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        public DateTimeOffset DateCreated { get; set; } = DateTimeOffset.Now;

        public bool IsDeleted { get; set; } = false;

        [Column(TypeName = "decimal(18,4)")]
        public decimal Total { get; set; }

        [ForeignKey("CustomerId")]
        public Guid? CustomerId { set; get; }
        public virtual Customer? Customer { get; set; }

        public Guid OwnerId { set; get; }

        public decimal? Deposit { get; set; }

        [Column(TypeName = "decimal(18,4)")]
        public decimal Price { get; set; }

        public int Quantity { get; set; }

        public decimal? DiscountAmount { get; set; }

        //[ForeignKey("ProductId")]
        public Guid BillId { set; get; }
        public Guid ProductId { set; get; }
        //public virtual Product? Product { get; set; }

        public virtual ICollection<Product> Products { get; set;}
    }
}
