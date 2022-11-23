using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;
using Pomelo.EntityFrameworkCore.MySql.Storage;
using SODP.Application.Interfaces;
using System;

namespace SODP.DataAccess
{
    public static class DependencyInjection
    {
        public static void AddDataAccessDI(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<SODPDBContext>(options =>
            {
                options.EnableDetailedErrors();
                options.UseMySql(
                    configuration.GetConnectionString("DefaultDbConnection"),
                    b =>
                    {
                        b.CharSetBehavior(CharSetBehavior.NeverAppend);
                        b.ServerVersion(new ServerVersion(new Version(10, 4, 6), ServerType.MariaDb));
                    });
            });
            
            services.AddScoped<ISODPDBContext>(provider => provider.GetService<SODPDBContext>());

            services.AddScoped<UserInitializer>();

            services.AddScoped<DataInitializer>();
        }
    }
}
