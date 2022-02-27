using System.Collections.Generic;

namespace CoreLoyalty.F5Seconds.Application.DTOs.GotIt
{
    public class GotItVoucherList
    {
        public List<GotItVoucherItem> productList { get; set; }
        public PaginationGotItVoucherList pagination { get; set; }
    }
    public class GotItVoucherItem : ItemVoucherBase
    {
        public string categoryNm { get; set; }
        public List<GotItVoucherItemSize> size { get; set; }
    }
    public class PaginationGotItVoucherList
    {
        public int pageSize { get; set; }
        public int page { get; set; }
        public int totalPage { get; set; }
    }

    public class PayloadGotItVoucherList
    {
        public PayloadGotItVoucherList()
        {
            minPrice = 1;
            maxPrice = 9000000000;
            orderBy = "asc";
            pagination = new PayloadPaginationGotItVoucherList()
            {
                page = 1,
                pageSize = 10000,
                pageTotal = 20
            };
        }
        public int minPrice { get; set; }
        public decimal maxPrice { get; set; }
        public string orderBy { get; set; }
        public PayloadPaginationGotItVoucherList pagination { get; set; }
    }

    public class PayloadPaginationGotItVoucherList
    {
        public int pageSize { get; set; }
        public int page { get; set; }
        public int pageTotal { get; set; }
    }

    public class GotItVoucherItemSize
    {
        public int priceId { get; set; }
        public string priceNm { get; set; }
        public int priceValue { get; set; }
    }
}
