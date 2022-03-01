using CoreLoyalty.F5Seconds.Application.Features.F5s.Commands.CreateTransaction;
using CoreLoyalty.F5Seconds.Application.Features.F5s.Queries.GetListVoucher;
using CoreLoyalty.F5Seconds.Application.Features.F5s.Queries.GetVoucher;
using CoreLoyalty.F5Seconds.Domain.MemoryModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
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
        public GatewayController(IMemoryCache cache)
        {
            _cache = cache;
        }
        [HttpGet("fake-trans")]
        public async Task<IActionResult> GetGotItVouchers()
        {
            _cache.TryGetValue("ProductCache", out products);
            if (products is null) return NotFound();
            Random rnd = new Random();
            int r = rnd.Next(products.Count);
            var p = products[r];
            var result = await Mediator.Send(new CreateTransactionCommand()
            {
                customerId = "341721226",
                customerPhone = "0979999067",
                propductId = p.Code,
                quantity = 1,
                transactionId = Guid.NewGuid().ToString()
            });
            return Ok(result);
        }

        [HttpGet("vouchers")]
        public async Task<IActionResult> GetVouchers()
        {
            return Ok(await Mediator.Send(new GetListVoucherQuery()));
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
