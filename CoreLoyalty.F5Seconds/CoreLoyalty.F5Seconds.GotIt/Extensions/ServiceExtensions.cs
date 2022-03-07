using CoreLoyalty.F5Seconds.GotIt.Consumer;
using CoreLoyalty.F5Seconds.GotIt.HostedService;
using CoreLoyalty.F5Seconds.GotIt.Interfaces;
using CoreLoyalty.F5Seconds.GotIt.Repositories;
using CoreLoyalty.F5Seconds.Infrastructure.Shared.Const;
using CoreLoyalty.F5Seconds.Infrastructure.Shared.RabbitMq.Consumer;
using GreenPipes;
using MassTransit;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;

namespace CoreLoyalty.F5Seconds.GotIt.Extensions
{
    public static class ServiceExtensions
    {
        public static void AddHttpClientExtension(this IServiceCollection services, IConfiguration configuration, IWebHostEnvironment env)
        {
            string gotItUri = configuration["GotIt:Uri"];
            string token = configuration["GotIt:Token"];
            if (env.IsProduction())
            {
                gotItUri = Environment.GetEnvironmentVariable("GOTIT_URI");
                token = Environment.GetEnvironmentVariable("GOTIT_TOKEN");
            }
            services.AddHttpClient<IGotItHttpClientService, GotItHttpClientRepository>(c => {
                c.BaseAddress = new Uri(gotItUri);
                c.DefaultRequestHeaders.Add("X-GI-Authorization", token);
            });
        }
        public static void AddApiVersioningExtension(this IServiceCollection services)
        {
            services.AddApiVersioning(config =>
            {
                // Specify the default API Version as 1.0
                config.DefaultApiVersion = new ApiVersion(1, 0);
                // If the client hasn't specified the API version in the request, use the default API version number 
                config.AssumeDefaultVersionWhenUnspecified = true;
                // Advertise the API versions supported for the particular endpoint
                config.ReportApiVersions = true;
            });
        }

        public static void AddHostedService(this IServiceCollection services)
        {
            //services.AddHostedService<TransCheckHostedService>();
        }

        public static void AddRabbitMqExtension(this IServiceCollection services, IConfiguration configuration, IWebHostEnvironment env)
        {
            string rabbitHost = configuration[RabbitMqAppSettingConst.Host];
            string rabbitvHost = configuration[RabbitMqAppSettingConst.Vhost];
            string rabbitUser = configuration[RabbitMqAppSettingConst.User];
            string rabbitPass = configuration[RabbitMqAppSettingConst.Pass];
            string rabbitTransReqQueue = configuration[RabbitMqAppSettingConst.TransRequest];
            string rabbitTransResQueue = configuration[RabbitMqAppSettingConst.TransResSuccess];
            string rabbitTransResFailQueue = configuration[RabbitMqAppSettingConst.TransResFail];
            string voucherNotUse = configuration[RabbitMqAppSettingConst.VoucherNotUsed];
            string voucherUpdateStatus = configuration[RabbitMqAppSettingConst.VoucherUpdateStatus];
            if (env.IsProduction())
            {
                rabbitHost = Environment.GetEnvironmentVariable(RabbitMqEnvConst.Host);
                rabbitvHost = Environment.GetEnvironmentVariable(RabbitMqEnvConst.Vhost);
                rabbitUser = Environment.GetEnvironmentVariable(RabbitMqEnvConst.User);
                rabbitPass = Environment.GetEnvironmentVariable(RabbitMqEnvConst.Pass);
                rabbitTransReqQueue = Environment.GetEnvironmentVariable(RabbitMqEnvConst.TransRequest);
                rabbitTransResQueue = Environment.GetEnvironmentVariable(RabbitMqEnvConst.TransResSuccess);
                rabbitTransResFailQueue = Environment.GetEnvironmentVariable(RabbitMqEnvConst.TransResFail);
                voucherNotUse = Environment.GetEnvironmentVariable(RabbitMqEnvConst.VoucherNotUsed);
                voucherUpdateStatus = Environment.GetEnvironmentVariable(RabbitMqEnvConst.VoucherUpdateStatus);
            }

            services.AddMassTransit(x =>
            {
                x.AddConsumer<GotItTransactionReqConsumer>();
                x.AddConsumer<GotItTransactionResSuccessConsumer>();
                x.AddConsumer<GotItTransactionResFailConsumer>();
                x.AddConsumer<GotItVoucherNotUsedConsumer>();
                x.AddConsumer<GotItVoucherUpdateStatusConumer>();
                x.AddBus(provider => Bus.Factory.CreateUsingRabbitMq(config =>
                {
                    config.Host(rabbitHost, rabbitvHost, h =>
                    {
                        h.Username(rabbitUser);
                        h.Password(rabbitPass);
                    });
                    config.ReceiveEndpoint(rabbitTransReqQueue, ep =>
                    {
                        ep.PrefetchCount = 16;
                        ep.UseMessageRetry(r => r.Interval(2, 100));
                        ep.ConfigureConsumer<GotItTransactionReqConsumer>(provider);
                    });
                    config.ReceiveEndpoint(rabbitTransResQueue, ep =>
                    {
                        ep.PrefetchCount = 16;
                        ep.UseMessageRetry(r => r.Interval(2, 100));
                        ep.ConfigureConsumer<GotItTransactionResSuccessConsumer>(provider);
                    });
                    config.ReceiveEndpoint(rabbitTransResFailQueue, ep =>
                    {
                        ep.PrefetchCount = 16;
                        ep.UseMessageRetry(r => r.Interval(2, 100));
                        ep.ConfigureConsumer<GotItTransactionResFailConsumer>(provider);
                    });
                    config.ReceiveEndpoint(voucherNotUse, ep =>
                    {
                        ep.PrefetchCount = 16;
                        ep.UseMessageRetry(r => r.Interval(2, 100));
                        ep.ConfigureConsumer<GotItVoucherNotUsedConsumer>(provider);
                    });
                    config.ReceiveEndpoint(voucherUpdateStatus, ep =>
                    {
                        ep.PrefetchCount = 16;
                        ep.UseMessageRetry(r => r.Interval(2, 100));
                        ep.ConfigureConsumer<GotItVoucherUpdateStatusConumer>(provider);
                    });
                }));
            });
            services.AddMassTransitHostedService();
        }

        public static void AddSwaggerExtension(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.IncludeXmlComments(string.Format(@"CoreLoyalty.F5Seconds.GotIt.xml"));
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "Clean Architecture - CleanArchitecture.WebApi.WebApi",
                    Description = "This Api will be responsible for overall data distribution and authorization.",
                    Contact = new OpenApiContact
                    {
                        Name = "toanla",
                        Email = "toanle@f5seconds.vn",
                        Url = new Uri("https://f5seconds.vn"),
                    }
                });
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    Description = "Input your Bearer token in this format - Bearer {your token here} to access this API",
                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer",
                            },
                            Scheme = "Bearer",
                            Name = "Bearer",
                            In = ParameterLocation.Header,
                        }, new List<string>()
                    },
                });
            });
        }
    }
}
