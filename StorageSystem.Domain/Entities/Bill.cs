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
        public Guid CustomerId { set; get; }
        public virtual Customer? Customer { get; set; }

        public Guid OwnerId { set; get; }

        public virtual List<BillDetail>? BillDetails { get; set; }
    }
}
