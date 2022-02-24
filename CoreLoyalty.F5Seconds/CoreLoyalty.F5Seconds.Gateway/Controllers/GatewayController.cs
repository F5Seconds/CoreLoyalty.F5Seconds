using CoreLoyalty.F5Seconds.Gateway.Entities;
using CoreLoyalty.F5Seconds.Gateway.HttpClients;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CoreLoyalty.F5Seconds.Gateway.Controllers
{
    [Route("api/gateway")]
    [ApiController]
    public class GatewayController : ControllerBase
    {
        private readonly IUrboxClient _urboxClient;
        private readonly IGotItClient _gotItClient;
        private static readonly List<Products> _products = new List<Products>()
        {
            new Products(){ Id = "F5S.000001",ProductId = "820",Partner = "GOTIT"},
            new Products(){ Id = "F5S.000002",ProductId = "1408",Partner = "GOTIT"},
            new Products(){ Id = "F5S.000003",ProductId = "987",Partner = "GOTIT"},
            new Products(){ Id = "F5S.000004",ProductId = "2478",Partner = "GOTIT"},
            new Products(){ Id = "F5S.000005",ProductId = "2442",Partner = "GOTIT"},
            new Products(){ Id = "F5S.000006",ProductId = "4732",Partner = "URBOX"},
            new Products(){ Id = "F5S.000007",ProductId = "4727",Partner = "URBOX"},
            new Products(){ Id = "F5S.000008",ProductId = "4724",Partner = "URBOX"},
            new Products(){ Id = "F5S.000009",ProductId = "4723",Partner = "URBOX"},
            new Products(){ Id = "F5S.000010",ProductId = "4669",Partner = "URBOX"},
        };
        public GatewayController(IUrboxClient urboxClient, IGotItClient gotItClient)
        {
            _urboxClient = urboxClient;
            _gotItClient = gotItClient;
        }

        [HttpGet("urbox-vouchers")]
        public async Task<IActionResult> UrboxVoucherListAsync()
        {
            var vouchers = await _urboxClient.VoucherListAsync();
            if(vouchers is null) return NotFound();
            return Ok(vouchers);
        }

        [HttpGet("urbox-voucher-detail/{id}")]
        public async Task<IActionResult> UrboxVoucherDetailAsync(int id)
        {
            var vouchers = await _urboxClient.VoucherDetailAsync(id);
            if (vouchers is null) return NotFound();
            return Ok(vouchers);
        }

        [HttpGet("gotit-vouchers")]
        public async Task<IActionResult> GotItVoucherListAsync()
        {
            var vouchers = await _gotItClient.VoucherListAsync();
            if (vouchers is null) return NotFound();
            return Ok(vouchers);
        }

        [HttpGet("goit-voucher-detail/{id}")]
        public async Task<IActionResult> GotItVoucherDetailAsync(int id)
        {
            var vouchers = await _gotItClient.VoucherDetailAsync(id);
            if (vouchers is null) return NotFound();
            return Ok(vouchers);
        }

        [HttpGet("f5s-voucher-detail/{id}")]
        public async Task<IActionResult> F5sVoucherDetailAsync(string id)
        {
            var product = _products.Find(x => x.Id.Equals(id));
            if(product is null) return NotFound();
            if (product.Partner.Equals("URBOX"))
            {
                var vUrbox = await _urboxClient.VoucherDetailAsync(int.Parse(product.ProductId));
                if (vUrbox is null) return NotFound();
                return Ok(vUrbox);
            }
            if (product.Partner.Equals("GOTIT"))
            {
                var vGotIt = await _gotItClient.VoucherDetailAsync(int.Parse(product.ProductId));
                if (vGotIt is null) return NotFound();
                return Ok(vGotIt);
            }
            return NotFound();
        }
    }
}
