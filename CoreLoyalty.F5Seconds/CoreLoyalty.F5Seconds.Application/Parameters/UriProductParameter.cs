namespace CoreLoyalty.F5Seconds.Application.Parameters
{
    public class UriProductParameter
    {
        public static string List { get; set; } = "/api/v1/product/list";
        public static string Detail { get; set; } = "/api/v1/product/detail";
        public static string Transaction { get; set; } = "/api/v1/product/transaction";
    }
}
