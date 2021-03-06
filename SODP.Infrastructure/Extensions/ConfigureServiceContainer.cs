using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;
using SODP.DataAccess;
using SODP.Domain.Managers;
using SODP.Infrastructure.Managers;
using System;

namespace SODP.Infrastructure.Extensions
{
    public static class ConfigureServiceContainer
    {
        public static void AddSwagger(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "SODP.API",
                    Version = "v1",
                    Description = "Aplikacja do zarządzania projektami SODP",
                    TermsOfService = new Uri(configuration.GetSection($"AppSettings:termsOfService").Value),
                    Contact = new OpenApiContact
                    {
                        Name = configuration.GetSection($"AppSettings:contactName").Value,
                        Email = configuration.GetSection($"AppSettings:contactEmail").Value,
                        Url = new Uri(configuration.GetSection($"AppSettings:contactUrl").Value)
                    },
                    License = new OpenApiLicense
                    {
                        Name = "Used License",
                        Url = new Uri(configuration.GetSection($"AppSettings:licenseUrl").Value)
                    }
                });

                //var filePath = Path.Combine(AppContext.BaseDirectory, "SODP.UI.xml");
                //c.IncludeXmlComments(filePath);

                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Description = "Please insert JWT with Bearer into field",
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        Array.Empty<string>()
                    }
                });
            });
        }

        public static void AddDbContext(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<SODPDBContext>(options =>
            {
                options.EnableDetailedErrors();
                options.UseMySql(
                    configuration.GetConnectionString("DefaultDbConnection"),
                    b => b.CharSetBehavior(CharSetBehavior.NeverAppend));
            });
        }

        public static void AddInfrastructureDIServices(this IServiceCollection services)
        {
            AddScopedInfrastructureServices(services);
            AddTransientInfrastructureServices(services);
            AddSingletonInfrastructureServices(services);
        }

        private static void AddTransientInfrastructureServices(this IServiceCollection services)
        {
        }

        private static void AddScopedInfrastructureServices(this IServiceCollection services)
        {
            services.AddScoped<UserInitializer>();

            services.AddScoped<DataInitializer>();

            services.AddScoped<FolderConfigurator>();

            services.AddScoped<IFolderCommandCreator, FolderCommandCreator>();

            services.AddScoped<IFolderManager, FolderManager>();

        }

        private static void AddSingletonInfrastructureServices(this IServiceCollection services)
        {
        }
    }
}
