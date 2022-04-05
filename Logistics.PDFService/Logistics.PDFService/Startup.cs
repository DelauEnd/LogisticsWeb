using Logistics.PdfService.Services.Interfaces;
using Logistics.PdfService.Extensions;
using Logistics.PdfService.Repositories;
using Logistics.PdfService.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

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
            services.AddSingleton<PdfLogsContext>();
            services.AddScoped<IPdfLogRepository, PdfLogRepository>();
            services.AddScoped<IOrderPdfRepository, OrderPdfRepository>();
            services.AddScoped<IOrderPdfBuilder, OrderPdfBuilder>();
            services.ConfigureMassTransit(_configuration);
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGet("/", async context =>
                {
                    await context.Response.WriteAsync("Hello World!");
                });
            });
        }
    }
}
