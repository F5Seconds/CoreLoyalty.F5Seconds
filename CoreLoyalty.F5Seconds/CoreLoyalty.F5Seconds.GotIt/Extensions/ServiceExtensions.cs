using CoreLoyalty.F5Seconds.GotIt.Interfaces;
using CoreLoyalty.F5Seconds.GotIt.Repositories;
using CoreLoyalty.F5Seconds.Infrastructure.Shared.RabbitMq.Consumer;
using CoreLoyalty.F5Seconds.Shared.RabbitMq.Consumer;
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
        
        public static void AddRabbitMqExtension(this IServiceCollection services, IConfiguration configuration, IWebHostEnvironment env)
        {
            string rabbitHost = configuration["RabbitMqSettings:Host"];
            string rabbitvHost = configuration["RabbitMqSettings:vHost"];
            string rabbitUser = configuration["RabbitMqSettings:Username"];
            string rabbitPass = configuration["RabbitMqSettings:Password"];
            string rabbitTransReqQueue = configuration["RabbitMqSettings:transactionRequestQueue"];
            string rabbitTransResQueue = configuration["RabbitMqSettings:transactionResponseQueue"];
            string rabbitTransResFailQueue = configuration["RabbitMqSettings:rabbitTransResFailQueue"];

            if (env.IsProduction())
            {
                rabbitHost = Environment.GetEnvironmentVariable("RABBITMQ_HOST");
                rabbitvHost = Environment.GetEnvironmentVariable("RABBITMQ_VHOST");
                rabbitUser = Environment.GetEnvironmentVariable("RABBITMQ_USER");
                rabbitPass = Environment.GetEnvironmentVariable("RABBITMQ_PASS");
                rabbitTransReqQueue = Environment.GetEnvironmentVariable("RABBITMQ_TRANS_REQUEST");
                rabbitTransResQueue = Environment.GetEnvironmentVariable("RABBITMQ_TRANS_RES_SUCCESS");
                rabbitTransResFailQueue = Environment.GetEnvironmentVariable("RABBITMQ_TRANS_RES_FAIL");
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
