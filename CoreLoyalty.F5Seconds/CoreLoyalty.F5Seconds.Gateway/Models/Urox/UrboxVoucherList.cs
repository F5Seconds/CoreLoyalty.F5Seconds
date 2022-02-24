using System.Collections.Generic;

namespace CoreLoyalty.F5Seconds.Gateway.Models.Urox
{
    public class UrboxVoucherList
    {
    }

    public class DataVoucher
    {
        public int done { get; set; }
        public string msg { get; set; }
        public string microtime { get; set; }
        public int status { get; set; }
        public List<ItemVoucher> data { get; set; }
    }
    public class ItemVoucher
    {
        public int id { get; set; }
        public int branch { get; set; }
    }
}
