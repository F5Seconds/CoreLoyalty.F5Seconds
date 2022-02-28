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
            private readonly IGotItHttpClientExternalService _gotItHttpClientService;
            private readonly ILogger<GetListGotItVoucherQuery> _logger;
            public GetListGotItVoucherQueryHandler(IGotItHttpClientExternalService gotItHttpClientService, ILogger<GetListGotItVoucherQuery> logger)
            {
                _gotItHttpClientService = gotItHttpClientService;
                _logger = logger;
            }
            public async Task<Response<List<F5sVoucherBase>>> Handle(GetListGotItVoucherQuery request, CancellationToken cancellationToken)
            {
                return await _gotItHttpClientService.VoucherListAsync();
            }
        }
    }
}
