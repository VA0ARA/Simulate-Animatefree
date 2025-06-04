using AFAP.EndPointMVCPattern.Infrastructure;
using AFAP.EndPointMVCPattern.MiddelWare;
using APAF.AppServices.Domain;
using APAF.Domain.Core.Contract.AppServices;
using APAF.Domain.Core.Contracts.AppServices;
using APAF.Domain.Core.Contracts.Repository;
using APAF.Domain.Core.Contracts.Services;
using APAF.Domain.Core.Entities.Configs;
using APAF.Domain.Core.Entities.IdentityEntities;
using APAF.Infrastructure.EFCore.Common;
using APAF.Infrastructure.EFCore.Repositories;
using APAF.Redis;
using APAF.Services.Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using Serilog;
using StackExchange.Redis;
using System;
namespace AFAP.EndPointMVCPattern
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddControllersWithViews();
            #region Register Services
            //1
            //builder.Services.AddScoped<IAdminRepository, AdminRepository>();
            //builder.Services.AddScoped<IAdminService, AdminService>();
           // builder.Services.AddScoped<IAdminAppService, AdminAppService>();
            //2
           // builder.Services.AddScoped<IOwnerRepository, OwnerRepository>();
            //builder.Services.AddScoped<IOwnerService, OwnerService>();
            //builder.Services.AddScoped<IOwnerAppService, OwnerAppService>();
            //3
            builder.Services.AddScoped<IAPKRepository, APKRepository>();
            builder.Services.AddScoped<IAPKService, APKService>();
            builder.Services.AddScoped<IAPKAppService, APKAppService>();
            //4
            builder.Services.AddScoped<IMatchRepository, MatchRepository>();
            builder.Services.AddScoped<IMatchService, MatchService>();
            builder.Services.AddScoped<IMatchAppService, MatchAppService>();
            //5
            builder.Services.AddScoped<IAssetBundleRepository, AssetBundleRepository>();
            builder.Services.AddScoped<IAssetBundleService, AssetBundleService>();
            builder.Services.AddScoped<IAssetBundleAppService, AssetBundleAppService>();
            //6
            builder.Services.AddScoped<IAnimatorRepository, AnimatorRepository>();
            builder.Services.AddScoped<IAnimatorService, AnimatorService>();
            builder.Services.AddScoped<IAnimatorAppService, AnimatorAppService>();
            //7
            builder.Services.AddScoped<IAvatarRepository, AvatarRepository>();
            builder.Services.AddScoped<IAvatarService, AvatarService>();
            builder.Services.AddScoped<IAvatarAppService, AvatarAppService>();
            //8
            builder.Services.AddScoped<ICartoonRepository, CartoonRepository>();
            builder.Services.AddScoped<ICartoonService, CartoonService>();
            builder.Services.AddScoped<ICartoonAppService, CartoonAppService>();
            //9
            //builder.Services.AddScoped<ICalculateScoreService, CalculateScoreService>();
            //builder.Services.AddScoped<ICalculateScoreAppService, CalculateScoreAppService>();
            //10
            builder.Services.AddScoped<IRedisCacheServices, RedisCacheServices>();
            //11
            //builder.Services.AddSingleton<PathService>();
            //12
            builder.Services.AddScoped<IAssetbundelAnimatoreRepository, AssetbundelAnimatorRepository>();
            builder.Services.AddScoped<IAssetBundelAnimatorService, AssetBundelAnimatorService>();
            builder.Services.AddScoped<IAssetBundelAnimatorAppService, AssetBundelAnimatorAppService>();
            //13
            builder.Services.AddSingleton<IPhysicalFolderExtensions, PhysicalFolderExtensions>();
            #endregion
            #region Json Environment Configuration
            builder.Configuration
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: true, reloadOnChange: true)
                .AddEnvironmentVariables();
            #endregion
            #region ConectionString
            #region win
            //builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("WindowsConetion")));
            #endregion
            #region static Form
            //builder.Services.AddDbContext<AppDbContext>
            //(options => options.UseSqlServer(builder.Configuration.GetConnectionString("Conection"), sqlOptions => sqlOptions.EnableRetryOnFailure(
            //maxRetryCount: 5, // Maximum number of retries
            //maxRetryDelay: TimeSpan.FromSeconds(10), // Max delay between retries
            //errorNumbersToAdd: null // Optional: Specify additional SQL error numbers to retry on
            //    )));
            #endregion
            #region dinamic Form
            string? connectionString = builder.Configuration.GetConnectionString("Connection");
             connectionString = connectionString
                .Replace("${DB_SERVER}", Environment.GetEnvironmentVariable("DB_SERVER") ?? "2.180.34.38") //"sql-server"
                .Replace("${DB_PORT}", Environment.GetEnvironmentVariable("DB_PORT") ?? "4030")
                .Replace("${DB_NAME}", Environment.GetEnvironmentVariable("DB_NAME") ?? "AdminDeveloperDatabase")
                .Replace("${DB_USER}", Environment.GetEnvironmentVariable("DB_USER") ?? "sa")
                .Replace("${DB_PASSWORD}", Environment.GetEnvironmentVariable("DB_PASSWORD") ?? " 18s<Rl0hF*J0WR02<x+)_#UA");
            builder.Services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(connectionString, sqlOptions =>
                    sqlOptions.EnableRetryOnFailure(
                        maxRetryCount: 5,
                        maxRetryDelay: TimeSpan.FromSeconds(20),
                        errorNumbersToAdd: null
                    )));
            #endregion
            #endregion
            #region Set Url
            var url = builder.Configuration.GetValue<string>("Application:Url");
            #endregion
            #region Add services to the container
            builder.Services.AddDataProtection()
                .PersistKeysToFileSystem(new DirectoryInfo("/root/.aspnet/DataProtection-Keys")) // For the MVC app's Data Protection keys
                .SetApplicationName("AFAP.EndPointMVCPattern"); // Use a unique application name
            #endregion
            #region Enable Identity in this project
            builder.Services.AddIdentity<ApplicationUser, ApplicationRole>(options => {
                options.Password.RequiredLength = 5;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireLowercase = true;
                options.Password.RequireDigit = false;
                options.Password.RequiredUniqueChars = 3; //Eg: AB12AB
            })
             .AddEntityFrameworkStores<AppDbContext>()
             .AddDefaultTokenProviders()
             .AddUserStore<UserStore<ApplicationUser, ApplicationRole, AppDbContext, Guid>>()
             .AddRoleStore<RoleStore<ApplicationRole, AppDbContext, Guid>>();
            #endregion
            #region policy
            // if client do not have Token
            builder.Services.AddAuthorization(options =>
            {
                options.FallbackPolicy = new AuthorizationPolicyBuilder().RequireAuthenticatedUser().Build(); //enforces authoriation policy (user must be authenticated) for all the action methods
            });

            builder.Services.ConfigureApplicationCookie(options =>
            {
                options.LoginPath = "/Account/Login";
            });
            #endregion
            #region CachConfiguration
            //builder.Services.AddStackExchangeRedisCache(options =>
            //{
            //    options.Configuration = siteSettings.RedisConfiguration.ConnectionString;
            //    options.ConfigurationOptions = new ConfigurationOptions
            //    {
            //        Password = string.Empty,
            //        DefaultDatabase = 10,
            //        ConnectTimeout = 5000,
            //    };
            //    options.ConfigurationOptions.EndPoints.Add(siteSettings.RedisConfiguration.ConnectionString);
            //});
            #endregion
            #region LOG Configuration
            builder.Host.UseSerilog((HostBuilderContext context, IServiceProvider services, LoggerConfiguration loggerConfiguration) =>
            {
                loggerConfiguration
                .ReadFrom.Configuration(context.Configuration) //read configuration settings from built-in IConfiguration
                .ReadFrom.Services(services); //read out current app's services and make them available to serilog
            });
            // log HTTPS
            builder.Services.AddHttpLogging(options =>
            {
                options.LoggingFields = Microsoft.AspNetCore.HttpLogging.HttpLoggingFields.RequestProperties | Microsoft.AspNetCore.HttpLogging.HttpLoggingFields.ResponsePropertiesAndHeaders;
            });
            #endregion
            #region PathVariable
            builder.Services.Configure<PathVariable>(builder.Configuration.GetSection("PathVariable"));
            #endregion
            var app = builder.Build();
            #region url pip
            if (!string.IsNullOrEmpty(url))
            {
                builder.WebHost.UseUrls(url);
            }
            #endregion
            #region   Pipeline
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            #region WWWROOT
            #region Createby.Net Nested Folder
            using (var scope = app.Services.CreateScope())
            {
                var directoryService = scope.ServiceProvider.GetRequiredService<IPhysicalFolderExtensions>();
                string baseDirectoryPath = directoryService.GetPhysicalFolder();

                // Define the paths for the nested folders
                string folderPath1 = Path.Combine(baseDirectoryPath, "File", "AssetBundelanim");
                string folderPath2 = Path.Combine(baseDirectoryPath, "File", "AssetBundelBanner");
                string folderPath3 = Path.Combine(baseDirectoryPath, "File", "AvatrFile");
                // Create the nested folders if they don't exist
                if (!Directory.Exists(folderPath1))
                {
                    Directory.CreateDirectory(folderPath1);
                }

                if (!Directory.Exists(folderPath2))
                {
                    Directory.CreateDirectory(folderPath2);
                }

                if (!Directory.Exists(folderPath3))
                {
                    Directory.CreateDirectory(folderPath3);
                }
                // Add PhysicalFileProvider with the directory path
                app.UseStaticFiles(new StaticFileOptions
                {
                    FileProvider = new PhysicalFileProvider(baseDirectoryPath),
                    RequestPath = "/external-static" // This is the URL segment for accessing the files
                });
            }

            #endregion
            #region CreatebyLinux
            //var externalStaticFolderPath = "/AnimateFree/File";
            //if (!Directory.Exists(physicalFolderPath))
            //{
            //    Directory.CreateDirectory(physicalFolderPath);
            //}
            //app.UseStaticFiles(new StaticFileOptions
            //{
            //    FileProvider = new PhysicalFileProvider(physicalFolderPath),
            //    RequestPath = "/static"
            //});
            #endregion
            #region Windows
            //var externalStaticFolderPath = @"D:\ExternalStaticFiles";
            ////Ensure the folder exists
            //if (!Directory.Exists(externalStaticFolderPath))
            //{
            //    Directory.CreateDirectory(externalStaticFolderPath);
            //}
            ////Serve static files from the external folder
            //app.UseStaticFiles(new StaticFileOptions
            //{
            //    FileProvider = new PhysicalFileProvider(externalStaticFolderPath),
            //    RequestPath = "/external-static"
            //});
            #endregion
            #endregion
            #region ExceptionHandeling-middelware
            var environment = builder.Environment;
            if (environment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else if(environment.IsProduction())
            {
                // build-in middleware for client 
                app.UseExceptionHandler("/Home/Error");
                // custom Exception for programmer
                app.UseExceptionHandlingMiddleware();
            }
            #endregion
            app.UseRouting(); //Identifying action method based on route
            app.UseAuthentication(); //Reading Identity cookie
            app.UseAuthorization(); //Validates access permissions of the user
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");
            app.Run();
            #endregion
        }
    }
}
