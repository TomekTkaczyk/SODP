using Microsoft.AspNetCore.Hosting;

[assembly: HostingStartup(typeof(SODP.UI.Areas.Identity.IdentityHostingStartup))]
namespace SODP.UI.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => {
            });
        }
    }
}