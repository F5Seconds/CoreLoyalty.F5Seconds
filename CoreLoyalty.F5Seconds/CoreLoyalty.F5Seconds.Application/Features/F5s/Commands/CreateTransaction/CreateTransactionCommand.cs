using AutoMapper;
using CoreLoyalty.F5Seconds.Application.DTOs.F5seconds;
using CoreLoyalty.F5Seconds.Application.DTOs.GotIt;
using CoreLoyalty.F5Seconds.Application.DTOs.Urox;
using CoreLoyalty.F5Seconds.Application.Interfaces.GotIt;
using CoreLoyalty.F5Seconds.Application.Interfaces.Urbox;
using CoreLoyalty.F5Seconds.Application.Wrappers;
using CoreLoyalty.F5Seconds.Domain.MemoryModels;
using MediatR;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CoreLoyalty.F5Seconds.Application.Features.F5s.Commands.CreateTransaction
{
    public class CreateTransactionCommand : IRequest<Response<List<F5sVoucherCode>>>
    {
        public string productCode { get; set; }
        public int quantity { get; set; }
        public string transactionId { get; set; }
        public string customerId { get; set; }
        public string customerPhone { get; set; }

        public class CreateTransactionCommandHandler : IRequestHandler<CreateTransactionCommand, Response<List<F5sVoucherCode>>>
        {
            private IMemoryCache _cache;
            public List<ProductMemory> products;
            private readonly IUrboxHttpClientExternalService _urboxClient;
            private readonly IGotItHttpClientExternalService _gotItClient;
            private readonly IMapper _mapper;
            private readonly ILogger<CreateTransactionCommandHandler> _logger;
            public CreateTransactionCommandHandler(
                IMemoryCache cache,
                IUrboxHttpClientExternalService urboxClient,
                IGotItHttpClientExternalService gotItClient,
                ILogger<CreateTransactionCommandHandler> logger,
                IMapper mapper)
            {
                _cache = cache;
                _urboxClient = urboxClient;
                _gotItClient = gotItClient;
                _mapper = mapper;
                _logger = logger;
            }
            public async Task<Response<List<F5sVoucherCode>>> Handle(CreateTransactionCommand request, CancellationToken cancellationToken)
            {
                _cache.TryGetValue("ProductCache", out products);
                if (products is null) return new Response<List<F5sVoucherCode>>(false, null, "No data found");
                var p = products.SingleOrDefault(x => x.Code.Equals(request.productCode));
                if (p is null) return new Response<List<F5sVoucherCode>>(false, null, "No data found");
                if (p.Partner.Equals("URBOX"))
                {
                    var urboxBuyInfo = _mapper.Map<UrboxBuyVoucherReq>(request, opt => opt.AfterMap((s, d) =>
                    {
                        d.productPrice = p.Price;
                        d.productCode = p.Code;
                        d.productType = p.Type;
                        d.dataBuy = new List<UrboxBuyVoucherReq.UrboxBuyVoucherItem>();
                    }));
                    urboxBuyInfo.dataBuy.Add(new UrboxBuyVoucherReq.UrboxBuyVoucherItem()
                    {
                        priceId = p.ProductId,
                        quantity = request.quantity
                    });
                    urboxBuyInfo.transaction_id = "00000000967";
                    var urboxBuy = await _urboxClient.BuyVoucherAsync(urboxBuyInfo);
                    return urboxBuy;
                }
                if (p.Partner.Equals("GOTIT"))
                {
                    var gotItBuyInfo = _mapper.Map<GotItBuyVoucherReq>(request, opt => opt.AfterMap((s, d) =>
                    {
                        d.productId = p.ProductId;
                        d.productPriceId = p.Size;
                        d.productCode = p.Code;
                        d.productPrice = p.Price;
                    }));
                    var gotItBuy = await _gotItClient.BuyVoucherAsync(gotItBuyInfo);

                    return gotItBuy;
                }
                return new Response<List<F5sVoucherCode>>(false, null, "Not found data");
            }
        }
    }
}
