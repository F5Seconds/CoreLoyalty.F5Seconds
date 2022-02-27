using AutoMapper;
using CoreLoyalty.F5Seconds.Application.DTOs.F5seconds;
using CoreLoyalty.F5Seconds.Application.Interfaces.GotIt;
using CoreLoyalty.F5Seconds.Application.Wrappers;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace CoreLoyalty.F5Seconds.Application.Features.GotIt.Queries.GetListVoucher
{
    public class GetListGotItVoucherQuery : IRequest<Response<List<F5sVoucherBase>>>
    {
        public class GetListGotItVoucherQueryHandler : IRequestHandler<GetListGotItVoucherQuery, Response<List<F5sVoucherBase>>>
        {
            private readonly IGotItHttpClientService _gotItHttpClientService;
            private readonly IMapper _mapper;
            private readonly ILogger<GetListGotItVoucherQuery> _logger;
            public GetListGotItVoucherQueryHandler(IGotItHttpClientService gotItHttpClientService, IMapper mapper, ILogger<GetListGotItVoucherQuery> logger)
            {
                _gotItHttpClientService = gotItHttpClientService;
                _mapper = mapper;
                _logger = logger;
            }
            public async Task<Response<List<F5sVoucherBase>>> Handle(GetListGotItVoucherQuery request, CancellationToken cancellationToken)
            {
                var listV = new List<F5sVoucherBase>();
                var response = await _gotItHttpClientService.VoucherListAsync();
                if (response.Succeeded)
                {
                    foreach (var item in response.Data.productList)
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
                    return new Response<List<F5sVoucherBase>>(true,listV);
                }

                return new Response<List<F5sVoucherBase>>(false,null,response.Message,response.Errors);
            }
        }
    }
}
