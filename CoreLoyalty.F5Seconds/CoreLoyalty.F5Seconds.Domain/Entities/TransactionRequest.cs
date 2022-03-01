using CoreLoyalty.F5Seconds.Domain.Common;
using System.Collections.Generic;

namespace CoreLoyalty.F5Seconds.Domain.Entities
{
    public class TransactionRequest : AuditableBaseEntity
    {
        public string PropductId { get; set; }
        public int Quantity { get; set; }
        public string TransactionId { get; set; }
        public string CustomerId { get; set; }
        public string CustomerPhone { get; set; }
        public string Partner { get; set; }
        public int Status { get; set; }
    }
}
