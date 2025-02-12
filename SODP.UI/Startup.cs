using FluentValidation;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpLogging;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using SODP.Application.Services;
using SODP.DataAccess;
using SODP.Infrastructure;
using SODP.Infrastructure.Managers;
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
        private readonly string _appPrefix;
        
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;

            _appPrefix = Configuration.GetSection("AppSettings:AppPrefix").Value;
            var sodpDllFiles = Directory.GetFiles(AppDomain.CurrentDomain.BaseDirectory, _appPrefix + "*.dll")
                .Where(x => !x.Contains("SODP.UI.Views.dll"));

            foreach (string filePath in sodpDllFiles)
            {   
                AppDomain.CurrentDomain.Load(Path.GetFileNameWithoutExtension(filePath));
            }
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(options => options.AddPolicy(name: "SODPOriginsSpecification", builder => builder.WithOrigins($"{Configuration.GetSection($"AppSettings:Origin").Value}")));

            services.AddInfrastructure(Configuration);

            var app = AppDomain.CurrentDomain
                    .GetAssemblies()
                    .Where(x => x.GetName()
                    .Name
                    .Contains(_appPrefix))
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

            services.AddSingleton<LanguageTranslatorFactory>();

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

            services.AddScoped<UserManager<User>,SODPUserManager>();

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

            services.AddHttpLogging(logging =>
            {
                logging.LoggingFields = HttpLoggingFields.All;
				logging.RequestHeaders.Add("Authorization");
				logging.ResponseHeaders.Add("Authorization");
				logging.MediaTypeOptions.AddText("application/json");
				logging.MediaTypeOptions.AddText("application/xml");
				logging.MediaTypeOptions.AddText("text/plain");
				logging.RequestBodyLogLimit = 4096;
				logging.ResponseBodyLogLimit = 4096;
			});

			services.AddAntiforgery(o => o.HeaderName = "XSRF-TOKEN");

			services.AddControllers();

			services.AddRazorPages()
                .AddRazorRuntimeCompilation();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IConfiguration configuration)
        {

            app.UseInfrastructure(configuration, env);
        }
    }
}
