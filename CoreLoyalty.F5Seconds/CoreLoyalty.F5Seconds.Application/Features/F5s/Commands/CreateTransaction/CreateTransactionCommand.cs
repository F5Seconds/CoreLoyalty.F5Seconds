using AutoMapper;
using CoreLoyalty.F5Seconds.Application.DTOs.F5seconds;
using CoreLoyalty.F5Seconds.Application.DTOs.GotIt;
using CoreLoyalty.F5Seconds.Application.DTOs.Urox;
using CoreLoyalty.F5Seconds.Application.Interfaces.GotIt;
using CoreLoyalty.F5Seconds.Application.Interfaces.Repositories;
using CoreLoyalty.F5Seconds.Application.Interfaces.Urbox;
using CoreLoyalty.F5Seconds.Application.Wrappers;
using CoreLoyalty.F5Seconds.Domain.Entities;
using CoreLoyalty.F5Seconds.Domain.MemoryModels;
using MediatR;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CoreLoyalty.F5Seconds.Application.Features.F5s.Commands.CreateTransaction
{
    public class CreateTransactionCommand : IRequest<Response<List<F5sVoucherCode>>>
    {
        public string propductId { get; set; }
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
            private readonly ITransactionRequestRepositoryAsync _transactionRequest;
            private readonly ITransactionResponseRepositoryAsync _transactionResponse;
            private readonly ILogger<CreateTransactionCommandHandler> _logger;
            public CreateTransactionCommandHandler(
                IMemoryCache cache, 
                IUrboxHttpClientExternalService urboxClient, 
                IGotItHttpClientExternalService gotItClient,
                ITransactionRequestRepositoryAsync transactionRequest,
                ITransactionResponseRepositoryAsync transactionResponse,
                ILogger<CreateTransactionCommandHandler> logger,
                IMapper mapper)
            {
                _cache = cache;
                _urboxClient = urboxClient;
                _gotItClient = gotItClient;
                _mapper = mapper;
                _transactionRequest = transactionRequest;
                _transactionResponse = transactionResponse;
                _logger = logger;
            }
            public async Task<Response<List<F5sVoucherCode>>> Handle(CreateTransactionCommand request, CancellationToken cancellationToken)
            {
                _cache.TryGetValue("ProductCache", out products);
                if(products is null) return new Response<List<F5sVoucherCode>>(false,null, "No data found");
                var p = products.SingleOrDefault(x => x.Code.Equals(request.propductId));
                if(p is null) return new Response<List<F5sVoucherCode>>(false,null, "No data found");
                await _transactionRequest.AddAsync(_mapper.Map<TransactionRequest>(request));
                if (p.Partner.Equals("URBOX"))
                {
                    var urboxBuyInfo = _mapper.Map<UrboxBuyVoucherReq>(request, opt => opt.AfterMap((s, d) => { 
                        d.productPrice = p.Price;
                        d.dataBuy = new List<UrboxBuyVoucherReq.UrboxBuyVoucherItem>(); 
                    }));
                    urboxBuyInfo.dataBuy.Add(new UrboxBuyVoucherReq.UrboxBuyVoucherItem()
                    {
                        priceId = p.ProductId,
                        quantity = request.quantity
                    });
                    urboxBuyInfo.transaction_id = "00000000967";
                    _logger.LogInformation(JsonConvert.SerializeObject(urboxBuyInfo));
                    var urboxBuy = await _urboxClient.BuyVoucherAsync(urboxBuyInfo);
                    if (urboxBuy.Succeeded) await _transactionResponse.AddRangeAsync(_mapper.Map<List<TransactionResponse>>(urboxBuy.Data));
                    return urboxBuy;
                }
                if (p.Partner.Equals("GOTIT"))
                {
                    var gotItBuyInfo = _mapper.Map<GotItBuyVoucherReq>(request, opt => opt.AfterMap((s, d) => {
                        d.productId = p.ProductId;
                        d.productPriceId = p.Size;
                        d.productCode = p.Code;
                        d.productPrice = p.Price;
                    }));
                    var gotItBuy = await _gotItClient.BuyVoucherAsync(gotItBuyInfo);

                    if (gotItBuy.Succeeded) await _transactionResponse.AddRangeAsync(_mapper.Map<List<TransactionResponse>>(gotItBuy.Data));
                    return gotItBuy;
                }
                return new Response<List<F5sVoucherCode>>(false,null,"Bad request");
            }
        }
    }
}
