using AutoMapper;
using CoreLoyalty.F5Seconds.Application.DTOs.F5seconds;
using CoreLoyalty.F5Seconds.Application.Interfaces.Urbox;
using CoreLoyalty.F5Seconds.Application.Wrappers;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace CoreLoyalty.F5Seconds.Application.Features.Urbox.Queries.GetListVoucher
{
    public class GetListUrboxVoucherQuery: IRequest<Response<List<F5sVoucherBase>>>
    {
        public class GetListVoucherQueryHandler : IRequestHandler<GetListUrboxVoucherQuery, Response<List<F5sVoucherBase>>>
        {
            IUrboxHttpClientExternalService _urboxHttpClientService;
            private readonly IMapper _mapper;
            public GetListVoucherQueryHandler(IUrboxHttpClientExternalService urboxHttpClientService, IMapper mapper)
            {
                _urboxHttpClientService = urboxHttpClientService;
                _mapper = mapper;
            }
            public async Task<Response<List<F5sVoucherBase>>> Handle(GetListUrboxVoucherQuery request, CancellationToken cancellationToken)
            {
                return await _urboxHttpClientService.VoucherListAsync();
            }
        }
    }
}
