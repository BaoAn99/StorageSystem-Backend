using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StorageSystem.Domain.Entities;

public class Product
{
    public Guid Id { set; get; } = Guid.NewGuid();

    public string Name { set; get; }

    public decimal Price { set; get; }

    public int Quantity { set; get; }

    public decimal OriginalPrice { set; get; }

    public int Stock { set; get; }

    public string? Description { set; get; }

    public Guid CategoryId { set; get; }

    public bool IsDeleted { get; set; } = false;

    public DateTimeOffset DateCreated { set; get; } = DateTimeOffset.Now;

    public string ThumbnailImage { get; set; } = "image";

    public virtual Category? Category { get; set; } = null!;

    public virtual List<ProductImage>? ProductImages { get; set; }
}
