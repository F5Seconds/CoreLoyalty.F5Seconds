using AutoMapper;
using CoreLoyalty.F5Seconds.Application.DTOs.F5seconds;
using CoreLoyalty.F5Seconds.Application.Wrappers;
using CoreLoyalty.F5Seconds.Urbox.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CoreLoyalty.F5Seconds.Urbox.Controllers.v1
{
    [ApiVersion("1.0")]
    public class ProductController : BaseApiController
    {
        private readonly IUrboxHttpClientService _urboxHttpClientService;
        private readonly IMapper _mapper;
        private readonly ILogger<ProductController> _logger;
        public ProductController(IUrboxHttpClientService urboxHttpClientService, IMapper mapper, ILogger<ProductController> logger)
        {
            _urboxHttpClientService = urboxHttpClientService;
            _mapper = mapper;
            _logger = logger;   
        }

        [HttpGet("list")]
        public async Task<IActionResult> GetProductList()
        {
            var voucher = await _urboxHttpClientService.VoucherListAsync();
            return Ok(voucher);
            
        }

        [HttpGet("detail/{id}")]
        public async Task<IActionResult> GetProductDetail(int id)
        {
            var voucher = await _urboxHttpClientService.VoucherDetailAsync(id);
            return Ok(voucher);
        }

        //[HttpPost("transaction")]
        //public async Task<IActionResult> PostTransaction(GotItBuyVoucherReq payload)
        //{

        //}
    }
}
