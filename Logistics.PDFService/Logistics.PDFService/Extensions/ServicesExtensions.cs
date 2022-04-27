using Logistics.PdfService.MassTransit;
using MassTransit;
using MassTransit.Definition;
using MassTransit.MultiBus;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Logistics.PdfService.Extensions
{
    public static class ServicesExtensions
    {
        public static void ConfigureAuthentication(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAuthentication(opt =>
            {
                opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })

            .AddJwtBearer("Bearer", options =>
            {
                options.Authority = configuration.GetSection("IdentityServerBaseUrl").Value;
                options.Audience = "Logistics.API";
            });
        }

        public static void ConfigureMassTransit(this IServiceCollection services, IConfiguration configuration)
        {
            var massTransitSection = configuration.GetSection("MassTransit");
            var url = massTransitSection.GetValue<string>("Url");
            var host = massTransitSection.GetValue<string>("Host");
            var userName = massTransitSection.GetValue<string>("UserName");
            var password = massTransitSection.GetValue<string>("Password");
            var queueName = massTransitSection.GetValue<string>("QueueName");

            if (massTransitSection == null || url == null || host == null)
            {
                throw new Exception("Section 'mass-transit' configuration settings are not found in appSettings.json");
            }

            services.AddMassTransit(x =>
            {
                x.AddBus(busFactory =>
                {
                    var bus = Bus.Factory.CreateUsingRabbitMq(cfg =>
                    {
                        cfg.Host($"rabbitmq://{url}/{host}", configurator =>
                        {
                            configurator.Username(userName);
                            configurator.Password(password);
                        });

                        cfg.ClearMessageDeserializers();
                        cfg.UseRawJsonSerializer();
                        cfg.ConfigureEndpoints(busFactory, SnakeCaseEndpointNameFormatter.Instance);
                    });

                    return bus;
                });

                x.AddConsumer<AppOrderCreatedConsumer>();
                x.AddConsumer<AppOrderUpdatedConsumer>();
            });

            services.AddMassTransitHostedService();
        }
    }
}
