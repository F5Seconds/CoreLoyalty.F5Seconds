using AutoMapper;
using CoreLoyalty.F5Seconds.Application.DTOs.F5seconds;
using CoreLoyalty.F5Seconds.Application.DTOs.GotIt;
using CoreLoyalty.F5Seconds.Application.Interfaces.GotIt;
using MediatR;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CoreLoyalty.F5Seconds.Application.Features.GotIt.Queries.GetListVoucher
{
    public class GetListGotItVoucherQuery : IRequest<List<F5sVoucherBase>>
    {
        public class GetListGotItVoucherQueryHandler : IRequestHandler<GetListGotItVoucherQuery, List<F5sVoucherBase>>
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
            public async Task<List<F5sVoucherBase>> Handle(GetListGotItVoucherQuery request, CancellationToken cancellationToken)
            {
                var listV = new List<F5sVoucherBase>();
                var voucher = await _gotItHttpClientService.VoucherListAsync();
                foreach (var item in voucher.productList)
                {
                    var itemV = _mapper.Map<F5sVoucherBase>(item,opt => opt.AfterMap((s,d) => { d.productPartner = "GOTIT"; }));
                    if(item.size.Count > 0)
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
                return listV;
            }
        }
    }
}
