using Microsoft.Extensions.Configuration;

namespace SODP.UI.Infrastructure
{
    public class WebAPIProvider : IWebAPIProvider
    {
        public WebAPIProvider(IConfiguration configuration)
        {
            apiUrl = "https://localhost:44303/api";
            apiVersion = "/v0_01";
        }
        public string apiUrl { get; private set; }

        public string apiVersion { get; private set; }
    }
}
