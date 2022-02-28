using AutoMapper;
using CoreLoyalty.F5Seconds.Application.DTOs.F5seconds;
using CoreLoyalty.F5Seconds.Application.Interfaces.GotIt;
using CoreLoyalty.F5Seconds.Application.Wrappers;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace CoreLoyalty.F5Seconds.Application.Features.GotIt.Queries.GetVoucher
{
    public class GetGotItVoucherQuery : IRequest<Response<F5sVoucherBase>>
    {
        public int Id { get; set; }
        public class GetGotItVoucherQueryHandler : IRequestHandler<GetGotItVoucherQuery, Response<F5sVoucherBase>>
        {
            private readonly IGotItHttpClientExternalService _gotItHttpClientService;
            private readonly IMapper _mapper;
            public GetGotItVoucherQueryHandler(IGotItHttpClientExternalService gotItHttpClientService, IMapper mapper)
            {
                _gotItHttpClientService = gotItHttpClientService;
                _mapper = mapper;
            }
            public async Task<Response<F5sVoucherBase>> Handle(GetGotItVoucherQuery request, CancellationToken cancellationToken)
            {
                var voucher = await _gotItHttpClientService.VoucherDetailAsync(request.Id);
                if (voucher.Succeeded)
                {
                    return new Response<F5sVoucherBase>(true, _mapper.Map<F5sVoucherBase>(voucher.Data));
                }
                return new Response<F5sVoucherBase>(false, null, voucher.Message, voucher.Errors);
            }
        }
    }
}
