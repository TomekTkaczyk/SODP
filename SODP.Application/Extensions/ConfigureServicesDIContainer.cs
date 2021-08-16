using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace SODP.Application.Extensions
{
    public static class ConfigureServicesDIContainer
    {
        public static IServiceCollection AddApplicationDIServices(this IServiceCollection services)
        {
            return services;
        }
    }
}
