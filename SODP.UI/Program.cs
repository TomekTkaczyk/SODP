using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SODP.DataAccess;

//#error version

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
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                    webBuilder.UseUrls(configuration.GetSection("AppSettings:applicationUrl").Value);
                });

            var host = hostBuilder.Build();
            using (var scope = host.Services.CreateScope())
            {
                var db = scope.ServiceProvider.GetRequiredService<SODPDBContext>();
                db.Database.Migrate();
                db.Database.EnsureCreated();
                scope.ServiceProvider.GetRequiredService<UserInitializer>().UserInit();
                scope.ServiceProvider.GetRequiredService<DataInitializer>().LoadData();
            }

            host.Run();
        }
    }
}
