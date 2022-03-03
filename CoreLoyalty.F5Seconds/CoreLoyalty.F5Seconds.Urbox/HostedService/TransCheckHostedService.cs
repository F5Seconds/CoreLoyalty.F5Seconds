using AutoMapper;
using CoreLoyalty.F5Seconds.Application.Interfaces.Urbox.Repositories;
using CoreLoyalty.F5Seconds.Infrastructure.Shared.Const;
using MassTransit;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using NCrontab;
using Newtonsoft.Json;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace CoreLoyalty.F5Seconds.Urbox.HostedService
{
    public class TransCheckHostedService : IHostedService, IDisposable
    {
        private readonly CrontabSchedule _crontabSchedule;
        private readonly IServiceProvider _service;
        private DateTime _nextRun;
        private string Schedule = "*/3 * * * * *";
        private Timer _timer = null!;
        private readonly IBus _bus;
        private readonly IWebHostEnvironment _env;
        private readonly ILogger<TransCheckHostedService> _logger;
        private readonly IMapper _mapper;
        IConfiguration _config;
        public TransCheckHostedService(
            ILogger<TransCheckHostedService> logger, 
            IServiceProvider service, 
            IWebHostEnvironment env, 
            IMapper mapper, 
            IConfiguration config,
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
            _bus = bus;
            _config = config;
        }
        private async Task DoWork()
        {
            string rabbitHost = _config[RabbitMqAppSettingConst.Host];
            string rabbitvHost = _config[RabbitMqAppSettingConst.Vhost];
            string voucherNotUse = _config[RabbitMqAppSettingConst.VoucherNotUsed];
            if (_env.IsProduction())
            {
                rabbitHost = Environment.GetEnvironmentVariable(RabbitMqEnvConst.Host);
                rabbitvHost = Environment.GetEnvironmentVariable(RabbitMqEnvConst.Vhost);
                voucherNotUse = Environment.GetEnvironmentVariable(RabbitMqEnvConst.VoucherNotUsed);
            }
            using (var scope = _service.CreateScope())
            {
                var _gotItTransResponse = scope.ServiceProvider.GetRequiredService<IUrboxTransResSuccessRepositoryAsync>();
                var voucherNotUsed = await _gotItTransResponse.ListVoucherNotUsed();
                
                if(voucherNotUsed is not null && voucherNotUsed.Count > 0)
                {
                    Uri uri = new Uri($"rabbitmq://{rabbitHost}/{rabbitvHost}/{voucherNotUse}");
                    var endPoint = await _bus.GetSendEndpoint(uri);
                    foreach (var i in voucherNotUsed)
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

        public void Dispose()
        {
            _timer?.Dispose();
        }
    }
}
