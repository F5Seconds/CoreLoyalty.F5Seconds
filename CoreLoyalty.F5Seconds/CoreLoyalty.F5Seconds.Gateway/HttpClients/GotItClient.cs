using CoreLoyalty.F5Seconds.Gateway.Models.GotIt;
using Newtonsoft.Json;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace CoreLoyalty.F5Seconds.Gateway.HttpClients
{
    public class GotItClient : IGotItClient
    {
        private readonly HttpClient _client;
        public GotItClient(HttpClient client)
        {
            _client = client;
        }

        public async Task<GotItVoucherDetail> VoucherDetailAsync(int id)
        {
            var content = new StringContent(JsonConvert.SerializeObject(new PayloadGotItVoucherDetail() { productId = id }), Encoding.UTF8, "application/json");
            var response = await _client.PostAsync("/api/product/detail", content);
            if (response.IsSuccessStatusCode)
            {
                var jsonString = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<GotItVoucherDetail>(jsonString);
            }
            return null;
        }

        public async Task<GotItVoucherList> VoucherListAsync()
        {
            var content = new StringContent(JsonConvert.SerializeObject(new PayloadGotItVoucherList()), Encoding.UTF8, "application/json");
            var response = await _client.PostAsync("/api/product/list", content);
            if (response.IsSuccessStatusCode)
            {
                var jsonString = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<GotItVoucherList>(jsonString);
            }
            return null;
        }
    }

    public interface IGotItClient
    {
        Task<GotItVoucherList> VoucherListAsync();
        Task<GotItVoucherDetail> VoucherDetailAsync(int id);

    }
}
