using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;
using Pomelo.EntityFrameworkCore.MySql.Storage;
using SODP.Application.Services;
using SODP.DataAccess;
using SODP.Domain.Repositories;
using SODP.Domain.Services;
using SODP.Infrastructure.Managers;
using SODP.Infrastructure.Repositories;
using SODP.Infrastructure.Services;

namespace SODP.Infrastructure;

public static class DependecyInjection
{
	public static IServiceCollection AddDataAccess(this IServiceCollection services, IConfiguration configuration)
	{
		services.AddTransient(typeof(IActiveStatusService<>), typeof(ActiveStatusService<>));

		services.AddDbContext<SODPDBContext>(options =>
		{
			options.EnableDetailedErrors();
			if (services.BuildServiceProvider().GetService<IHostEnvironment>().IsDevelopment())
			{
				options.EnableSensitiveDataLogging();
			}
			options.UseMySql(
				configuration.GetConnectionString("DefaultDbConnection"),
				b =>
				{
					b.SchemaBehavior(MySqlSchemaBehavior.Ignore);
					b.CharSetBehavior(CharSetBehavior.NeverAppend);
					b.ServerVersion(new ServerVersion(new Version(10, 4, 6), ServerType.MariaDb));
				});
		});

		services.AddScoped<IUnitOfWork, UnitOfWork>();

		services.AddScoped<SODPDBContext>();
		services.AddScoped<UserInitializer>();
		services.AddScoped<DataInitializer>();


		return services;
	}

	public static void UseSwagger(this IApplicationBuilder app)
	{
		SwaggerBuilderExtensions.UseSwagger(app);
		app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "SODP.WEBAPI"));
	}

	public static IServiceCollection AddSwagger(this IServiceCollection services, IConfiguration configuration)
	{
		services.AddSwaggerGen(c =>
		{
			c.SwaggerDoc("v1", new OpenApiInfo
			{
				Title = "SODP.WEBAPI",
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

		return services;
	}

	public static IServiceCollection AddInfrastructure(this IServiceCollection services)
	{
		services.AddTransient<IDateTime, DateTimeService>();
		services.AddScoped<IFolderConfigurator, FolderConfigurator>();
		services.AddScoped<IFolderCommandCreator, FolderCommandCreator>();
		services.AddScoped<IFolderManager, FolderManager>();

		return services;
	}

}
