﻿using Logistics.OrderService.Services.Interfaces;
using Logistics.OrderService.Services.Services;
using MassTransit;
using MassTransit.Definition;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace Logistics.OrderService.Extensions
{
    public static class ServiceExtensions
    {
        public static void ConfigureCors(this IServiceCollection services)
            => services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy", builder =>
                    builder.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader());
            });

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

        public static void ConfigureFormatters(this IMvcBuilder builder)
            => builder
            .AddNewtonsoftJson(options =>
            {
                options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            })
            .AddXmlDataContractSerializerFormatters();


        public static void ConfigureServices(this IServiceCollection services)
        {
            services.AddScoped<ICargoCategoryService, CargoCategoryService>();
            services.AddScoped<ICargoService, CargoService>();
            services.AddScoped<ICustomerService, CustomerService>();
            services.AddScoped<IOrderService, OrderrService>();
        }

        public static void ConfigureSwagger(this IServiceCollection services)
            => services.AddSwaggerGen(setup =>
            {
                setup.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Cargo Transportation Api",
                    Version = "v1"
                });

                setup.SwaggerDoc("v2", new OpenApiInfo
                {
                    Title = "Cargo Transportation Api",
                    Version = "v2"
                });

                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                setup.IncludeXmlComments(xmlPath);

                setup.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Description = "Place to add JWT with Bearer",
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer"
                });

                setup.AddSecurityRequirement(new OpenApiSecurityRequirement()
                {

                    {
                        new OpenApiSecurityScheme
                        { Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            },
                            Name = "Bearer",
                        },
                        new List<string>()
                    }
                });
            });

        public static void ConfigureMassTransit(this IServiceCollection services, IConfiguration configuration)
        {
            var massTransitSection = configuration.GetSection("MassTransit");
            var url = massTransitSection.GetValue<string>("Url");
            var host = massTransitSection.GetValue<string>("Host");
            var userName = massTransitSection.GetValue<string>("UserName");
            var password = massTransitSection.GetValue<string>("Password");
            if (massTransitSection == null || url == null || host == null)
            {
                throw new Exception("Section 'MassTransit' configuration settings are not found in appSettings.json");
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
    }
}
