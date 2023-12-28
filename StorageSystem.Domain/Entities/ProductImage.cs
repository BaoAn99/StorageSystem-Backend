using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StorageSystem.Domain.Entities
{
    public class ProductImage
    {
        public Guid Id { get; set; }

        public string ImagePath { get; set; } = null!;

        public string? Caption { get; set; }

        public Guid ProductId { get; set; }

        public bool IsDeleted { get; set; }

        public DateTimeOffset DateCreated { get; set; }

        public bool IsImageFeature { get; set; }

        public virtual Product Product { get; set; } = null!;
    }
}
