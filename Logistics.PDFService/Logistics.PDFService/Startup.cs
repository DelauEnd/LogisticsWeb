using Logistics.PdfService.Extensions;
using Logistics.PdfService.Repositories.Interfaces;
using Logistics.PdfService.Repositories.Repositories;
using Logistics.PdfService.Services.Interfaces;
using Logistics.PdfService.Services.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Npgsql;
using System.Data;

namespace Logistics.PdfService
{
    public class Startup
    {
        private readonly IConfiguration _configuration;

        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.ConfigureAuthentication(_configuration);

            services.AddTransient<IDbConnection>(_ => new NpgsqlConnection(_configuration.GetSection("npgsqlConnection").Value));
            services.AddScoped<IPdfLogService, PdfLogService>();
            services.AddScoped<IOrderPdfLogRepository, OrderPdfLogRepository>();
            services.AddScoped<IOrderPdfRepository, OrderPdfRepository>();
            services.AddScoped<IOrderPdfBuilder, OrderPdfBuilder>();
            services.ConfigureMassTransit(_configuration);

            services.AddControllers(config =>
            {
                config.RespectBrowserAcceptHeader = true;
                config.ReturnHttpNotAcceptable = true;
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
