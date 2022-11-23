using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SODP.Application.Interfaces;

namespace SODP.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplicationDI(this IServiceCollection services, IConfiguration configuration)
        {
            return services;
        }
    }
}
