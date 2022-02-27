using CoreLoyalty.F5Seconds.Application.Common;
using CoreLoyalty.F5Seconds.Application.DTOs.GotIt;
using CoreLoyalty.F5Seconds.Application.Interfaces.GotIt;
using CoreLoyalty.F5Seconds.Application.Wrappers;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace CoreLoyalty.F5Seconds.Infrastructure.Persistence.Repositories.GotIt
{
    public class GotItHttpClientRepository: IGotItHttpClientService
    {
        private readonly HttpClient _client;
        private readonly ILogger<GotItHttpClientRepository> _logger;
        public GotItHttpClientRepository(HttpClient client, ILogger<GotItHttpClientRepository> logger)
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

        public async Task<Response<GotItVoucherDetail>> VoucherDetailAsync(int id)
        {
            var content = new StringContent(JsonConvert.SerializeObject(new PayloadGotItVoucherDetail() { productId = id }), Encoding.UTF8, "application/json");
            var response = await _client.PostAsync("/api/product/detail", content);
            if (response.IsSuccessStatusCode)
            {
                var jsonString = await response.Content.ReadAsStringAsync();
                if (Helpers.TryParseJsonConvert(jsonString, out GotItErrorMessage error))
                {
                    return new Response<GotItVoucherDetail>(false, null, error.code, new List<string> { error.msg });
                }
                return new Response<GotItVoucherDetail>(true, JsonConvert.DeserializeObject<GotItVoucherDetail>(jsonString));
            }
            return new Response<GotItVoucherDetail>(false, null, "Server Error");
        }

        public async Task<Response<GotItVoucherList>> VoucherListAsync()
        {
            var content = new StringContent(JsonConvert.SerializeObject(new PayloadGotItVoucherList()), Encoding.UTF8, "application/json");
            var response = await _client.PostAsync("/api/product/list", content);
            if (response.IsSuccessStatusCode)
            {
                var jsonString = await response.Content.ReadAsStringAsync();
                if (Helpers.TryParseJsonConvert(jsonString, out GotItErrorMessage error))
                {
                    return new Response<GotItVoucherList>(false, null, error.code, new List<string> { error.msg });
                }
                return new Response<GotItVoucherList>(true, JsonConvert.DeserializeObject<GotItVoucherList>(jsonString));
            }
            return new Response<GotItVoucherList>(false,null,"Server Error");
        }
    }
}
