using Newtonsoft.Json;
using System.Collections.Generic;

namespace CoreLoyalty.F5Seconds.Application.DTOs.GotIt
{
    public class GotItVoucherDetail : ItemVoucherBase
    {
        //public List<string> productSubImg { get; set; }
        public string brandNameSlug { get; set; }
        public string brandPhone { get; set; }
        public string brandAddress { get; set; }
        public string brandDesc { get; set; }
        public string serviceGuide { get; set; }
        public string nameSlug { get; set; }
        public string productDesc { get; set; }
        public string productShortDesc { get; set; }
        public string terms { get; set; }
        public int totalStore { get; set; }
        public int totalPage { get; set; }
        public List<GotItVoucherDetailStore> storeList { get; set; }
    }

    public class GotItVoucherDetailStore
    {
        public int storeId { get; set; }
        public string storeNm { get; set; }
        public string storeAddr { get; set; }
        
        [JsonProperty("long")]
        public float storeLong { get; set; }
        
        [JsonProperty("lat")]
        public float storeLat { get; set; }
        public int city_id { get; set; }
        public string city { get; set; }
        public int dist_id { get; set; }
        public string district { get; set; }
}

    public class PayloadGotItVoucherDetail
    {
        public int productId { get; set; }
    }
}
