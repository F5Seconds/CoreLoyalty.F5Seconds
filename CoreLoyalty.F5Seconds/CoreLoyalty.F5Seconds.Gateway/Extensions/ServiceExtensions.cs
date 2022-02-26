using CoreLoyalty.F5Seconds.Application.Interfaces.GotIt;
using CoreLoyalty.F5Seconds.Application.Interfaces.Urbox;
using CoreLoyalty.F5Seconds.Infrastructure.Persistence.Repositories.GotIt;
using CoreLoyalty.F5Seconds.Infrastructure.Persistence.Repositories.Urbox;
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

namespace CoreLoyalty.F5Seconds.Gateway.Extensions
{
    public static class ServiceExtensions
    {
        public static void AddHttpClientExtension(this IServiceCollection services)
        {
            services.AddHttpClient<IGotItHttpClientService, GotItHttpClientRepository>(c => { 
                c.BaseAddress = new Uri("https://localhost:5001"); 
            });
            services.AddHttpClient<IUrboxHttpClientService, IUboxHttpClientRepository>(c => { 
                c.BaseAddress = new Uri("https://localhost:5004"); 
            });
        }

        public static void AddSwaggerExtension(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.IncludeXmlComments(string.Format(@"CoreLoyalty.F5Seconds.Gateway.xml"));
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "Clean Architecture - CoreLoyalty.F5Seconds.WebApi",
                    Description = "This Api will be responsible for overall data distribution and authorization.",
                    Contact = new OpenApiContact
                    {
                        Name = "codewithmukesh",
                        Email = "hello@codewithmukesh.com",
                        Url = new Uri("https://codewithmukesh.com/contact"),
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
            string rabbitEmployeeSync = configuration["RabbitMqSettings:EmployeesSyncQueue"];
            string rabbitWorkPlaceSync = configuration["RabbitMqSettings:WorkPlaceSyncQueue"];
            string rabbitReportMail = configuration["RabbitMqSettings:ReportsMailQueue"];

            if (env.IsProduction())
            {
                rabbitHost = Environment.GetEnvironmentVariable("RABBITMQ_HOST");
                rabbitvHost = Environment.GetEnvironmentVariable("RABBITMQ_VHOST");
                rabbitUser = Environment.GetEnvironmentVariable("RABBITMQ_USER");
                rabbitPass = Environment.GetEnvironmentVariable("RABBITMQ_PASS");
                rabbitEmployeeSync = Environment.GetEnvironmentVariable("RABBITMQ_EMPLOYEESSYNC");
                rabbitWorkPlaceSync = Environment.GetEnvironmentVariable("RABBITMQ_WORKPLACESYNC");
                rabbitReportMail = Environment.GetEnvironmentVariable("RABBITMQ_REPORT_MAIL");
            }

            services.AddMassTransit(x =>
            {
                x.AddConsumer<ProductConsumer>();
                x.AddBus(provider => Bus.Factory.CreateUsingRabbitMq(config =>
                {
                    config.Host(rabbitHost, rabbitvHost, h =>
                    {
                        h.Username(rabbitUser);
                        h.Password(rabbitPass);
                    });
                    config.ReceiveEndpoint(rabbitEmployeeSync, ep =>
                    {
                        ep.PrefetchCount = 16;
                        ep.UseMessageRetry(r => r.Interval(2, 100));
                        ep.ConfigureConsumer<ProductConsumer>(provider);
                    });
                }));
            });
            services.AddMassTransitHostedService();
        }
    }
}
