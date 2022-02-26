using CoreLoyalty.F5Seconds.Application.DTOs.GotIt;
using CoreLoyalty.F5Seconds.Application.Interfaces.GotIt;
using Newtonsoft.Json;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace CoreLoyalty.F5Seconds.Infrastructure.Persistence.Repositories.GotIt
{
    public class GotItHttpClientRepository: IGotItHttpClientService
    {
        private readonly HttpClient _client;
        public GotItHttpClientRepository(HttpClient client)
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
}
