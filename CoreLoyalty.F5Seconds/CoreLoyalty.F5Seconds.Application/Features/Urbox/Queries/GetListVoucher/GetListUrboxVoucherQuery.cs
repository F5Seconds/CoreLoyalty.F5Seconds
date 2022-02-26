using AutoMapper;
using CoreLoyalty.F5Seconds.Application.DTOs.F5seconds;
using CoreLoyalty.F5Seconds.Application.Interfaces.Urbox;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace CoreLoyalty.F5Seconds.Application.Features.Urbox.Queries.GetListVoucher
{
    public class GetListUrboxVoucherQuery: IRequest<List<F5sVoucherBase>>
    {
        public class GetListVoucherQueryHandler : IRequestHandler<GetListUrboxVoucherQuery, List<F5sVoucherBase>>
        {
            IUrboxHttpClientService _urboxHttpClientService;
            private readonly IMapper _mapper;
            public GetListVoucherQueryHandler(IUrboxHttpClientService urboxHttpClientService, IMapper mapper)
            {
                _urboxHttpClientService = urboxHttpClientService;
                _mapper = mapper;
            }
            public async Task<List<F5sVoucherBase>> Handle(GetListUrboxVoucherQuery request, CancellationToken cancellationToken)
            {
                var voucher = await _urboxHttpClientService.VoucherListAsync();
                return _mapper.Map<List<F5sVoucherBase>>(voucher.data.items, opt => opt.AfterMap((s, d) =>
                {
                    foreach (var i in d)
                    {
                        i.productPartner = "URBOX";
                    }
                }));
            }
        }
    }
}
