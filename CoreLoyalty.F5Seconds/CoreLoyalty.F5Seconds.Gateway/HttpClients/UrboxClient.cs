using CoreLoyalty.F5Seconds.Gateway.Models.Urox;
using Newtonsoft.Json;
using System.Net.Http;
using System.Threading.Tasks;

namespace CoreLoyalty.F5Seconds.Gateway.HttpClients
{
    public class UrboxClient: IUrboxClient
    {
        private readonly HttpClient _client;
        public UrboxClient(HttpClient client)
        {
            _client = client;
        }

        public async Task<UrboxVoucherDetailData> VoucherDetailAsync(int id)
        {
            var response = await _client.GetAsync($"/api/gift/detail/{id}");
            if (response.IsSuccessStatusCode)
            {
                var jsonString = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<UrboxVoucherDetailData>(jsonString);
            }
            return null;
        }

        public async Task<UrboxVoucherList> VoucherListAsync()
        {
            var response = await _client.GetAsync("/api/gift/lists");
            if (response.IsSuccessStatusCode)
            {
                var jsonString = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<UrboxVoucherList>(jsonString);
            }
            return null;
        }
    }

    public interface IUrboxClient
    {
        Task<UrboxVoucherList> VoucherListAsync();
        Task<UrboxVoucherDetailData> VoucherDetailAsync(int id);
    }
}
