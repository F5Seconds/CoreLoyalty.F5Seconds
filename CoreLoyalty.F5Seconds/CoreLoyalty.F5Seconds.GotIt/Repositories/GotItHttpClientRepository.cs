using AutoMapper;
using CoreLoyalty.F5Seconds.Application.Common;
using CoreLoyalty.F5Seconds.Application.DTOs.F5seconds;
using CoreLoyalty.F5Seconds.Application.DTOs.GotIt;
using CoreLoyalty.F5Seconds.Application.Wrappers;
using CoreLoyalty.F5Seconds.GotIt.Interfaces;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using static CoreLoyalty.F5Seconds.Application.DTOs.GotIt.GotItBuyVoucherRes;

namespace CoreLoyalty.F5Seconds.GotIt.Repositories
{
    public class GotItHttpClientRepository : IGotItHttpClientService
    {
        private readonly HttpClient _client;
        private readonly IMapper _mapper;
        private readonly ILogger<GotItHttpClientRepository> _logger;
        public GotItHttpClientRepository(HttpClient client, IMapper mapper, ILogger<GotItHttpClientRepository> logger)
        {
            _client = client;
            _mapper = mapper;
            _logger = logger;
        }
        public async Task<Response<List<F5sVoucherCode>>> BuyVoucherAsync(GotItBuyVoucherReq voucher)
        {
            var content = new StringContent(JsonConvert.SerializeObject(voucher), Encoding.UTF8, "application/json");
            var response = await _client.PostAsync("/api/transaction", content);
            if (response.IsSuccessStatusCode)
            {
                var jsonString = await response.Content.ReadAsStringAsync();
                _logger.LogInformation(jsonString);
                if (Helpers.TryParseJsonConvert(jsonString, out GotItErrorMessage error))
                {
                    return new Response<List<F5sVoucherCode>>(false, null, error.code, new List<string> { error.msg });
                }
                var resultV = JsonConvert.DeserializeObject<List<GotItBuyVoucherRes>>(jsonString);
                List<VoucherInfoRes> vRes = new List<VoucherInfoRes>();
                foreach (var i in resultV)
                {
                    foreach (var v in i.vouchers)
                    {
                        vRes.Add(v);
                    }
                }
                return new Response<List<F5sVoucherCode>>(true,FormatVoucherCode(voucher,vRes));
            }
            var errorStr = await response.Content.ReadAsStringAsync();
            return new Response<List<F5sVoucherCode>>(false, null, $"{response.StatusCode} - {errorStr}");
        }

        private List<F5sVoucherCode> FormatVoucherCode(GotItBuyVoucherReq vReq, List<VoucherInfoRes> vRes)
        {
            List<F5sVoucherCode> f5SVoucherCodes = new List<F5sVoucherCode>();
            foreach (var v in vRes)
            {
                f5SVoucherCodes.Add(new F5sVoucherCode()
                {
                    customerPhone = vReq.phone,
                    expiryDate = vReq.expiryDate,
                    transactionId = vReq.voucherRefId,
                    voucherCode = v.voucherCode,
                    propductId = vReq.productCode,
                    productPrice = vReq.productPrice
                });
            }
            return f5SVoucherCodes;
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
