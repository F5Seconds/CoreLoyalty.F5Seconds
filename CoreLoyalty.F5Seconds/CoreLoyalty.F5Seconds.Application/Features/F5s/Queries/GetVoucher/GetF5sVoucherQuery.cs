using CoreLoyalty.F5Seconds.Application.DTOs.F5seconds;
using CoreLoyalty.F5Seconds.Application.Interfaces.GotIt;
using CoreLoyalty.F5Seconds.Application.Interfaces.Urbox;
using CoreLoyalty.F5Seconds.Application.Wrappers;
using CoreLoyalty.F5Seconds.Domain.MemoryModels;
using MediatR;
using Microsoft.Extensions.Caching.Memory;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CoreLoyalty.F5Seconds.Application.Features.F5s.Queries.GetVoucher
{
    public class GetF5sVoucherQuery : IRequest<Response<F5sVoucherDetail>>
    {
        public string Id { get; set; }
        public class GetF5sVoucherQueryHandler : IRequestHandler<GetF5sVoucherQuery, Response<F5sVoucherDetail>>
        {
            IUrboxHttpClientExternalService _urboxHttpClientExternalService;
            IGotItHttpClientExternalService _gotItHttpClientExternalService;
            private IMemoryCache _cache;
            public GetF5sVoucherQueryHandler(
                IUrboxHttpClientExternalService urboxHttpClientExternalService, 
                IGotItHttpClientExternalService gotItHttpClientExternalService,
                IMemoryCache cache)
            {
                _urboxHttpClientExternalService = urboxHttpClientExternalService;
                _gotItHttpClientExternalService = gotItHttpClientExternalService;
                _cache = cache;
            }
            public async Task<Response<F5sVoucherDetail>> Handle(GetF5sVoucherQuery request, CancellationToken cancellationToken)
            {
                List<ProductMemory> products;
                _cache.TryGetValue("ProductCache", out products);
                if (products is null) return new Response<F5sVoucherDetail>(false,null,"Not found products");
                var p = products.SingleOrDefault(x => x.ProductCode.Equals(request.Id));
                if (p is null) return new Response<F5sVoucherDetail>(false, null, "Not found product with id");
                if (p.Partner.Equals("URBOX"))
                {
                    return await _urboxHttpClientExternalService.VoucherDetailAsync(p.ProductId);
                }
                if (p.Partner.Equals("GOTIT"))
                {
                    var productGotIt = await _gotItHttpClientExternalService.VoucherDetailAsync(p.ProductId);
                    if(productGotIt.Succeeded) productGotIt.Data.productPrice = p.Price;
                    return productGotIt;
                }
                return new Response<F5sVoucherDetail>(false, null, "Not found data");
            }
        }
    }
}
