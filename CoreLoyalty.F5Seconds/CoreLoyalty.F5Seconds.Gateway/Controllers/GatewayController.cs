using AutoMapper;
using CoreLoyalty.F5Seconds.Application.DTOs.F5seconds;
using CoreLoyalty.F5Seconds.Application.Features.F5s.Commands.CreateTransaction;
using CoreLoyalty.F5Seconds.Application.Features.GotIt.Queries.GetListVoucher;
using CoreLoyalty.F5Seconds.Application.Interfaces.GotIt;
using CoreLoyalty.F5Seconds.Application.Interfaces.Repositories;
using CoreLoyalty.F5Seconds.Application.Interfaces.Urbox;
using CoreLoyalty.F5Seconds.Domain.MemoryModels;
using MassTransit;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
namespace CoreLoyalty.F5Seconds.Gateway.Controllers
{
    [Route("api/gateway")]
    [ApiController]
    public class GatewayController : BaseApiController
    {
        private readonly IUrboxHttpClientService _urboxClient;
        private readonly IGotItHttpClientService _gotItClient;
        private readonly IProductRepositoryAsync _productRepositoryAsync;
        private readonly IMapper _mapper;
        private readonly IBus _bus;
        private readonly IConfiguration _config;
        private readonly ILogger<GatewayController> _logger;
        private IMemoryCache _cache;
        public GatewayController(IUrboxHttpClientService urboxClient, IGotItHttpClientService gotItClient, IProductRepositoryAsync productRepositoryAsync, IMapper mapper, IBus bus, IConfiguration config, ILogger<GatewayController> logger, IMemoryCache cache)
        {
            _urboxClient = urboxClient;
            _gotItClient = gotItClient;
            _productRepositoryAsync = productRepositoryAsync;
            _mapper = mapper;
            _bus = bus;
            _config = config;  
            _logger = logger;
            _cache = cache;
        }

        [HttpGet("gotit-vouchers")]
        public async Task<IActionResult> GetGotItVouchers()
        {
            return Ok(await Mediator.Send(new GetListGotItVoucherQuery()));
        }
        [HttpGet("vouchers")]
        public async Task<IActionResult> GetVouchers()
        {
            var products = await _productRepositoryAsync.GetAllAsync();
            if(products is null) return NotFound();
            return Ok(products);
        }

        [HttpGet("voucher/{id}")]
        public async Task<IActionResult> GetMemoryVouchers(string id)
        {
            List<ProductMemory> products;
            _cache.TryGetValue("ProductCache", out products);
            if(products is null) return NotFound();
            var p = products.SingleOrDefault(x => x.Code.Equals(id));
            if(p is null) return NotFound();
            if (p.Partner.Equals("URBOX"))
            {
                var urboxDetail = await _urboxClient.VoucherDetailAsync(p.ProductId);
                if(urboxDetail is null) return NotFound();
                return Ok(_mapper.Map<F5sVoucherDetail>(urboxDetail,opt => opt.AfterMap((s,d) => d.productPartner = "URBOX")));
            }
            if (p.Partner.Equals("GOTIT"))
            {
                var gotItDetail = await _gotItClient.VoucherDetailAsync(p.ProductId);
                if (gotItDetail.Succeeded)
                {
                    var f5sVoucherDetail = _mapper.Map<F5sVoucherDetail>(gotItDetail.Data, opt => opt.AfterMap((s, d) =>
                    {
                        d.productPartner = "GOTIT";
                        d.productPrice = p.Price;
                    }));
                    return Ok(new Application.Wrappers.Response<F5sVoucherDetail>(true, f5sVoucherDetail));
                }
                return Ok(gotItDetail);
            }
            return BadRequest();
        }

        [HttpPost("transaction")]
        public async Task<IActionResult> CreateTransaction(CreateTransactionCommand command)
        {
            return Ok(await Mediator.Send(command));
        }
    }
}
