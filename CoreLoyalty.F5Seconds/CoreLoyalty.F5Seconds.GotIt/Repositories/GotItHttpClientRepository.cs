﻿using AutoMapper;
using CoreLoyalty.F5Seconds.Application.Common;
using CoreLoyalty.F5Seconds.Application.DTOs.F5seconds;
using CoreLoyalty.F5Seconds.Application.DTOs.GotIt;
using CoreLoyalty.F5Seconds.Application.Wrappers;
using CoreLoyalty.F5Seconds.GotIt.Interfaces;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace CoreLoyalty.F5Seconds.GotIt.Repositories
{
    public class GotItHttpClientRepository : IGotItHttpClientService
    {
        private readonly HttpClient _client;
        private readonly IMapper _mapper;
        public GotItHttpClientRepository(HttpClient client, IMapper mapper)
        {
            _client = client;
            _mapper = mapper;
        }
        public async Task<Response<List<GotItBuyVoucherRes>>> BuyVoucherAsync(GotItBuyVoucherReq voucher)
        {
            var content = new StringContent(JsonConvert.SerializeObject(voucher), Encoding.UTF8, "application/json");
            var response = await _client.PostAsync("/api/transaction", content);
            if (response.IsSuccessStatusCode)
            {
                var jsonString = await response.Content.ReadAsStringAsync();
                if (Helpers.TryParseJsonConvert(jsonString, out GotItErrorMessage error))
                {
                    return new Response<List<GotItBuyVoucherRes>>(false, null, error.code, new List<string> { error.msg });
                }
                return new Response<List<GotItBuyVoucherRes>>(true, JsonConvert.DeserializeObject<List<GotItBuyVoucherRes>>(jsonString));
            }
            return new Response<List<GotItBuyVoucherRes>>(false, null, "Server Error");
        }

        public async Task<Response<F5sVoucherDetail>> VoucherDetailAsync(int id)
        {
            var content = new StringContent(JsonConvert.SerializeObject(new PayloadGotItVoucherDetail() { productId = id }), Encoding.UTF8, "application/json");
            var response = await _client.PostAsync("/api/product/detail", content);
            if (response.IsSuccessStatusCode)
            {
                var jsonString = await response.Content.ReadAsStringAsync();
                if (Helpers.TryParseJsonConvert(jsonString, out GotItErrorMessage error))
                {
                    return new Response<F5sVoucherDetail>(false, null, error.code, new List<string> { error.msg });
                }
                var product = JsonConvert.DeserializeObject<GotItVoucherDetail>(jsonString);
                return new Response<F5sVoucherDetail>(true, _mapper.Map<F5sVoucherDetail>(product));
            }
            return new Response<F5sVoucherDetail>(false, null, "Server Error");
        }

        public async Task<Response<List<F5sVoucherBase>>> VoucherListAsync()
        {
            var content = new StringContent(JsonConvert.SerializeObject(new PayloadGotItVoucherList()), Encoding.UTF8, "application/json");
            var response = await _client.PostAsync("/api/product/list", content);
            if (response.IsSuccessStatusCode)
            {
                var jsonString = await response.Content.ReadAsStringAsync();
                if (Helpers.TryParseJsonConvert(jsonString, out GotItErrorMessage error))
                {
                    return new Response<List<F5sVoucherBase>>(false, null, error.code, new List<string> { error.msg });
                }
                var listV = new List<F5sVoucherBase>();
                var res = JsonConvert.DeserializeObject<GotItVoucherList>(jsonString);
                    foreach (var item in res.productList)
                    {
                        var itemV = _mapper.Map<F5sVoucherBase>(item, opt => opt.AfterMap((s, d) => { d.productPartner = "GOTIT"; }));
                        if (item.size.Count > 0)
                        {
                            foreach (var iSize in item.size)
                            {
                                listV.Add(new F5sVoucherBase()
                                {
                                    brandLogo = itemV.brandLogo,
                                    brandNm = itemV.brandNm,
                                    productId = itemV.productId,
                                    productImg = itemV.productImg,
                                    productNm = itemV.productNm,
                                    productPartner = itemV.productPartner,
                                    productPrice = iSize.priceValue,
                                    productSize = iSize.priceId,
                                    productTyp = itemV.productTyp
                                });
                            }
                        }
                        else
                        {
                            listV.Add(itemV);
                        }
                    }
                    return new Response<List<F5sVoucherBase>>(true, listV);
            }
            return new Response<List<F5sVoucherBase>>(false, null, "Server Error");
        }
    }
}
