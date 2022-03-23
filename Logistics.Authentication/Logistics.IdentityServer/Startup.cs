using Logistics.IdentityServer.Entities;
using Logistics.IdentityServer.Entities.Models;
using Logistics.IdentityServer.Extensions;
using Logistics.IdentityServer.Services;
using Logistics.IdentityServer.Services.Interfaces;
using Logistics.IdentityServer.Services.Services;
using Logistics.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace IdentityServer
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
            services.AddAutoMapper(typeof(MappingProfile));

            services.AddDbContext<AuthenticationDbContext>(options =>
                options.UseSqlServer(_configuration.GetConnectionString("sqlConnection")));

            services.ConfigureSwagger();

            services.AddIdentity<User, IdentityRole>(config =>
            {

                config.Password.RequireNonAlphanumeric = false;
                config.Password.RequireDigit = false;
                config.Password.RequireUppercase = false;

            }).AddEntityFrameworkStores<AuthenticationDbContext>()
            .AddDefaultTokenProviders();

            services.AddIdentityServer()
                .AddAspNetIdentity<User>()
                .AddInMemoryIdentityResources(IdentityConfiguration.IdentityResources)
                .AddInMemoryClients(IdentityConfiguration.Clients)
                .AddInMemoryApiScopes(IdentityConfiguration.ApiScopes)
                .AddInMemoryApiResources(IdentityConfiguration.ApiResources)
                .AddDeveloperSigningCredential();

            services.ConfigureApplicationCookie(config =>
            {
                config.Cookie.Name = "Logistics.Identity.Cookie";
                config.LoginPath = "/Auth/Login";
                config.LogoutPath = "/Auth/Logout";
            });

            services.AddScoped<IAuthenticationManager, AuthenticationManager>();
            services.AddScoped<IAuthenticationService, AuthenticationService>();
            services.AddControllers();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
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

            app.UseRouting();

            app.UseIdentityServer();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
