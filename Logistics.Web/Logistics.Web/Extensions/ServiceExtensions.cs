using MassTransit;
using MassTransit.Definition;
using MassTransit.MultiBus;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RequestHandler.Interfaces;
using RequestHandler.ModelHandlers;
using System;

namespace CargoTransportation.Extensions
{
    public static class ServiceExtensions
    {
        public static void ConfigureRequestManagers(this IServiceCollection services)
        {
            services.AddSingleton<ICargoCategoriesRequestHandler, CargoCategoriesRequestHandler>();
            services.AddSingleton<ICargoRequestHandler, CargoRequestHandler>();
            services.AddSingleton<IOrderRequestHandler, OrderRequestHandler>();
            services.AddSingleton<ITransportRequestHandler, TransportRequestHandler>();
            services.AddSingleton<ICustomerRequestHandler, CustomerRequestHandler>();
            services.AddSingleton<IRouteRequestHandler, RouteRequestHandler>();
        }

        public static void ConfigureMassTransit(this IServiceCollection services, IConfiguration configuration)
        {
            var massTransitSection = configuration.GetSection("MassTransit");
            var url = massTransitSection.GetValue<string>("Url");
            var host = massTransitSection.GetValue<string>("Host");
            var userName = massTransitSection.GetValue<string>("UserName");
            var password = massTransitSection.GetValue<string>("Password");
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
            });

            services.AddMassTransitHostedService();
        }

        public static void ConfigureAuthentication(this IServiceCollection services, IConfiguration _configuration)
        {
            services.AddAuthentication(config =>
            {
                config.DefaultScheme = "Cookie";
                config.DefaultChallengeScheme = "oidc";
            })
                .AddCookie("Cookie")
                .AddOpenIdConnect("oidc", config =>
                {
                    config.Authority = _configuration.GetSection("IdentityServerBaseUrl").Value;
                    config.ClientId = "MVCClient";
                    config.ClientSecret = "MVC_super_secert";

                    config.ResponseType = "code";
                    config.SaveTokens = true;

                    config.GetClaimsFromUserInfoEndpoint = true;

                    config.Scope.Clear();
                    config.Scope.Add("openid");
                    config.Scope.Add("profile");
                    config.Scope.Add("roles");

                    config.ClaimActions.MapJsonKey("role","role","role");
                    config.TokenValidationParameters.RoleClaimType = "role";
                });
        }

        public static void ConfigureActionAttributes(this IServiceCollection services)
        {
        }
    }
}
