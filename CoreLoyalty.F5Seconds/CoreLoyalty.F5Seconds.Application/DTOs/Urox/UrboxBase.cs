using System.ComponentModel;

namespace CoreLoyalty.F5Seconds.Application.DTOs.Urox
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
        [Description("Mã quà tặng. Sử dụng để gọi API tạo đơn quà tặng ở trường priceId")]
        public int id { get; set; }
        
        [Description("Tên thương hiệu")]
        public int branch { get; set; }
        
        [Description("Mã thương hiệu")]
        public int brand_id { get; set; }
        
        [Description("Mã danh mục")]
        public int cat_id { get; set; }
        
        [Description("Mã quà tặng cha")]
        public int gift_id { get; set; }
        
        [Description("Tên quà tặng")]
        public string title { get; set; }
        
        [Description("Loại quà tặng")]
        public int type { get; set; }
        
        [Description("Giá trị quà tặng")]
        public int price { get; set; }
        
        [Description("Số lượt xem")]
        public int view { get; set; }
        
        [Description("Số lượng code có thể mua")]
        public int quantity { get; set; }
        
        [Description("Có/không thể kiểm tra tình trạng sử dụng")]
        public int usage_check { get; set; }
        
        [Description("Đường dẫn hình ảnh quà tặng")]
        public string image { get; set; }
        // public List<string> images { get; set; }
        [Description("Thời hạn sử dụng quà")]
        public string expire_duration { get; set; }
        
        [Description("")]
        public string code_display { get; set; }

        [Description("")]
        public int code_display_type { get; set; }
        
        [Description("Thương hiệu này áp dụng online hay offline")]
        public int brand_online { get; set; }
        
        [Description("Đường dẫn hình ảnh thương hiệu")]
        public string brandImage { get; set; }
        
        [Description("")]
        public string parent_cat_id { get; set; }
    }
}
