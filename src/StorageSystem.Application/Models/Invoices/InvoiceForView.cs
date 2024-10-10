using StorageSystem.Domain.Enums;

namespace StorageSystem.Application.Models.Invoices
{
    public class InvoiceForView
    {
        public Guid Id { get; set; }
        public DateTimeOffset Time { get; set; }
        public double Amount { get; set; }
        public double NetAmount { get; set; }
        public double? DiscountAmount { get; set; }
        public double? DiscountPercent { get; set; }
        public double? Deposit { get; set; }
        //public bool IsPaid { get; set; }
        public InvoiceStatus Status { get; set; }
        public string StatusName { get; set; }
        #region Relationship
        public Guid CashierId { get; set; }
        public string CashierName { get; set; }
        public List<InvoiceLineForView> Items { get; set; }
        #endregion
    }

    public class InvoiceLineForView
    {
        //public double Price { get; set; }
        //public double NetPrice { get; set; }
        public double UnitPrice { get; set; }
        public int Quantity { get; set; }
        public double? DiscountAmount { get; set; }
        public double? DiscountPercent { get; set; }
        public string? Description { get; set; }
        public InvoiceLineStatus Status { get; set; }
        public string StatusName { get; set; }
        #region Relationship
        public Guid ProductId { set; get; }
        public string ProductName { get; set; }
        #endregion
    }
}

