using AutoMapper;
using CoreLoyalty.F5Seconds.Application.DTOs.F5seconds;
using CoreLoyalty.F5Seconds.Application.Features.F5s.Commands.CreateTransaction;
using CoreLoyalty.F5Seconds.Application.Features.F5s.Queries.GetListVoucher;
using CoreLoyalty.F5Seconds.Application.Features.F5s.Queries.GetVoucher;
using CoreLoyalty.F5Seconds.Application.Features.GotIt.Queries.GetListVoucher;
using CoreLoyalty.F5Seconds.Application.Features.Urbox.Queries.GetListVoucher;
using CoreLoyalty.F5Seconds.Domain.Entities;
using CoreLoyalty.F5Seconds.Domain.MemoryModels;
using CoreLoyalty.F5Seconds.Domain.Settings;
using MassTransit;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
namespace CoreLoyalty.F5Seconds.Gateway.Controllers
{
    [Route("api/gateway")]
    [ApiController]
    public class GatewayController : BaseApiController
    {
        private IMemoryCache _cache;
        public List<ProductMemory> products;
        private readonly RabbitMqSettings _rabbitMqSettings;
        private readonly IWebHostEnvironment _env;
        private readonly IBus _bus;
        private readonly IMapper _mapper;
        public GatewayController(IMemoryCache cache, IOptions<RabbitMqSettings> rabbitMqSettings, IWebHostEnvironment env, IBus bus, IMapper mapper)
        {
            _cache = cache;
            _rabbitMqSettings = rabbitMqSettings.Value;
            _env = env;
            _bus = bus;
            _mapper = mapper;
        }

        [HttpGet("product-sync")]
        public async Task<IActionResult> GetGotItVouchers()
        {
            string rabbitHost = _rabbitMqSettings.Host;
            string rabbitvHost = _rabbitMqSettings.vHost;
            string productSyncQueue = _rabbitMqSettings.ProductSyncQueue;
            if (_env.IsProduction())
            {
                rabbitHost = Environment.GetEnvironmentVariable("RABBITMQ_HOST");
                rabbitvHost = Environment.GetEnvironmentVariable("RABBITMQ_VHOST");
                productSyncQueue = Environment.GetEnvironmentVariable("RABBITMQ_PRODUCTSYNC");
            }
            var gotIt = await Mediator.Send(new GetListGotItVoucherQuery());
            var urbox = await Mediator.Send(new GetListUrboxVoucherQuery());
            if (gotIt.Succeeded)
            {
                var vouchers = new List<F5sVoucherBase>();
                vouchers.InsertRange(0, gotIt.Data);
                vouchers.InsertRange(gotIt.Data.Count, urbox.Data);
                var v = _mapper.Map<List<Product>>(vouchers);
                Uri uri = new Uri($"rabbitmq://{rabbitHost}/{rabbitvHost}/{productSyncQueue}");
                var endPoint = await _bus.GetSendEndpoint(uri);
                foreach (var i in v)
                {
                    await endPoint.Send(i);
                }
                return Ok();
            }
            return BadRequest();
        }

        [HttpGet("vouchers")]
        public async Task<IActionResult> GetVouchers()
        {
            return Ok(await Mediator.Send(new GetListVoucherQuery()));
        }

        [HttpGet("voucher/{id}")]
        public async Task<IActionResult> GetMemoryVouchers(string id)
        {
            return Ok(await Mediator.Send(new GetF5sVoucherQuery() { Id = id }));
        }

        [HttpPost("transaction")]
        public async Task<IActionResult> CreateTransaction(CreateTransactionCommand command)
        {
            return Ok(await Mediator.Send(command));
        }
    }
}
