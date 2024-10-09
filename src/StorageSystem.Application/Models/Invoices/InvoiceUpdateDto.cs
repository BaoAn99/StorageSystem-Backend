using StorageSystem.Domain.Enums;

namespace StorageSystem.Application.Models.Invoices
{
    public class InvoiceUpdateDto
    {
        public string? CustomerName { get; set; }
        public string? CustomerPhone { get; set; }
        public string? CustomerAddress { get; set; }
        public DateTimeOffset Time { get; set; }
        public double Amount { get; set; }
        public double NetAmount { get; set; }
        public double? DiscountAmount { get; set; }
        public double? DiscountPercent { get; set; }
        public double? Deposit { get; set; }
        //public bool IsPaid { get; set; }
        public InvoiceStatus Status { get; set; }

        #region Relationship
        public Guid? CustomerId { set; get; }
        public Guid CashierId { get; set; }
        public List<InvoiceLineUpdateDto> Items { get; set; }
        #endregion
    }

    public class InvoiceLineUpdateDto
    {
        //public double Price { get; set; }
        //public double NetPrice { get; set; }
        public double UnitPrice { get; set; }
        public int Quantity { get; set; }
        public double? DiscountAmount { get; set; }
        public double? DiscountPercent { get; set; }
        public string? Description { get; set; }
        public InvoiceLineStatus Status { get; set; }

        #region Relationship
        public Guid ProductId { set; get; }
        #endregion
    }
}
