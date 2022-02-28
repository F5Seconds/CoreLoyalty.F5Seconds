using AutoMapper;
using CoreLoyalty.F5Seconds.Application.DTOs.F5seconds;
using CoreLoyalty.F5Seconds.Application.Features.GotIt.Queries.GetListVoucher;
using CoreLoyalty.F5Seconds.Application.Features.Urbox.Queries.GetListVoucher;
using CoreLoyalty.F5Seconds.Domain.Entities;
using CoreLoyalty.F5Seconds.Domain.Settings;
using MassTransit;
using MediatR;
using Microsoft.AspNetCore.Hosting;
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
    public class ProductHostedService : IHostedService, IDisposable
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
        public ProductHostedService(ILogger<ProductHostedService> logger, IServiceProvider service, IWebHostEnvironment env, IMapper mapper, IOptions<RabbitMqSettings> rabbitMqSettings, IBus bus)
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
        }
        public void Dispose()
        {
            _timer?.Dispose();
        }

        private async Task DoWork()
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
            using (var scope = _service.CreateScope())
            {
                var vouchers = new List<F5sVoucherBase>();
                var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();
                var gotIt = await mediator.Send(new GetListGotItVoucherQuery());
                var urbox = await mediator.Send(new GetListUrboxVoucherQuery());
                if (gotIt.Succeeded)
                {
                    vouchers.InsertRange(0, gotIt.Data);
                    vouchers.InsertRange(gotIt.Data.Count, urbox.Data);
                    var v = _mapper.Map<List<Product>>(vouchers);
                    Uri uri = new Uri($"rabbitmq://{rabbitHost}/{rabbitvHost}/{productSyncQueue}");
                    var endPoint = await _bus.GetSendEndpoint(uri);
                    foreach (var i in v)
                    {
                        await endPoint.Send(i);
                    }
                }
            }
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            Task.Run(async () =>
            {
                //await DoWork();
                while (!cancellationToken.IsCancellationRequested)
                {
                    await Task.Delay(UntilNextExecution(), cancellationToken);
                    await DoWork();
                    _nextRun = _crontabSchedule.GetNextOccurrence(DateTime.Now);
                }
            }, cancellationToken);
            return Task.CompletedTask;
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
