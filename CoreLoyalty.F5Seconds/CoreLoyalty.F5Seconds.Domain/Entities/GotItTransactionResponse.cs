using CoreLoyalty.F5Seconds.Domain.Common;
using System;

namespace CoreLoyalty.F5Seconds.Domain.Entities
{
    public class GotItTransactionResponse : AuditableBaseEntity
    {
        public string TransactionId { get; set; }
        public string PropductId { get; set; }
        public float ProductPrice { get; set; }
        public string CustomerPhone { get; set; }
        public string VoucherCode { get; set; }
        public DateTime? ExpiryDate { get; set; }
        /// <summary> Status
        /// VOUCHER_WAS_PURCHASED, VOUCHER_WAS_CREATED -> New (0,1)
        /// VOUCHER_WAS_SENT, VOUCHER_WAS_DOWNLOADED --> Sent(2,3)
        /// VOUCHER_WAS_RECEIVED --> Opened(3)
        /// VOUCHER_WAS_USED --> Used(4)
        /// VOUCHER_EXPIRED --> Expired(8)
        /// VOUCHER_WAS_CANCEL --> Canceled(9)
        /// </summary>
        public int Status { get; set; } = 0;
        public string Description { get; set; }
        public string Payload { get; set; }
        public string VoucherLink { get; set; }
        public string VoucherLinkCode { get; set; }
        public string VoucherImageLink { get; set; }
        public string Type { get; set; }
        public int? BrandId { get; set; }
        public string BrandName { get; set; }
        public string UsedTime { get; set; }
        public string UsedBrand { get; set; }
        public string StateText { get; set; }
    }
}
