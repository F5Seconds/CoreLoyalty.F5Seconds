using CoreLoyalty.F5Seconds.Domain.Common;

namespace CoreLoyalty.F5Seconds.Domain.Entities
{
    public class GotItTransactionRequest : AuditableBaseEntity
    {
        public string Channel { get; set; }
        public string ProductCode { get; set; }
        public int Quantity { get; set; }
        public string TransactionId { get; set; }
        public string CustomerId { get; set; }
        public string CustomerPhone { get; set; }
        public string Partner { get; set; }
        public int Status { get; set; }
        public string Payload { get; set; }
    }
}
