using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StorageSystem.Domain.Entities;

public class Product
{
    [Key]
    public Guid Id { set; get; } = Guid.NewGuid();

    [Required]
    public string Name { set; get; }

    [Column(TypeName = "decimal(18,4)")]
    public decimal Price { set; get; }

    [Column(TypeName = "decimal(18,4)")]
    public decimal OriginalPrice { set; get; }

    public string? Description { set; get; }

    public int StockStatus { set; get; }

    //miss field stock

    [ForeignKey("CategoryId")]
    public Guid CategoryId { set; get; }
    public virtual Category? Category { get; set; }

    [ForeignKey("UnitId")]
    public Guid UnitId { set; get; }
    public virtual Unit? Unit { get; set; }

    public bool IsDeleted { get; set; } = false;

    public DateTimeOffset DateCreated { set; get; } = DateTimeOffset.Now;

    public string ThumbnailImage { get; set; }

    public virtual ICollection<Bill>? Bills { get; set; }

    public virtual ICollection<Coupon>? Coupons  { get; set; }

    public virtual ICollection<Order>? Orders { get; set; }

    public virtual ICollection<ProductImage>? ProductImages { get; set; }

    public virtual ICollection<Supplier>? Suppliers { get; set; }

    public SupplierProduct SupplierProduct { get; set; }
}