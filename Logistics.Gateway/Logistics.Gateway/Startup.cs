using Logistics.API.Extensions;
using Logistics.API.Middleware;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NLog;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;
using System.IO;

namespace Logistics
{
    public class Startup
    {
        private readonly IConfiguration _configuration;

        public Startup(IConfiguration configuration)
        {
            this._configuration = configuration;

            LogManager.LoadConfiguration(GetNlogConfigPath());
        }

        private string GetNlogConfigPath()
            => Directory.GetCurrentDirectory() + "/nLog.config";

        public void ConfigureServices(IServiceCollection services)
        {   
            services.ConfigureCors();
            services.Configure<IISOptions>(options =>
            { });

            services.AddOcelot();
           
            services.ConfigureAuthentication(_configuration);

            
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseMiddleware<ExceptionHandler>();
            }
            else
            {
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseCors("CorsPolicy");

            app.UseForwardedHeaders(new ForwardedHeadersOptions
            {
                ForwardedHeaders = ForwardedHeaders.All
            });

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseOcelot().Wait();
        }
    }
}
