using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SODP.Application;
using SODP.Application.Services;
using SODP.DataAccess;
using SODP.Domain.Entities;
using SODP.Infrastructure;
using SODP.Infrastructure.Managers;
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
			services.AddMediatR(Application.AssemblyReference.Assembly);

             // Warning: Generic handlers must be manually registered in the MS DI container
			services.AddActiveStatusCommandHandlers(Domain.AssemblyReference.Assembly);

			services.AddSwagger(Configuration);

            services.AddCors(options => options.AddPolicy(name: "SODPOriginsSpecification", builder => builder.WithOrigins($"{Configuration.GetSection($"AppSettings:Origin").Value}")));

            services.AddDataAccess(Configuration);

            services.AddInfrastructure();

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
                scan
                    .FromAssemblies(
                        SODP.Infrastructure.AssemblyReference.Assembly,
                        SODP.DataAccess.AssemblyReference.Assembly)
                    .AddClasses(false)
                    .AsImplementedInterfaces()
                    .WithScopedLifetime();
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

            services.AddAntiforgery(o => o.HeaderName = "XSRF-TOKEN");

            services.AddControllers()
                .AddApplicationPart(Application.AssemblyReference.Assembly);

            services.AddRazorPages()
                .AddRazorRuntimeCompilation();

            // remove if use .net core 5 or higher
            services.AddMvc(options => options.SuppressAsyncSuffixInActionNames = false);
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {

			app.UseSwagger();
            
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Errors");
                app.UseHsts();
            }

            app.UseHttpLogging();

            app.UseStatusCodePagesWithRedirects("/Errors/{0}");

            app.UseHttpsRedirection();

            app.UseStaticFiles();

            app.UseRouting();

            app.UseCors(options => options.WithOrigins($"{Configuration.GetSection($"AppSettings:Origin").Value}"));

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
