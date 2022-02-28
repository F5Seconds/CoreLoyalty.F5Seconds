using CoreLoyalty.F5Seconds.Application.DTOs.F5seconds;
using CoreLoyalty.F5Seconds.Application.DTOs.Urox;
using CoreLoyalty.F5Seconds.Application.Interfaces.Urbox;
using CoreLoyalty.F5Seconds.Application.Parameters;
using CoreLoyalty.F5Seconds.Application.Wrappers;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace CoreLoyalty.F5Seconds.Infrastructure.Persistence.Repositories.Urbox
{
    public class UboxHttpClientExternalRepository : IUrboxHttpClientExternalService
    {
        private readonly HttpClient _client;
        public UboxHttpClientExternalRepository(HttpClient client)
        {
            _client = client;  
        }

        public async Task<Response<List<F5sVoucherCode>>> BuyVoucherAsync(UrboxBuyVoucherReq voucher)
        {
            var content = new StringContent(JsonConvert.SerializeObject(voucher), Encoding.UTF8, "application/json");
            var response = await _client.PostAsync(UriProductParameter.Transaction, content);
            if (response.IsSuccessStatusCode)
            {
                var jsonString = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<Response<List<F5sVoucherCode>>>(jsonString);
            }
            return new Response<List<F5sVoucherCode>>(false, null, "Server Error");
        }

        public async Task<Response<F5sVoucherDetail>> VoucherDetailAsync(int id)
        {
            var response = await _client.GetAsync($"{UriProductParameter.Detail}/{id}");
            if (response.IsSuccessStatusCode)
            {
                var jsonString = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<Response<F5sVoucherDetail>>(jsonString);
            }
            return new Response<F5sVoucherDetail>(false, null, "Server Error");
        }

        public async Task<Response<List<F5sVoucherBase>>> VoucherListAsync()
        {
            var response = await _client.GetAsync(UriProductParameter.List);
            if (response.IsSuccessStatusCode)
            {
                var jsonString = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<Response<List<F5sVoucherBase>>>(jsonString);
            }
            return new Response<List<F5sVoucherBase>>(false, null, "Server Error");
        }
    }
}
