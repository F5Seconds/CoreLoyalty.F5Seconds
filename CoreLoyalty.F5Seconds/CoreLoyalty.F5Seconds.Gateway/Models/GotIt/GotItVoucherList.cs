using System.Collections.Generic;

namespace CoreLoyalty.F5Seconds.Gateway.Models.GotIt
{
    public class GotItVoucherList
    {
        public List<PaginationGotItVoucherItem> productList { get; set; }
        public PaginationGotItVoucherList pagination { get; set; }
    }
    public class PaginationGotItVoucherItem : ItemVoucherBase
    {
        public string categoryNm { get; set; }
        
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
                pageSize = 10,
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
}
