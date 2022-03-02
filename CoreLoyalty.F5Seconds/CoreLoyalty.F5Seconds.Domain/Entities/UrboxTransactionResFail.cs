using CoreLoyalty.F5Seconds.Domain.Common;

namespace CoreLoyalty.F5Seconds.Domain.Entities
{
    public class UrboxTransactionResFail : AuditableBaseEntity
    {
        public string TransactionId { get; set; }
        public string ProductId { get; set; }
        public string Code { get; set; }
        public string Message { get; set; }
        public string Partner { get; set; }
        public string Payload { get; set; }
    }
}
