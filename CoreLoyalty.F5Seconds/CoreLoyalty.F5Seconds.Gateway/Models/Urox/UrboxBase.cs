namespace CoreLoyalty.F5Seconds.Gateway.Models.Urox
{
    public class UrboxMessageBas
    {
        public int done { get; set; }
        public string msg { get; set; }
        public string microtime { get; set; }
        public int status { get; set; }
    }
    public class ItemVoucherBase
    {
        public int id { get; set; }
        public int branch { get; set; }
        public int brand_id { get; set; }
        public int cat_id { get; set; }
        public int gift_id { get; set; }
        public string title { get; set; }
        public int type { get; set; }
        public int price { get; set; }
        public int view { get; set; }
        public int quantity { get; set; }
        public int usage_check { get; set; }
        public string image { get; set; }
        // public List<string> images { get; set; }
        public string expire_duration { get; set; }
        public string code_display { get; set; }
        public int code_display_type { get; set; }
        public int brand_online { get; set; }
        public string brandImage { get; set; }
        public string parent_cat_id { get; set; }
    }
}
