using AutoMapper;
using CoreLoyalty.F5Seconds.Application.DTOs.F5seconds;
using CoreLoyalty.F5Seconds.Application.Interfaces.Urbox;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace CoreLoyalty.F5Seconds.Application.Features.Urbox.Queries.GetVoucher
{
    public class GetUrboxVoucherQuery : IRequest<F5sVoucherBase>
    {
        public int Id { get; set; }
        public class GetVoucherQueryHandler : IRequestHandler<GetUrboxVoucherQuery, F5sVoucherBase>
        {
            IUrboxHttpClientExternalService _urboxHttpClientService;
            private readonly IMapper _mapper;
            public GetVoucherQueryHandler(IUrboxHttpClientExternalService urboxHttpClientService, IMapper mapper)
            {
                _urboxHttpClientService = urboxHttpClientService;
                _mapper = mapper;   
            }
            public async Task<F5sVoucherBase> Handle(GetUrboxVoucherQuery request, CancellationToken cancellationToken)
            {
                var voucher = await _urboxHttpClientService.VoucherDetailAsync(request.Id);
                return _mapper.Map<F5sVoucherBase>(voucher);
            }
        }
    }
}
