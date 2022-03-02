using CoreLoyalty.F5Seconds.Domain.Common;

namespace CoreLoyalty.F5Seconds.Domain.Entities
{
    public class UrboxTransactionResponse : AuditableBaseEntity
    {
        public string TransactionId { get; set; }
        public string PropductId { get; set; }
        public float ProductPrice { get; set; }
        public string CustomerPhone { get; set; }
        public string VoucherCode { get; set; }
        public string ExpiryDate { get; set; }
        public int Status { get; set; }
        public string Description { get; set; }
        public string Payload { get; set; }
    }
}
