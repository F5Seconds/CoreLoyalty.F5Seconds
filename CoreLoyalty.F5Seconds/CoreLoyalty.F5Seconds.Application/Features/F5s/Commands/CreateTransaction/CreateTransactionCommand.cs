using AutoMapper;
using CoreLoyalty.F5Seconds.Application.DTOs.GotIt;
using CoreLoyalty.F5Seconds.Application.DTOs.Urox;
using CoreLoyalty.F5Seconds.Application.Interfaces.GotIt;
using CoreLoyalty.F5Seconds.Application.Interfaces.Urbox;
using CoreLoyalty.F5Seconds.Application.Wrappers;
using CoreLoyalty.F5Seconds.Domain.MemoryModels;
using MediatR;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CoreLoyalty.F5Seconds.Application.Features.F5s.Commands.CreateTransaction
{
    public class CreateTransactionCommand : IRequest<Response<object>>
    {
        public string propductId { get; set; }
        public int quantity { get; set; }
        public string transactionId { get; set; }
        public string customerId { get; set; }
        public string customerPhone { get; set; }

        public class CreateTransactionCommandHandler : IRequestHandler<CreateTransactionCommand, Response<object>>
        {
            private IMemoryCache _cache;
            public List<ProductMemory> products;
            private readonly IUrboxHttpClientService _urboxClient;
            private readonly IGotItHttpClientService _gotItClient;
            private readonly IMapper _mapper;
            public CreateTransactionCommandHandler(IMemoryCache cache, IUrboxHttpClientService urboxClient, IGotItHttpClientService gotItClient, IMapper mapper)
            {
                _cache = cache;
                _urboxClient = urboxClient;
                _gotItClient = gotItClient;
                _mapper = mapper;
            }
            public async Task<Response<object>> Handle(CreateTransactionCommand request, CancellationToken cancellationToken)
            {
                _cache.TryGetValue("ProductCache", out products);
                if(products is null) return new Response<object>(false, "No data found");
                var p = products.SingleOrDefault(x => x.Code.Equals(request.propductId));
                if(p is null) return new Response<object>(false, "No data found");
                if (p.Partner.Equals("URBOX"))
                {
                    var urboxBuyInfo = _mapper.Map<UrboxBuyVoucherReq>(request,opt => opt.AfterMap((s,d) => d.dataBuy = new List<UrboxBuyVoucherReq.UrboxBuyVoucherItem>()));
                    urboxBuyInfo.dataBuy.Add(new UrboxBuyVoucherReq.UrboxBuyVoucherItem()
                    {
                        priceId = p.ProductId,
                        quantity = request.quantity
                    });
                    var urboxBuy = await _urboxClient.BuyVoucherAsync(urboxBuyInfo);
                    if(urboxBuy is null) return new Response<object>(false, "Bad request");
                    return new Response<object>(true,urboxBuy);
                }
                if (p.Partner.Equals("GOTIT"))
                {
                    var gotItBuyInfo = _mapper.Map<GotItBuyVoucherReq>(request, opt => opt.AfterMap((s, d) => {
                        d.productId = p.ProductId;
                        d.productPriceId = p.Size; 
                    }));
                    var gotItBuy = await _gotItClient.BuyVoucherAsync(gotItBuyInfo);
                    if (gotItBuy.Succeeded)
                    {
                        return new Response<object>(true, gotItBuy.Data);
                    }
                    return new Response<object>(false,null,gotItBuy.Message,gotItBuy.Errors);
                }
                return new Response<object>(false,p);
            }
        }
    }
}
