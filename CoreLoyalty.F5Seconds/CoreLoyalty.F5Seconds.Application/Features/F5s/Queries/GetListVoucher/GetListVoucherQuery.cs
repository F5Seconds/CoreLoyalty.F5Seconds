using CoreLoyalty.F5Seconds.Application.Interfaces.Repositories;
using CoreLoyalty.F5Seconds.Application.Wrappers;
using CoreLoyalty.F5Seconds.Domain.Entities;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace CoreLoyalty.F5Seconds.Application.Features.F5s.Queries.GetListVoucher
{
    public class GetListVoucherQuery : IRequest<Response<IReadOnlyList<Product>>>
    {
        public class GetListVoucherQueryHandler : IRequestHandler<GetListVoucherQuery, Response<IReadOnlyList<Product>>>
        {
            private readonly IProductRepositoryAsync _productRepositoryAsync;
            public GetListVoucherQueryHandler(IProductRepositoryAsync productRepositoryAsync)
            {
                _productRepositoryAsync = productRepositoryAsync;
            }
            public async Task<Response<IReadOnlyList<Product>>> Handle(GetListVoucherQuery request, CancellationToken cancellationToken)
            {
                var product = await _productRepositoryAsync.GetAllAsync();
                if (product is null) return new Response<IReadOnlyList<Product>>(false, null, "Not found data");
                return new Response<IReadOnlyList<Product>>(true, product);
            }
        }
    }
}
