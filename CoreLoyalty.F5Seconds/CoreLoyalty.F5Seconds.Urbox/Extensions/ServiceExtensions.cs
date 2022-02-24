using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Ocelot.Configuration.File;
using Ocelot.DependencyInjection;
using System;
using System.Collections.Generic;

namespace CoreLoyalty.F5Seconds.Urbox.Extensions
{
    public static class ServiceExtensions
    {
        public static void AddOcelotExtension(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddOcelot();
            //https://stackoverflow.com/questions/63421563/environment-variable-in-ocelot-config-file
            //services.ConfigureDownstreamHostAndPortsPlaceholders(configuration);
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

        public static IServiceCollection ConfigureDownstreamHostAndPortsPlaceholders(this IServiceCollection services,IConfiguration configuration)
        {
            services.PostConfigure<FileConfiguration>(fileConfiguration =>
            {
                var globalHosts = configuration
                    .GetSection($"{nameof(FileConfiguration.GlobalConfiguration)}:Hosts")
                    .Get<GlobalHosts>();

                foreach (var route in fileConfiguration.Routes)
                {
                    ConfigureRote(route, globalHosts);
                }
            });

            return services;
        }

        private static void ConfigureRote(FileRoute route, GlobalHosts globalHosts)
        {
            foreach (var hostAndPort in route.DownstreamHostAndPorts)
            {
                var host = hostAndPort.Host;
                if (host.StartsWith("{") && host.EndsWith("}"))
                {
                    var placeHolder = host.TrimStart('{').TrimEnd('}');
                    if (globalHosts.TryGetValue(placeHolder, out var uri))
                    {
                        route.DownstreamScheme = uri.Scheme;
                        hostAndPort.Host = uri.Host;
                        hostAndPort.Port = uri.Port;
                    }
                }
            }
        }
    }
    public class GlobalHosts : Dictionary<string, Uri> { }
}
