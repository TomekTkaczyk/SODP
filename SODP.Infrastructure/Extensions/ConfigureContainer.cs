using Microsoft.AspNetCore.Builder;

namespace SODP.Infrastructure.Extensions
{
    public static class ConfigureContainer
    {
        public static void UseMySwagger(this IApplicationBuilder app)
        {
            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "SODP.API"));
        }
    }
}
