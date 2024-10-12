using StorageSystem.Domain.Commons;
using StorageSystem.Domain.Entities.Cashiers;
using StorageSystem.Domain.Entities.Customers;
using StorageSystem.Domain.Entities.Products;
using StorageSystem.Domain.Enums;

namespace StorageSystem.Domain.Entities.Invoices
{
    public class Invoice : EntityAuditBase
    {
        public string CustomerName { get; set; }
        // public string CustomerEmail { get; set; }
        public string CustomerPhone { get; set; }
        public string CustomerAddress { get; set; }
        public DateTimeOffset Time { get; set; }
        public double Amount { get; set; }
        public double NetAmount { get; set; }
        public double? DiscountAmount { get; set; }
        public double? DiscountPercent { get; set; }
        public double? Deposit { get; set; }
        public bool IsPaid { get; set; }
        public InvoiceStatus Status { get; set; }

        #region Relationship
        public Guid? CustomerId { set; get; }
        public virtual Customer Customer { get; set; }
        public Guid CashierId { get; set; }
        public virtual Cashier Cashier { get; set; }
        public virtual ICollection<InvoiceLine> Lines { get; set; }

        #endregion
    }

    public class InvoiceLine : EntityAuditBase
    {
        public double Price { get; set; }
        public double NetPrice { get; set; }
        public double UnitPrice { get; set; }
        public int Quantity { get; set; }
        public double? DiscountAmount { get; set; }
        public double? DiscountPercent { get; set; }
        public string Description { get; set; }
        public InvoiceLineStatus Status { get; set; }

        #region Relationship
        public Guid InvoiceId { set; get; }
        public virtual Invoice Invoice { get; set; }
        public Guid ProductId { set; get; }
        public virtual Product Product { get; set; }
        #endregion
    }
}
