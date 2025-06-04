using System;
using APAF.Domain.Core.Contracts.Repository;
using APAF.Domain.Core.Contracts.Services;
using APAF.Infrastructure.EFCore.FileManagers;
using APAF.Infrastructure.EFCore.Repositories;
using APAF.Services.Domain;

namespace Panel.PanelServices;

public static class ServiceConfiguration
{
        public static void AddCustomServices(IServiceCollection services, string externalDirectoryPath)
        {
           
            services.AddSingleton(new FolderManagementService(externalDirectoryPath));
            //1
            services.AddScoped<IAvatarRepository, AvatarRepository>();
            services.AddScoped<IAvatarService, AvatarService>();
            //2
            services.AddTransient<IFileManager, FileManager>();
            services.AddTransient<IFileManagerService, FileManagerService>();
            //3
            services.AddScoped<IVersionOfAPKRepository, VersionOfAPKRepository>();
            services.AddScoped<IVersionOfAPKService, VersionOfAPKService>();
            //4
            services.AddScoped<ISchoolRepository,SchoolRepository>();
            services.AddScoped<ISchoolService,SchoolService>();


        }
}
