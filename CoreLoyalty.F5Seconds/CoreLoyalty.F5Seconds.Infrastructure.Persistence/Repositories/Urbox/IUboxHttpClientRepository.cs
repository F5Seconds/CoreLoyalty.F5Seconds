using CoreLoyalty.F5Seconds.Application.DTOs.Urox;
using CoreLoyalty.F5Seconds.Application.Interfaces.Urbox;
using Newtonsoft.Json;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace CoreLoyalty.F5Seconds.Infrastructure.Persistence.Repositories.Urbox
{
    public class IUboxHttpClientRepository : IUrboxHttpClientService
    {
        private readonly HttpClient _client;
        public IUboxHttpClientRepository(HttpClient client)
        {
            _client = client;  
        }

        public async Task<UrboxBuyVocherRes> BuyVoucherAsync(UrboxBuyVoucher voucher)
        {
            var content = new StringContent(JsonConvert.SerializeObject(voucher), Encoding.UTF8, "application/json");
            var response = await _client.PostAsync("/api/pay", content);
            if (response.IsSuccessStatusCode)
            {
                var jsonString = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<UrboxBuyVocherRes>(jsonString);
            }
            return null;
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
}
