using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;
using Pomelo.EntityFrameworkCore.MySql.Storage;
using SODP.Application.Services;
using SODP.DataAccess;
using SODP.Domain.Managers;
using SODP.Infrastructure.Managers;
using SODP.Infrastructure.Services;
using SODP.Shared.Interfaces;
using SODP.Shared.Services;

namespace SODP.Infrastructure
{
	public static class DependecyInjection
	{

		private const string _docsVersion = "v0.01";
		private const string _titleApi = "SODP";


		public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
		{
			services.AddDbContext<SODPDBContext>(options =>
			{
				options.EnableDetailedErrors();
				if(services.BuildServiceProvider().GetService<IHostEnvironment>().IsDevelopment()) {
					options.EnableSensitiveDataLogging();
				}
				options.UseMySql(
					configuration.GetConnectionString("DefaultDbConnection"),
					new MySqlServerVersion(new Version(10, 4, 6)),
					b => b.SchemaBehavior(MySqlSchemaBehavior.Ignore));

			});

			services.AddSwaggerGen(swagger =>
			{
				swagger.CustomSchemaIds(x => x.FullName!.Replace("+", "-"));
				swagger.SwaggerDoc("v1",
					new OpenApiInfo
					{
						Title = "SODP API",
						Version = "v1"
					});
			});

			services.AddScoped<SODPDBContext>();

			services.AddScoped<UserInitializer>();

			services.AddScoped<DataInitializer>();

			services.AddTransient(typeof(IActiveStatusService<>), typeof(ActiveStatusService<>));

			services.AddSingleton<IDateTime, DateTimeService>();

			services.AddSingleton<IFolderCommandCreator, FolderCommandCreator>();

			services.AddSingleton<IFolderConfigurator, FolderConfigurator>();

			services.AddSingleton<IFolderManager, FolderManager>();
		}

		// Other methods remain unchanged

		public static IApplicationBuilder UseInfrastructure(
			this IApplicationBuilder app,
			IConfiguration configuration,
			IWebHostEnvironment env)
		{
			if(env.IsDevelopment()) {
				app.UseDeveloperExceptionPage();
				app.UseSwagger();
				app.UseSwaggerUI(x =>
				{
					x.RoutePrefix = "docs/swagger";
					x.SwaggerEndpoint($"/swagger/{_docsVersion}/swagger.json", _titleApi);
				});
			}
			else {
				app.UseExceptionHandler("/Errors");
				app.UseHsts();
			}

			app.UseHttpLogging();
			app.UseStatusCodePagesWithRedirects("/Errors/{0}");

			app.UseHttpsRedirection();

			app.UseStaticFiles();

			app.UseRouting();

			app.UseCors(options => options.WithOrigins($"{configuration.GetSection($"AppSettings:Origin").Value}"));

			app.UseAuthentication();

			app.UseAuthorization();

			app.UseEndpoints(endpoints =>
			{
				endpoints.MapControllers();
				endpoints.MapRazorPages();

			});

			return app;
		}
	}
}
