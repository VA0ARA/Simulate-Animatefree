namespace AFAP.EndPointMVCPattern.Infrastructure
{
    public static  class ServicePhysicalFolderExtensions
    {
        public static IServiceCollection CreateFileFolder(this IServiceCollection services)
        {
            services.AddSingleton<IPhysicalFolderExtensions, PhysicalFolderExtensions>(); // Scoped service registration
            return services;
        }
    }
}
