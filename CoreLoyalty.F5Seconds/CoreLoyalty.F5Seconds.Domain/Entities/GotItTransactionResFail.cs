using CoreLoyalty.F5Seconds.Domain.Common;

namespace CoreLoyalty.F5Seconds.Domain.Entities
{
    public class GotItTransactionResFail : AuditableBaseEntity
    {
        public int GotItTransactionRequestId { get; set; }
        public string TransactionId { get; set; }
        public string ProductCode { get; set; }
        public string Code { get; set; }
        public string Message { get; set; }
        public string Partner { get; set; }
        public string Payload { get; set; }
    }
}
