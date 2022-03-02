using AutoMapper;
using CoreLoyalty.F5Seconds.Application.Interfaces.Repositories;
using CoreLoyalty.F5Seconds.Domain.Settings;
using CoreLoyalty.F5Seconds.GotIt.Interfaces;
using MassTransit;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using NCrontab;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace CoreLoyalty.F5Seconds.GotIt.HostedService
{
    public class TransCheckHostedService : IHostedService, IDisposable
    {
        private readonly CrontabSchedule _crontabSchedule;
        private readonly IServiceProvider _service;
        private DateTime _nextRun;
        private string Schedule = "0 0 0 * * *";
        private Timer _timer = null!;
        private readonly IBus _bus;
        private readonly IWebHostEnvironment _env;
        private readonly ILogger<TransCheckHostedService> _logger;
        private readonly IMapper _mapper;
        private readonly RabbitMqSettings _rabbitMqSettings;
        public TransCheckHostedService(
            ILogger<TransCheckHostedService> logger, 
            IServiceProvider service, 
            IWebHostEnvironment env, 
            IMapper mapper, 
            IOptions<RabbitMqSettings> rabbitMqSettings, 
            IBus bus)
        {
            _env = env;
            if (_env.IsProduction())
            {
                Schedule = Environment.GetEnvironmentVariable("SCHEDULE_TRANSCHECK");
            }
            _crontabSchedule = CrontabSchedule.Parse(Schedule, new CrontabSchedule.ParseOptions { IncludingSeconds = true });
            _nextRun = _crontabSchedule.GetNextOccurrence(DateTime.Now);
            _logger = logger;
            _service = service;
            _mapper = mapper;
            _rabbitMqSettings = rabbitMqSettings.Value;
            _bus = bus;
        }
        private async Task DoWork()
        {
            using (var scope = _service.CreateScope())
            {
                var productRepository = scope.ServiceProvider.GetRequiredService<IProductRepositoryAsync>();
                var products = await productRepository.w
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
