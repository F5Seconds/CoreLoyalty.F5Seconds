using AutoMapper;
using CoreLoyalty.F5Seconds.Application.Interfaces.Repositories;
using CoreLoyalty.F5Seconds.Domain.MemoryModels;
using CoreLoyalty.F5Seconds.Domain.Settings;
using CoreLoyalty.F5Seconds.Infrastructure.Persistence.Contexts;
using MassTransit;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using NCrontab;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace CoreLoyalty.F5Seconds.Gateway.HostedService
{
    public class ProductMemoryHostedService : IHostedService, IDisposable
    {
        private readonly CrontabSchedule _crontabSchedule;
        private readonly IServiceProvider _service;
        private DateTime _nextRun;
        private string Schedule = "0 0 0 * * *";
        private Timer _timer = null!;
        private readonly IBus _bus;
        private readonly IWebHostEnvironment _env;
        private readonly ILogger<ProductHostedService> _logger;
        private readonly IMapper _mapper;
        private readonly RabbitMqSettings _rabbitMqSettings;
        private readonly IMemoryCache _cache;
        public ProductMemoryHostedService(ILogger<ProductHostedService> logger, IServiceProvider service, IWebHostEnvironment env, IMapper mapper, IOptions<RabbitMqSettings> rabbitMqSettings, IBus bus, IMemoryCache cache)
        {
            _env = env;
            if (_env.IsProduction())
            {
                Schedule = Environment.GetEnvironmentVariable("SCHEDULE_PRODUCTSYNC");
            }
            _crontabSchedule = CrontabSchedule.Parse(Schedule, new CrontabSchedule.ParseOptions { IncludingSeconds = true });
            _nextRun = _crontabSchedule.GetNextOccurrence(DateTime.Now);
            _logger = logger;
            _service = service;
            _mapper = mapper;
            _rabbitMqSettings = rabbitMqSettings.Value;
            _bus = bus;
            _cache = cache;
        }
        public void Dispose()
        {
            _timer?.Dispose();
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation($"Started ProductMemoryHostedService");
            Task.Run(async () =>
            {
                await DoWork();
                while (!cancellationToken.IsCancellationRequested)
                {
                    await Task.Delay(UntilNextExecution(), cancellationToken);
                    await DoWork();
                    _nextRun = _crontabSchedule.GetNextOccurrence(DateTime.Now);
                }
            }, cancellationToken);
            return Task.CompletedTask;
        }

        private async Task DoWork()
        {
            MemoryCacheEntryOptions cacheEntryOptions = new MemoryCacheEntryOptions().SetSlidingExpiration(TimeSpan.FromSeconds(10));
            using (var scope = _service.CreateScope())
            {
                var productService = scope.ServiceProvider.GetRequiredService<IProductRepositoryAsync>();
                var products = await productService.GetAllAsync();
                var productMemories = _mapper.Map<List<ProductMemory>>(products);
                await _cache.GetOrCreateAsync("ProductCache",entry =>
                {
                    entry.AbsoluteExpiration = DateTime.Now.AddYears(1);
                    return Task.FromResult(productMemories);
                });
            }
        }

        private int UntilNextExecution() => Math.Max(0, (int)_nextRun.Subtract(DateTime.Now).TotalMilliseconds);
        public Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Timed Hosted Service is stopping.");
            _timer?.Change(Timeout.Infinite, 0);
            return Task.CompletedTask;
        }
    }
}
