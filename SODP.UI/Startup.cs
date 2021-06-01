using AutoMapper;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;
using SODP.Application.Managers;
using SODP.DataAccess;
using SODP.Domain.Managers;
using SODP.Domain.Services;
using SODP.Model;
using SODP.UI.Areas.Identity;
using System;
using System.IO;
using System.Linq;

namespace SODP.UI
{
    public class Startup
    {
        private readonly string appPrefix;

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;

            appPrefix = Configuration.GetSection("AppSettings:AppPrefix").Value;

            foreach (string filePath in Directory.GetFiles(AppDomain.CurrentDomain.BaseDirectory, appPrefix + "*.dll"))
            {
                if (Path.GetFileName(filePath) != "SODP.UI.Views.dll")
                {
                    AppDomain.CurrentDomain.Load(Path.GetFileNameWithoutExtension(filePath));
                }
            }
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "SODP.API", Version = "v1" });
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Description = "Please insert JWT with Bearer into field",
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey
                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        Array.Empty<string>()
                    }
                });
            });

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

            services.AddScoped<UserInitializer>();

            services.AddScoped<DataInitializer>();

            services.AddScoped<FolderConfigurator>();

            services.AddScoped<IFolderCommandCreator, FolderCommandCreator>();

            services.AddScoped<IFolderManager, FolderManager>();

            services.AddTransient<IdentityErrorDescriber, CustomIdentityErrorDescriber>();

            services.AddDbContext<SODPDBContext>(options =>
            {
                options.UseMySql(
                    Configuration.GetConnectionString("DefaultDbConnection"),
                    b => b.CharSetBehavior(CharSetBehavior.NeverAppend));
            });

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

            services.AddControllers();
            services.AddRazorPages()
                .AddRazorRuntimeCompilation();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "SODP.API v1"));
            }
            else
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();

            app.UseStaticFiles();

            app.UseRouting();

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
