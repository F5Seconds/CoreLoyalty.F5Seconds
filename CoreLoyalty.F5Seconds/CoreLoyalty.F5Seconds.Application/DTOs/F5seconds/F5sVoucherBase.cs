﻿using System.Collections.Generic;
using System.ComponentModel;

namespace CoreLoyalty.F5Seconds.Application.DTOs.F5seconds
{
    public class F5sVoucherBase
    {
        public int productId { get; set; }
        public string productNm { get; set; }
        public string productImg { get; set; }
        public float productPrice { get; set; }
        public int productTyp { get; set; } = 1;
        public int productSize { get; set; } = 0;
        public string productPartner { get; set; }
        public string brandNm { get; set; }
        public string brandLogo { get; set; }
    }

    public class F5sVoucherDetail : F5sVoucherBase
    {
        [Description("Mô tả sản phẩm")]
        public string productContent { get; set; }

        [Description("Điều khoản sử dụng")]
        public string productTerm { get; set; }

        [Description("Mảng chứa tất cả thông tin cửa hàng có thể sử dung voucher")]
        public List<F5sVoucherOffice> storeList { get; set; }
    }

    public class F5sVoucherOffice
    {
        public string storeNm { get; set; }
        public string storeAddr { get; set; }
        [Description("Số Lat trên bản đồ Google")]
        public float storeLat { get; set; }
        [Description("Số Long trên bản đồ Google")]
        public float storeLong { get; set; }
    }
}