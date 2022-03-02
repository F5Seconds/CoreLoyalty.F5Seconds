using CoreLoyalty.F5Seconds.Domain.Common;
using System;

namespace CoreLoyalty.F5Seconds.Domain.Entities
{
    public class UrboxTransactionResponse : AuditableBaseEntity
    {
        public string TransactionId { get; set; }
        public string PropductId { get; set; }
        public float ProductPrice { get; set; }
        public string CustomerPhone { get; set; }
        public string VoucherCode { get; set; }
        public string Pin { get; set; }
        public DateTime? ExpiryDate { get; set; }
        public int Status { get; set; }
        public string DeliveryNote { get; set; }
        public string Payload { get; set; }
        public string CodeImage { get; set; }
        public string Link { get; set; }
        public string CodeDisplay { get; set; }
        public int? CodeDisplayType { get; set; }
        public string Token { get; set; }
        public string EstimateDelivery { get; set; }
        /// <summary> Type product
        /// 1: Voucher tiền mặt
        /// 2: Giftset
        /// 3: Combo
        /// 4: Thẻ balance
        /// 5: Thẻ điện thoại
        /// 6: Topup điện thoại
        /// 7: Thẻ điểm
        /// 8: Topup điểm
        /// 9: Vật lý
        /// 10: Item(Sản phẩm cụ thể)
        /// 11: Voucher khuyến mãi
        /// 12: Bảo hiểm
        /// 14: Lượt quay số
        /// 15: Premium Service
        /// </summary>
        public int Type { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public int? CityId { get; set; }
        public int? DistrictId { get; set; }
        public int? WardId { get; set; }
        public string Address { get; set; }
    }
}
