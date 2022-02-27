namespace CoreLoyalty.F5Seconds.Application.DTOs.GotIt
{
    public class GotItItemVoucherBase
    {
        public int productId { get; set; }
        public string productNm { get; set; }
        public string productImg { get; set; }
        public string productType { get; set; }
        public int brandId { get; set; }
        public string brandNm { get; set; }
        public string brandLogo { get; set; }
        public string brandServiceGuide { get; set; }
        public int categoryId { get; set; }
        public string link { get; set; }
    }

    public class GotItBuyVoucher
    {
        public int productId { get; set; }
        public int productPriceId { get; set; }
        public int quantity { get; set; }
        public string campaignNm { get; set; }
    }
}
