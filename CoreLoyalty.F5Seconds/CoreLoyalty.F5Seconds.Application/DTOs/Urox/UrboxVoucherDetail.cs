using System.Collections.Generic;

namespace CoreLoyalty.F5Seconds.Application.DTOs.Urox
{
    public class UrboxVoucherDetailData : UrboxMessageBas
    {
        public UrboxVoucherDetail data { get; set; }
    }
    public class UrboxVoucherDetail: ItemVoucherBase
    {
        public int justGetOrder { get; set; }
        //public List<string> images_rectangle { get; set; }
        public string content { get; set; }
        public string note { get; set; }
        public List<UrboxVoucherOffice> office { get; set; }
    }

    public class UrboxVoucherOffice
    {
        public string address { get; set; }
        public string address_en { get; set; }
        public string title_city { get; set; }
        public int? city_id { get; set; }
        public float? latitude { get; set; }
        public float? longitude { get; set; }
        public int? brand_id { get; set; }
        public int? district_id { get; set; }
        public int? ward_id { get; set; }
        public string code { get; set; }
        public string number { get; set; }
        public string phone { get; set; }
        public int? isApply { get; set; }
        public int? id { get; set; }
    }
}
