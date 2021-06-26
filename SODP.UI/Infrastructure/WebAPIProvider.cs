using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SODP.UI.Infrastructure
{
    public class WebAPIProvider : IWebAPIProvider
    {
        public WebAPIProvider(IConfiguration configuration)
        {
            apiUrl = "https://localhost:44303";
        }
        public string apiUrl { get; private set; }
    }
}
