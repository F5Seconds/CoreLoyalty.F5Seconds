using CoreLoyalty.F5Seconds.Application.Common;
using CoreLoyalty.F5Seconds.Application.DTOs.F5seconds;
using CoreLoyalty.F5Seconds.Application.DTOs.GotIt;
using CoreLoyalty.F5Seconds.Application.Interfaces.GotIt;
using CoreLoyalty.F5Seconds.Application.Parameters;
using CoreLoyalty.F5Seconds.Application.Wrappers;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace CoreLoyalty.F5Seconds.Infrastructure.Persistence.Repositories.GotIt
{
    public class GotItHttpClientExternalRepository: IGotItHttpClientExternalService
    {
        private readonly HttpClient _client;
        private readonly ILogger<GotItHttpClientExternalRepository> _logger;
        public GotItHttpClientExternalRepository(HttpClient client, ILogger<GotItHttpClientExternalRepository> logger)
        {
            _client = client;
            _logger = logger;
        }

        public async Task<Response<List<GotItBuyVoucherRes>>> BuyVoucherAsync(GotItBuyVoucherReq voucher)
        {
            var content = new StringContent(JsonConvert.SerializeObject(voucher), Encoding.UTF8, "application/json");
            var response = await _client.PostAsync("/api/transaction", content);
            if (response.IsSuccessStatusCode)
            {
                var jsonString = await response.Content.ReadAsStringAsync();
                if (Helpers.TryParseJsonConvert(jsonString,out GotItErrorMessage error))
                {
                    return new Response<List<GotItBuyVoucherRes>>(false,null, error.code,new List<string> { error.msg });
                }
                return new Response<List<GotItBuyVoucherRes>>(true,JsonConvert.DeserializeObject<List<GotItBuyVoucherRes>>(jsonString));
            }
            return new Response<List<GotItBuyVoucherRes>>(false,null,"Server Error");
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
            return new Response<List<F5sVoucherBase>>(false,null,"Server Error");
        }
    }
}
