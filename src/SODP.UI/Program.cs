using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;
using SODP.DataAccess;

namespace SODP.UI
{
	public class Program
    {
        public static void Main(string[] args)
        {
			var configuration = new ConfigurationBuilder()
						 .AddEnvironmentVariables()
						 .AddCommandLine(args)
						 .AddJsonFile("appsettings.json")
						 .Build();

			var hostBuilder = Host.CreateDefaultBuilder(args)
				.ConfigureLogging(logBuilder =>
				{
					logBuilder.ClearProviders();
					logBuilder.AddConsole();
					logBuilder.AddTraceSource("Information, ActivityTracing");
				})
				.ConfigureWebHostDefaults(webBuilder =>
				{
					webBuilder.UseStartup<Startup>();
					webBuilder.UseUrls(configuration.GetSection("AppSettings:applicationUrl").Value);

				})
				.UseSerilog((context, services, configuration) => configuration
					.ReadFrom.Configuration(context.Configuration)
					.ReadFrom.Services(services)
					.Enrich.FromLogContext()
					.WriteTo.Console());

			var host = hostBuilder.Build();

            using (var scope = host.Services.CreateScope())
            {
                var provider = scope.ServiceProvider;
                var db = provider.GetRequiredService<SODPDBContext>();

                db.Database.Migrate();
                db.Database.EnsureCreated();

				//provider.GetRequiredService<UserInitializer>().UserInit();
				//provider.GetRequiredService<DataInitializer>().LoadData();
			}

			host.Run();
		}
    }
}