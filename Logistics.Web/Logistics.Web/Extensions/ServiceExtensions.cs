using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RequestHandler.Interfaces;
using RequestHandler.ModelHandlers;

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
                    config.SaveTokens = true;
                    config.ResponseType = "code";
                    config.GetClaimsFromUserInfoEndpoint = true;
                });
        }

        public static void ConfigureActionAttributes(this IServiceCollection services)
        {
        }
    }
}
