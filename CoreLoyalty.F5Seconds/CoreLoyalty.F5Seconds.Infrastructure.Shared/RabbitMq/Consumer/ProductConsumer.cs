using CoreLoyalty.F5Seconds.Application.Interfaces.Repositories;
using CoreLoyalty.F5Seconds.Domain.Entities;
using CoreLoyalty.F5Seconds.Shared.RabbitMq.Publisher;
using MassTransit;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace CoreLoyalty.F5Seconds.Shared.RabbitMq.Consumer
{
    public class ProductConsumer : IConsumer<Product>
    {
        private readonly ILogger<ProductConsumer> _logger;
        private readonly IProductRepositoryAsync _productRepositoryAsync;
        public ProductConsumer(ILogger<ProductConsumer> logger, IProductRepositoryAsync productRepositoryAsync)
        {
            _logger = logger;
            _productRepositoryAsync = productRepositoryAsync;
        }
        public async Task Consume(ConsumeContext<Product> context)
        {
            var p = context.Message;
            var exited = await _productRepositoryAsync.IsUniqueProductAsync(p.ProductId,p.Partner,p.Size);
            _logger.LogInformation($"{exited is not null}");
            if (exited is not null)
            {
                exited.Price = p.Price;
                exited.Name = p.Name;
                exited.Type = p.Type;
                exited.BrandLogo = p.BrandLogo;
                exited.BrandName = p.BrandName;
                exited.Image = p.Image;
                await _productRepositoryAsync.UpdateAsync(exited);
            }
            else
            {
                await _productRepositoryAsync.AddAsync(p);
            }
        }
    }
}
