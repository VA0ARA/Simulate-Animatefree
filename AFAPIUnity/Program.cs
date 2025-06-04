using AFAPIUnity.TokenService;
using APAF.AppServices.Domain;
using APAF.Domain.Core.Contracts.AppServices;
using APAF.Domain.Core.Contracts.Repository;
using APAF.Domain.Core.Contracts.Services;
using APAF.Infrastructure.EFCore.Common;
using APAF.Infrastructure.EFCore.Repositories;
using APAF.Services.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System;
using System.Text;
using APAF.Domain.Core.Contract.AppServices;
using Microsoft.OpenApi.Models;
using Serilog;
using AFAPIUnity.MiddelWare;
using AFAPIUnity.EnpointServices.Contract;
using AFAPIUnity.EnpointServices.Services;
using Microsoft.Extensions.FileProviders;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Authorization;
using System.Threading.Tasks;
namespace AFAPIUnity
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            #region SetUp-Swagger
            builder.Services.AddSwaggerGen(options =>
            {
                var securityScheme = new OpenApiSecurityScheme
                {
                    Name = "JWT Authentication",
                    Description = "Enter a valid JWT bearer token",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.Http,
                    Scheme = "bearer",
                    BearerFormat = "JWT",
                    Reference = new OpenApiReference
                    {
                        Id = JwtBearerDefaults.AuthenticationScheme,
                        Type = ReferenceType.SecurityScheme
                    }
                };
                options.AddSecurityDefinition(securityScheme.Reference.Id, securityScheme);
                options.AddSecurityRequirement(new OpenApiSecurityRequirement
                  {
                          {securityScheme, new string[] {} }
                 });
            });
            #endregion
            #region Register Services
            builder.Services.AddSingleton<IAuthorizationHandler, UserOnlyHandler>();
            builder.Services.AddSingleton<IAuthorizationHandler, GuestOnlyHandler>();
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
            builder.Services.AddScoped<ICartoonRepositoryService, CartoonRepositoryService>();
            builder.Services.AddScoped<ICartoonRepositoryAppService, CartoonRepositoryAppService>();
            //9
            builder.Services.AddScoped<IAssetbundelAnimatoreRepository, AssetbundelAnimatorRepository>();
            builder.Services.AddScoped<IAssetBundelAnimatorService, AssetBundelAnimatorService>();
            builder.Services.AddScoped<IAssetBundelAnimatorAppService, AssetBundelAnimatorAppService>();
            //10
            builder.Services.AddScoped<ISetAssetAppService, SetAssetAppService>();
            //11
            builder.Services.AddScoped<IGenerateToken, GernerateToken>();
            //12 FileDownload
            builder.Services.AddHttpContextAccessor();  // Register IHttpContextAccessor
            builder.Services.AddScoped<IMyUrlService, MyUrlService>();
            builder.Services.AddScoped<IGenerateUnityDto, GenerateUnityDto>();
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
            #region Configuration ConectionString
            #region static Form
            /*            builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("Conetion"), sqlOptions => sqlOptions.EnableRetryOnFailure(
                        maxRetryCount: 5, // Maximum number of retries
                        maxRetryDelay: TimeSpan.FromSeconds(10), // Max delay between retries
                        errorNumbersToAdd: null // Optional: Specify additional SQL error numbers to retry on
                            )));*/
            #endregion
            #region dinamic Form
            string connectionString = builder.Configuration.GetConnectionString("Concetion");
            connectionString = connectionString
                .Replace("${DB_SERVER}", Environment.GetEnvironmentVariable("DB_SERVER") ?? "*************") //"sql-server"
                .Replace("${DB_PORT}", Environment.GetEnvironmentVariable("DB_PORT") ?? "4030")
                .Replace("${DB_NAME}", Environment.GetEnvironmentVariable("DB_NAME") ?? "*******************")
                .Replace("${DB_USER}", Environment.GetEnvironmentVariable("DB_USER") ?? "sa")
                .Replace("${DB_PASSWORD}", Environment.GetEnvironmentVariable("DB_PASSWORD") ?? " 1*************************)_#UA");
            builder.Services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(connectionString, sqlOptions =>
                    sqlOptions.EnableRetryOnFailure(
                        maxRetryCount: 5,
                        maxRetryDelay: TimeSpan.FromSeconds(20),
                        errorNumbersToAdd: null
                    )));
            #endregion
            #endregion
            #region Add services to the container.
            builder.Services.AddDataProtection()
                .PersistKeysToFileSystem(new DirectoryInfo("/root/.aspnet/DataProtection-Keys")) // API app's Data Protection keys
                .SetApplicationName("AFAPIUnity"); // Use a unique application name
            #endregion
            #region Set Url
            var url = builder.Configuration.GetValue<string>("Application:Url");
            #endregion
            #region Token
            var secretKey = builder.Configuration.GetValue<string>("SecretKey");
            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultSignInScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(secretKey ?? string.Empty)),
                    ValidateLifetime = true,
                    ValidateAudience = false,
                    ValidateIssuer = false,
                    ClockSkew = TimeSpan.Zero,
                };
            });
            #endregion
            #region policy
            builder.Services.AddAuthorization(options =>
            {
                options.AddPolicy("AnimatorOnly", policy =>
                policy.Requirements.Add(new UserOnlyAttribute()));
            });
            builder.Services.AddAuthorization(options =>
              {
                options.AddPolicy("GuestOnly", policy =>
                policy.Requirements.Add(new GuestOnlyAttribute()));
              });
            #endregion
            #region PathVariable
            builder.Services.Configure<PathVariable>(builder.Configuration.GetSection("PathVariable"));
            #endregion
            #region LOG
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
            var app = builder.Build();
            #region url pip
            //bool useHttps = true;
            if (!string.IsNullOrEmpty(url))
            {
                builder.WebHost.UseUrls(url);
               // Add the protocol dynamically
             //var protocol = useHttps ? "https://" : "http://";
             //builder.WebHost.UseUrls($"{protocol}{url}");
            }
            #endregion
            #region Excption Handeler
            if (app.Environment.IsDevelopment())
            {

                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "APIAnimateFree");
                    c.RoutePrefix = string.Empty; // To serve Swagger UI at the app's root (http://localhost:<port>/)
                });

            }
            #endregion
            #region Pipeline
            #region WWWROOT
            #region Createby.Net nested Folder
            using (var scope = app.Services.CreateScope())
            {
                var directoryService = scope.ServiceProvider.GetRequiredService<IPhysicalFolderExtensions>();
                string baseDirectoryPath = directoryService.GetPhysicalFolder();

                // Define the paths for the nested folders
                string folderPath1 = Path.Combine(baseDirectoryPath, "File", "AssetBundelanim");
                string folderPath2 = Path.Combine(baseDirectoryPath,  "File", "AssetBundelBanner");
                string folderPath3 = Path.Combine(baseDirectoryPath,  "File", "AvatrFile");

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
            #region Windows
            /*            var externalStaticFolderPath = @"D:\ExternalStaticFiles";
                        // Ensure the folder exists
                        if (!Directory.Exists(externalStaticFolderPath))
                        {
                            Directory.CreateDirectory(externalStaticFolderPath);
                        }
                        // Serve static files from the external folder
                        app.UseStaticFiles(new StaticFileOptions
                        {
                            FileProvider = new PhysicalFileProvider(externalStaticFolderPath),
                            // RequestPath = "/static" // URL prefix for accessing static files
                            RequestPath = "/external-static"
                        });*/
            #endregion
            #region Linux
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
            #endregion
            app.UseExceptionHandlingMiddleware();
            app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseAuthorization();
            app.MapControllers();
            app.Run();
            #endregion
        }
    }
}
