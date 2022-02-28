using CoreLoyalty.F5Seconds.Application.DTOs.GotIt;
using CoreLoyalty.F5Seconds.Application.Wrappers;
using CoreLoyalty.F5Seconds.GotIt.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace CoreLoyalty.F5Seconds.GotIt.Controllers.v1
{
    [ApiVersion("1.0")]
    public class ProductController : BaseApiController
    {
        private readonly IGotItHttpClientService _gotItHttpClientService;
        private readonly ILogger<ProductController> _logger;
        public ProductController(IGotItHttpClientService gotItHttpClientService, ILogger<ProductController> logger)
        {
            _gotItHttpClientService = gotItHttpClientService;
            _logger = logger;   
        }
        [HttpGet("list")]
        public async Task<IActionResult> GetProductList()
        {
            var product = await _gotItHttpClientService.VoucherListAsync();
            return Ok(product);
        }

        [HttpGet("detail/{id}")]
        public async Task<IActionResult> GetProductDetail(int id)
        {
            var voucher = await _gotItHttpClientService.VoucherDetailAsync(id);
            return Ok(voucher);
        }

        [HttpPost("transaction")]
        public async Task<IActionResult> PostTransaction(GotItBuyVoucherReq payload)
        {
            var gotItBuy = await _gotItHttpClientService.BuyVoucherAsync(payload);
            return Ok(gotItBuy);
        }
    }
}
