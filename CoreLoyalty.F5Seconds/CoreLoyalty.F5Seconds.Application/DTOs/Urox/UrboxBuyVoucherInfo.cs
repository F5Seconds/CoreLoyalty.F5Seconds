using System;
using System.Collections.Generic;
using System.Text;

namespace CoreLoyalty.F5Seconds.Application.DTOs.Urox
{
    public class UrboxBuyVoucherReq
    {
        public string site_user_id { get; set; }
        public string ttphone { get; set; }
        public string transaction_id { get; set; }
        public string productCode { get; set; }
        public float productPrice { get; set; }
        public List<UrboxBuyVoucherItem> dataBuy { get; set; }
        public class UrboxBuyVoucherItem
        {
            public int priceId { get; set; }
            public int quantity { get; set; }
        }
    }

    public class UrboxBuyVocherRes: UrboxMessageBase
    {
        public UrboxBuyVocherResData data { get; set; }
        public class UrboxBuyVocherResData
        {
            public int pay { get; set; }
            public string transaction_id { get; set; }
            public string linkCart { get; set; }
            public string linkCombo { get; set; }
            public string linkShippingInfo { get; set; }
            public UrboxBuyVoucherResCart cart { get; set; }
            public class UrboxBuyVoucherResCart
            {
                public int id { get; set; }
                public int cartNo { get; set; }
                public float money_total { get; set; }
                public List<UrboxBuyVoucherResCode> code_link_gift { get; set; }
                public class UrboxBuyVoucherResCode
                {
                    public int cart_detail_id { get; set; }
                    public string code_display { get; set; }
                    public int code_display_type { get; set; }
                    public string link { get; set; }
                    public string code { get; set; }
                    public int card_id { get; set; }
                    public string pin { get; set; }
                    public string serial { get; set; }
                    public int priceId { get; set; }
                    public string token { get; set; }
                    public string expired { get; set; }
                    public string code_image { get; set; }
                    public string estimateDelivery { get; set; }
                }
            }
        }
    }
}
