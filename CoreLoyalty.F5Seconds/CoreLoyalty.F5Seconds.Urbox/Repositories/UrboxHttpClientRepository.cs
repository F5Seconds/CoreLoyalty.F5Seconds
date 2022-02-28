using AutoMapper;
using CoreLoyalty.F5Seconds.Application.DTOs.F5seconds;
using CoreLoyalty.F5Seconds.Application.DTOs.Urox;
using CoreLoyalty.F5Seconds.Application.Wrappers;
using CoreLoyalty.F5Seconds.Urbox.Interfaces;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace CoreLoyalty.F5Seconds.Urbox.Repositories
{
    public class UrboxHttpClientRepository : IUrboxHttpClientService
    {
        private readonly HttpClient _client;
        private readonly IConfiguration _config;
        private readonly IMapper _mapper;
        public UrboxHttpClientRepository(HttpClient client, IConfiguration config, IMapper mapper)
        {
            _client = client;
            _config = config;
            _mapper = mapper;
        }
        public async Task<Response<UrboxBuyVocherRes>> BuyVoucherAsync(UrboxBuyVoucherReq voucher)
        {
            var content = new StringContent(JsonConvert.SerializeObject(voucher), Encoding.UTF8, "application/json");
            var response = await _client.PostAsync($"/2.0/cart/cartPayVoucher?app_id={_config["Urbox:AppId"]}&app_secret={_config["Urbox:AppSecret"]}", content);
            if (response.IsSuccessStatusCode)
            {
                var jsonString = await response.Content.ReadAsStringAsync();
                return new Response<UrboxBuyVocherRes>(true,JsonConvert.DeserializeObject<UrboxBuyVocherRes>(jsonString));
            }
            return new Response<UrboxBuyVocherRes>(false,null, "Server Error");
        }

        public async Task<Response<F5sVoucherDetail>> VoucherDetailAsync(int id)
        {
            var response = await _client.GetAsync($"/4.0/gift/detail?app_id={_config["Urbox:AppId"]}&app_secret={_config["Urbox:AppSecret"]}&lang=vi&id={id}");
            if (response.IsSuccessStatusCode)
            {
                var jsonString = await response.Content.ReadAsStringAsync();
                var p = JsonConvert.DeserializeObject<UrboxVoucherDetailData>(jsonString);
                return new Response<F5sVoucherDetail>(true, _mapper.Map<F5sVoucherDetail>(p));
            }
            return new Response<F5sVoucherDetail>(false, null, "Server Error");
        }

        public async Task<Response<List<F5sVoucherBase>>> VoucherListAsync()
        {
            var response = await _client.GetAsync($"/4.0/gift/lists?app_id={_config["Urbox:AppId"]}&app_secret={_config["Urbox:AppSecret"]}");
            if (response.IsSuccessStatusCode)
            {
                var jsonString = await response.Content.ReadAsStringAsync();
                var vouchers = JsonConvert.DeserializeObject<UrboxVoucherList>(jsonString);
                var p = _mapper.Map<List<F5sVoucherBase>>(vouchers.data.items, opt => opt.AfterMap((s, d) =>
                {
                    foreach (var i in d)
                    {
                        i.productPartner = "URBOX";
                    }
                }));
                return new Response<List<F5sVoucherBase>>(true,p);
            }
            return new Response<List<F5sVoucherBase>>(false, null, "Server Error");
        }
    }
}
