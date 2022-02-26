using AutoMapper;
using CoreLoyalty.F5Seconds.Application.DTOs.F5seconds;
using CoreLoyalty.F5Seconds.Application.Features.GotIt.Queries.GetListVoucher;
using CoreLoyalty.F5Seconds.Application.Features.GotIt.Queries.GetVoucher;
using CoreLoyalty.F5Seconds.Application.Features.Urbox.Queries.GetListVoucher;
using CoreLoyalty.F5Seconds.Application.Features.Urbox.Queries.GetVoucher;
using CoreLoyalty.F5Seconds.Application.Interfaces.GotIt;
using CoreLoyalty.F5Seconds.Application.Interfaces.Repositories;
using CoreLoyalty.F5Seconds.Application.Interfaces.Urbox;
using CoreLoyalty.F5Seconds.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
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
        public GatewayController(IUrboxHttpClientService urboxClient, IGotItHttpClientService gotItClient, IProductRepositoryAsync productRepositoryAsync, IMapper mapper)
        {
            _urboxClient = urboxClient;
            _gotItClient = gotItClient;
            _productRepositoryAsync = productRepositoryAsync;
            _mapper = mapper;
        }

        [HttpGet("urbox-vouchers")]
        public async Task<IActionResult> UrboxVoucherListAsync()
        {
            return Ok(await Mediator.Send(new GetListUrboxVoucherQuery()));
        }

        [HttpGet("urbox-voucher-detail/{id}")]
        public async Task<IActionResult> UrboxVoucherDetailAsync(int id)
        {
            return Ok(await Mediator.Send(new GetUrboxVoucherQuery() { Id = id }));
        }

        [HttpGet("gotit-vouchers")]
        public async Task<IActionResult> GotItVoucherListAsync()
        {
            var vouchers = await _gotItClient.VoucherListAsync();
            return Ok(vouchers);
        }

        [HttpGet("vouchers")]
        public async Task<IActionResult> GetVouchers()
        {
            var vouchers = new List<F5sVoucherBase>();
            var gotIt = await Mediator.Send(new GetListGotItVoucherQuery());
            var urbox = await Mediator.Send(new GetListUrboxVoucherQuery());
            vouchers.InsertRange(0, gotIt);
            vouchers.InsertRange(gotIt.Count, urbox);
            var rows = await _productRepositoryAsync.AddRangeAsync(_mapper.Map<List<Product>>(vouchers));
            return Ok(rows);
        }

        [HttpGet("goit-voucher-detail/{id}")]
        public async Task<IActionResult> GotItVoucherDetailAsync(int id)
        {
            return Ok(await Mediator.Send(new GetGotItVoucherQuery() { Id = id}));
        }
    }
}
