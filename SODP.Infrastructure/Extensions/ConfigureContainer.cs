using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SODP.DataAccess;

namespace SODP.Infrastructure.Extensions
{
    public static class ConfigureContainer
    {
        public static void ConfigureSwagger(this IApplicationBuilder app)
        {
            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "SODP.API v1"));
        }

        //public static void ApplyMigrations(this IApplicationBuilder app, IHost host)
        //{
        //    using (var scope = host.Services.CreateScope())
        //    {
        //        var db = scope.ServiceProvider.GetRequiredService<SODPDBContext>();
        //        db.Database.Migrate();
        //        db.Database.EnsureCreated();
        //        scope.ServiceProvider.GetRequiredService<UserInitializer>().UserInit();
        //        scope.ServiceProvider.GetRequiredService<DataInitializer>().LoadData();
        //    }
        //}
    }
}
