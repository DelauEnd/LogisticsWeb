using Logistics.Entities;
using Logistics.OrderService.Extensions;
using Logistics.OrderService.Middleware;
using Logistics.OrderService.Repository;
using Logistics.OrderService.Repository.Interfaces;
using Logistics.OrderService.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Logistics.OrderService
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
            services.ConfigureCors();
            services.Configure<IISOptions>(options =>
            { });

            services.AddDbContext<LogisticsDbContext>(options =>
                options.UseSqlServer(_configuration.GetConnectionString("sqlConnection")));

            services.ConfigureMassTransit(_configuration);
            services.AddScoped<IRepositoryManager, RepositoryManager>();
            services.ConfigureServices();
            services.AddAutoMapper(typeof(MappingProfile));

            services.ConfigureAuthentication(_configuration);

            services.AddControllers(config =>
            {
                config.RespectBrowserAcceptHeader = true;
                config.ReturnHttpNotAcceptable = true;
            }).ConfigureFormatters();
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
        }
    }
}
