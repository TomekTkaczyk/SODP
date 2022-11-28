using FluentValidation;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SODP.Application;
using SODP.Application.Services;
using SODP.DataAccess;
using SODP.Infrastructure;
using SODP.Model;
using SODP.UI.Areas.Identity;
using SODP.UI.Infrastructure;
using SODP.UI.Services;
using System;
using System.IO;
using System.Linq;

namespace SODP.UI
{
    public class Startup
    {

        private readonly string appPrefix;
        
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;

            appPrefix = Configuration.GetSection("AppSettings:AppPrefix").Value;
            var sodpDllFiles = Directory.GetFiles(AppDomain.CurrentDomain.BaseDirectory, appPrefix + "*.dll")
                .Where(x => !x.Contains("SODP.UI.Views.dll"));

            foreach (string filePath in sodpDllFiles)
            {   
                AppDomain.CurrentDomain.Load(Path.GetFileNameWithoutExtension(filePath));
            }
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSwagger(Configuration);

            services.AddCors(options =>
                options.AddPolicy(name: "SODPOriginsSpecification", builder => 
                    {
                        //builder.AllowAnyOrigin();                               // mozliwy dowolny origin inny ni¿ w³asny aplikacji
                        //builder.AllowAnyMethod();                               // mozliwy dowolny origin inny ni¿ w³asny aplikacji
                        //builder.AllowAnyHeader();                               // mozliwy dowolny origin inny ni¿ w³asny aplikacji
                        builder.WithOrigins("https://localhost:44303");           // mozliwy origin 
                    })
            );

            services.AddDataAccessDI(Configuration);

            services.AddApplicationDI(Configuration);

            services.AddInfrastructureDI(Configuration);

            var app = AppDomain.CurrentDomain
                    .GetAssemblies()
                    .Where(x => x.GetName()
                    .Name
                    .Contains(appPrefix))
                    .ToArray();

            services.Scan(scan =>
            {
                scan
                    .FromAssemblies(app)
                    .AddClasses(classes => classes.AssignableTo(typeof(IAppService)))
                    .AsImplementedInterfaces()
                    .WithTransientLifetime();

                scan
                    .FromAssemblies(app)
                    .AddClasses(classes => classes.AssignableTo(typeof(IValidator)))
                    .AsImplementedInterfaces()
                    .WithTransientLifetime();
            });

            services.AddTransient<ITranslator, Translator>();

            services.AddScoped<IWebAPIProvider, WebAPIProvider>();

            services.AddTransient<IdentityErrorDescriber, CustomIdentityErrorDescriber>();

            services.AddIdentity<User, Role>(options =>   
            {
                options.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._";
                options.Password.RequiredLength = int.Parse(Configuration.GetSection("PasswordPolicy:RequiredLength").Value);
                options.Password.RequireLowercase = bool.Parse(Configuration.GetSection("PasswordPolicy:RequireLowercase").Value);
                options.Password.RequireUppercase = bool.Parse(Configuration.GetSection("PasswordPolicy:RequireUppercase").Value);
                options.Password.RequireNonAlphanumeric = bool.Parse(Configuration.GetSection("PasswordPolicy:RequireNonAlphanumeric").Value);
                options.Password.RequireDigit = bool.Parse(Configuration.GetSection("PasswordPolicy:RequireDigit").Value);
            })  
                .AddEntityFrameworkStores<SODPDBContext>()
                .AddDefaultTokenProviders()
                .AddDefaultUI();

            //services.ConfigureApplicationCookie(options =>
            //{
            //    options.LoginPath = $"/Identity/Account/Login";
            //    options.LogoutPath = $"/Identity/Account/Logout";
            //    options.AccessDeniedPath = $"/Identity/Account/AccessDenied";
            //});

            services.AddAutoMapper(app);

            services.AddMemoryCache();

            services.AddDistributedMemoryCache();

            services.AddHttpClient("SODPApiClient", client => 
            {
                client.BaseAddress = new Uri(
                    $"{Configuration.GetSection($"AppSettings:apiUrl").Value}" +
                    $"{Configuration.GetSection($"AppSettings:apiVersion").Value}/"
                    );
                client.Timeout = new TimeSpan(0, 0, 30);
                client.DefaultRequestHeaders.Clear();
            });

            services.AddAntiforgery(o => o.HeaderName = "XSRF-TOKEN");

            services.AddControllers();

            services.AddRazorPages()
                .AddRazorRuntimeCompilation();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            //app.Run(async context => {
            //    await context.Response.WriteAsync("Hello world");
            //});

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }

            app.UseMySwagger();

            app.UseStatusCodePagesWithRedirects("/Errors/{0}");

            app.UseHttpsRedirection();

            app.UseStaticFiles();

            app.UseRouting();

            app.UseCors(options =>
                options
                //.AllowAnyOrigin()                           //
                //.AllowAnyMethod()                           //
                //.AllowAnyHeader()                           //
                .WithOrigins("https://localhost:44303")     //
            );

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapRazorPages();

            });
        }
    }
}
