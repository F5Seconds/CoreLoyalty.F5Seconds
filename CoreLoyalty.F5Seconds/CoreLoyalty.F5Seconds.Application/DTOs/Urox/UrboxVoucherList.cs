using System.Collections.Generic;

namespace CoreLoyalty.F5Seconds.Application.DTOs.Urox
{
    public class UrboxVoucherList: UrboxMessageBas
    {
        public DataVoucher data { get; set; }
    }

    public class DataVoucher
    {
        public int totalPage { get; set; }
        public int totalResult { get; set; }
        public List<ItemVoucher> items { get; set; }
    }
    public class ItemVoucher : ItemVoucherBase
    {
        public string cat_title { get; set; }
        public int stock { get; set; }
        public string brandLogoLoyalty { get; set; }
        public string brand_name { get; set; }
        public int code_quantity { get; set; }
    }
}
