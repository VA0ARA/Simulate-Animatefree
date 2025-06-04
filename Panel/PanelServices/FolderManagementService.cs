using Microsoft.Extensions.FileProviders;
using System.IO;

namespace Panel.PanelServices;
public class FolderManagementService
{
    private readonly string _externalDirectoryPath;

    public FolderManagementService(string externalDirectoryPath)
    {
        _externalDirectoryPath = externalDirectoryPath;
    }

    
    public void EnsureFoldersExist()
    {
        var requiredFolders = new string[]
        {
            "Assets",
            "Avatars",
            "Competitions",
            "Cartoons",
            "Gifts"
        };

        foreach (var folder in requiredFolders)
        {
            string folderPath = Path.Combine(_externalDirectoryPath, folder);
            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
                Console.WriteLine($"Folder {folder} created at {folderPath}");
            }
        }
    }

    // متدی برای تنظیم PhysicalFileProvider برای دسترسی به فایل‌ها
    public void ConfigureStaticFiles(IApplicationBuilder app)
    {
        app.UseStaticFiles(new StaticFileOptions
        {
            FileProvider = new PhysicalFileProvider(_externalDirectoryPath),
            RequestPath = "/external-static" // URL برای دسترسی به فایل‌ها
        });
    }
}

