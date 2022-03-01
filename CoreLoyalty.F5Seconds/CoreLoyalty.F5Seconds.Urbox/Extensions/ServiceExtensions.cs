using CoreLoyalty.F5Seconds.Application.Enums;
using CoreLoyalty.F5Seconds.Infrastructure.Shared.RabbitMq.Consumer;
using CoreLoyalty.F5Seconds.Urbox.Interfaces;
using CoreLoyalty.F5Seconds.Urbox.Repositories;
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

namespace CoreLoyalty.F5Seconds.Urbox.Extensions
{
    public static class ServiceExtensions
    {
        public static void AddHttpClientExtension(this IServiceCollection services, IConfiguration configuration, IWebHostEnvironment env)
        {
            string uri = configuration["Urbox:Uri"];
            string appId = configuration["Urbox:AppId"];
            string appSecret = configuration["Urbox:AppSecret"];
            if (env.IsProduction())
            {
                uri = Environment.GetEnvironmentVariable("URBOX_URI");
                appId = Environment.GetEnvironmentVariable("URBOX_APPID");
                appId = Environment.GetEnvironmentVariable("URBOX_APPID");
            }
            services.AddHttpClient<IUrboxHttpClientService, UrboxHttpClientRepository>(c => {
                c.BaseAddress = new Uri($"{uri}");
            });
        }

        public static void AddSwaggerExtension(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.IncludeXmlComments(string.Format(@"CoreLoyalty.F5Seconds.Urbox.xml"));
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

        public static void AddRabbitMqExtension(this IServiceCollection services, IConfiguration configuration, IWebHostEnvironment env)
        {
            string rabbitHost = configuration["RabbitMqSettings:Host"];
            string rabbitvHost = configuration["RabbitMqSettings:vHost"];
            string rabbitUser = configuration["RabbitMqSettings:Username"];
            string rabbitPass = configuration["RabbitMqSettings:Password"];
            string rabbitTransReqQueue = configuration["RabbitMqSettings:transactionReqQueue"];
            string rabbitTransResQueue = configuration["RabbitMqSettings:transactionResQueue"];
            string rabbitTransResFailQueue = configuration["RabbitMqSettings:transactionResFailQueue"];

            if (env.IsProduction())
            {
                rabbitHost = Environment.GetEnvironmentVariable(RabbitMqs.RABBITMQ_HOST.ToString());
                rabbitvHost = Environment.GetEnvironmentVariable(RabbitMqs.RABBITMQ_VHOST.ToString());
                rabbitUser = Environment.GetEnvironmentVariable(RabbitMqs.RABBITMQ_USER.ToString());
                rabbitPass = Environment.GetEnvironmentVariable(RabbitMqs.RABBITMQ_PASS.ToString());
                rabbitTransReqQueue = Environment.GetEnvironmentVariable(RabbitMqs.RABBITMQ_REQ_QUEUE.ToString());
                rabbitTransResQueue = Environment.GetEnvironmentVariable(RabbitMqs.RABBITMQ_RES_SUCCESS_QUEUE.ToString());
                rabbitTransResFailQueue = Environment.GetEnvironmentVariable(RabbitMqs.RABBITMQ_RES_FAIL_QUEUE.ToString());
            }

            services.AddMassTransit(x =>
            {
                x.AddConsumer<TransactionRequestConsumer>();
                x.AddConsumer<TransactionResponseConsumer>();
                x.AddConsumer<TransactionResponseFailConsumer>();
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
                        ep.ConfigureConsumer<TransactionRequestConsumer>(provider);
                    });
                    config.ReceiveEndpoint(rabbitTransResQueue, ep =>
                    {
                        ep.PrefetchCount = 16;
                        ep.UseMessageRetry(r => r.Interval(2, 100));
                        ep.ConfigureConsumer<TransactionResponseConsumer>(provider);
                    });
                    config.ReceiveEndpoint(rabbitTransResFailQueue, ep =>
                    {
                        ep.PrefetchCount = 16;
                        ep.UseMessageRetry(r => r.Interval(2, 100));
                        ep.ConfigureConsumer<TransactionResponseFailConsumer>(provider);
                    });
                }));
            });
            services.AddMassTransitHostedService();
        }
    }
}
