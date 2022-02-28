using AutoMapper;
using CoreLoyalty.F5Seconds.Application.DTOs.F5seconds;
using CoreLoyalty.F5Seconds.Application.Features.F5s.Commands.CreateTransaction;
using CoreLoyalty.F5Seconds.Application.Features.F5s.Queries.GetVoucher;
using CoreLoyalty.F5Seconds.Application.Features.GotIt.Queries.GetListVoucher;
using CoreLoyalty.F5Seconds.Application.Features.Urbox.Queries.GetListVoucher;
using CoreLoyalty.F5Seconds.Application.Interfaces.GotIt;
using CoreLoyalty.F5Seconds.Application.Interfaces.Repositories;
using CoreLoyalty.F5Seconds.Application.Interfaces.Urbox;
using CoreLoyalty.F5Seconds.Domain.Entities;
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
        private readonly IUrboxHttpClientExternalService _urboxClient;
        private readonly IGotItHttpClientExternalService _gotItClient;
        private readonly IProductRepositoryAsync _productRepositoryAsync;
        private readonly IMapper _mapper;
        private readonly IBus _bus;
        private readonly IConfiguration _config;
        private readonly ILogger<GatewayController> _logger;
        private IMemoryCache _cache;
        public GatewayController(IUrboxHttpClientExternalService urboxClient, IGotItHttpClientExternalService gotItClient, IProductRepositoryAsync productRepositoryAsync, IMapper mapper, IBus bus, IConfiguration config, ILogger<GatewayController> logger, IMemoryCache cache)
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
            var vouchers = new List<F5sVoucherBase>();
            var gotIt = await Mediator.Send(new GetListGotItVoucherQuery());
            var urbox = await Mediator.Send(new GetListUrboxVoucherQuery());
            if (gotIt.Succeeded && urbox.Succeeded)
            {
                vouchers.InsertRange(0, gotIt.Data);
                vouchers.InsertRange(gotIt.Data.Count, urbox.Data);
            }
            return Ok(_mapper.Map<List<Product>>(vouchers));
        }

        [HttpGet("voucher/{id}")]
        public async Task<IActionResult> GetMemoryVouchers(string id)
        {
            return Ok(await Mediator.Send(new GetF5sVoucherQuery() { Id = id}));
        }

        [HttpPost("transaction")]
        public async Task<IActionResult> CreateTransaction(CreateTransactionCommand command)
        {
            return Ok(await Mediator.Send(command));
        }
    }
}
