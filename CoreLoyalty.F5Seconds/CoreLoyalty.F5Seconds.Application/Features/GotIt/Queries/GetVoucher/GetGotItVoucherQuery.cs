using AutoMapper;
using CoreLoyalty.F5Seconds.Application.DTOs.F5seconds;
using CoreLoyalty.F5Seconds.Application.Exceptions;
using CoreLoyalty.F5Seconds.Application.Interfaces.GotIt;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace CoreLoyalty.F5Seconds.Application.Features.GotIt.Queries.GetVoucher
{
    public class GetGotItVoucherQuery : IRequest<F5sVoucherBase>
    {
        public int Id { get; set; }
        public class GetGotItVoucherQueryHandler : IRequestHandler<GetGotItVoucherQuery, F5sVoucherBase>
        {
            private readonly IGotItHttpClientService _gotItHttpClientService;
            private readonly IMapper _mapper;
            public GetGotItVoucherQueryHandler(IGotItHttpClientService gotItHttpClientService, IMapper mapper)
            {
                _gotItHttpClientService = gotItHttpClientService;
                _mapper = mapper;
            }
            public async Task<F5sVoucherBase> Handle(GetGotItVoucherQuery request, CancellationToken cancellationToken)
            {
                var voucher = await _gotItHttpClientService.VoucherDetailAsync(request.Id);
                if (voucher == null) throw new ApiException($"Not found data");
                return _mapper.Map<F5sVoucherBase>(voucher);
            }
        }
    }
}
