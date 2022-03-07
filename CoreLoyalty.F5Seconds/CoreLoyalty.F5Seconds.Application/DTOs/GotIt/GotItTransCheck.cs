namespace CoreLoyalty.F5Seconds.Application.DTOs.GotIt
{
    public class GotItTransCheckReq
    {
        public string voucherRefId { get; set; }
    }

    public class GotItTransCheckRes
    {
        public string refId { get; set; }
        public string invoiceNo { get; set; }
        public GotItTransCheckResVoucher voucher { get; set; }
        public class GotItTransCheckResVoucher
        {
            public string channel { get; set; }
            public string transactionId { get; set; }
            public string productCode { get; set; }
            public string voucherCode { get; set; }
            public string expiryDate { get; set; }
            public int stateCode { get; set; }
            public string stateText { get; set; }
            public string used_time { get; set; }
            public string used_brand { get; set; }
        }
    }
}
