using System.Text.Json.Serialization;

namespace StorageSystem.Order.Domain.Enums
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum InvoiceStatus
    {
        Paid,
        Unpaid,
        Cancelled,
        Debited,
        Refunded
    }
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum InvoiceLineStatus
    {
        Paid,
        Unpaid,
        Cancelled,
        Debited,
        Refunded
    }
}
