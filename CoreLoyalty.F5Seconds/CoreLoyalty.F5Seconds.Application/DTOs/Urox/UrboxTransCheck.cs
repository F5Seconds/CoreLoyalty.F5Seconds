using System.Collections.Generic;

namespace CoreLoyalty.F5Seconds.Application.DTOs.Urox
{
    public class UrboxTransCheckReq
    {
        public string transaction_id { get; set; }
    }

    public class UrboxTransCheckRes : UrboxMessageBase
    {
        public UrboxTransCheckResData data { get; set; }
        public class UrboxTransCheckResData
        {
            public List<UrboxTransCheckResDataDetail> detail { get; set; }
            public class UrboxTransCheckResDataDetail
            {
                public string channel { get; set; }
                public string transactionId { get; set; }
                public string productCode { get; set; }
                public string using_time { get; set; }
                public string link { get; set; }
                public string delivery { get; set; }
                public int? deliveryCode { get; set; }
                public string estimateDelivery { get; set; }
                public string delivery_note { get; set; }
                public string code { get; set; }
                public string expired { get; set; }
            }
        }
    }
}
