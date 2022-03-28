using Logistics.API.Extensions;
using Logistics.API.Middleware;
using Logistics.Entities;
using Logistics.Repository;
using Logistics.Repository.Interfaces;
using Logistics.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NLog;
using System.IO;

namespace Logistics
{
    public class Startup
    {
        private readonly IConfiguration configuration;

        public Startup(IConfiguration configuration)
        {
            this.configuration = configuration;

            LogManager.LoadConfiguration(GetNlogConfigPath());
        }

        private string GetNlogConfigPath()
            => Directory.GetCurrentDirectory() + "/nLog.config";

        public void ConfigureServices(IServiceCollection services)
        {
            services.ConfigureCors();
            services.Configure<IISOptions>(options =>
            { });

            services.AddDbContext<LogisticsDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("sqlConnection")));

            services.AddScoped<IRepositoryManager, RepositoryManager>();
            services.ConfigureServices();
            services.AddAutoMapper(typeof(MappingProfile));

            services.ConfigureVersioning();
            services.ConfigureSwagger();

            services.ConfigureJWT(configuration);

            services.AddControllers(config =>
            {
                config.RespectBrowserAcceptHeader = true;
                config.ReturnHttpNotAcceptable = true;
            }).ConfigureFormatters();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
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

            app.UseSwagger();
            app.UseSwaggerUI(setup =>
            {
                setup.SwaggerEndpoint("/swagger/v1/swagger.json", "Cargo Transportation Api v1");
                setup.SwaggerEndpoint("/swagger/v2/swagger.json", "Cargo Transportation Api v2");

                setup.RoutePrefix = "";
            });

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
