using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SODP.UI.Infrastructure
{
    public interface IWebAPIProvider
    {
          public string apiUrl { get; }
          public string apiVersion { get; }
    }
}
