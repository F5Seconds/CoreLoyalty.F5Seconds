using System;
using System.Collections.Generic;
using System.Text;

namespace CoreLoyalty.F5Seconds.Application.DTOs.GotIt
{
    public class GotItBuyVoucherRes
    {
        public string refId { get; set; }
        public string invoiceNo { get; set; }
        public List<VoucherInfoRes> vouchers { get; set; }
        public class VoucherInfoRes
        {
            public string voucherCode { get; set; }
            public string voucherLink { get; set; }
            public string voucherLinkCode { get; set; }
            public string voucherImageLink { get; set; }
            public string voucherRefId { get; set; }
            public string expiryDate { get; set; }
            public VoucherProductInfoRes product { get; set; }
            public class VoucherProductInfoRes
            {
                public int productId { get; set; }
                public string productNm { get; set; }
                public string productImg { get; set; }
                public int brandId { get; set; }
                public string brandNm { get; set; }
                public string brandServiceGuide { get; set; }
                public string link { get; set; }
                public string productDesc { get; set; }
                public string terms { get; set; }
                public string productType { get; set; }
                public VoucherSizeInfoRes size { get; set; }
                public class VoucherSizeInfoRes
                {
                    public int sizeId { get; set; }
                    public int priceNm { get; set; }
                    public float pricePrice { get; set; }
                }
            }
        }
    }

    public class GotItBuyVoucherReq
    {
        public int productId { get; set; }
        public int productPriceId { get; set; }
        public int quantity { get; set; }
        public string campaignNm { get; set; } = "F5Seconds Campaign";
        public string expiryDate { get; set; } = DateTime.Now.AddMonths(3).ToString("yyyy-MM-dd");
        public string phone { get; set; }
        public string voucherRefId { get; set; }
    }

    public class GotItErrorMessage
    {
        public string code { get; set; }
        public string msg { get; set; }
    }
}
