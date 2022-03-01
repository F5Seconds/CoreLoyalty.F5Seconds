using CoreLoyalty.F5Seconds.Domain.Common;

namespace CoreLoyalty.F5Seconds.Domain.Entities
{
    public class TransactionRequest : AuditableBaseEntity
    {
        public string PropductId { get; set; }
        public int Quantity { get; set; }
        public string TransactionId { get; set; }
        public string CustomerId { get; set; }
        public string CustomerPhone { get; set; }
    }
}
