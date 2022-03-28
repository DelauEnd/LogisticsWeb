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
        }

        public static void ConfigureAuthentication(this IServiceCollection services, Microsoft.Extensions.Configuration.IConfiguration _configuration)
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
                    config.SaveTokens = false;
                    config.ResponseType = "code";
                });
        }

        public static void ConfigureActionAttributes(this IServiceCollection services)
        {

        }
    }
}
