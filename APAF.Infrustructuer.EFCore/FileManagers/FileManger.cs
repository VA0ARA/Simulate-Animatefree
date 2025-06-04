using System.IO;
using System.Linq.Expressions;
using System.Threading.Tasks;
using APAF.Domain.Core.Contracts.Repository;
using Microsoft.AspNetCore.Http;

namespace APAF.Infrastructure.EFCore.FileManagers;
public class FileManager : IFileManager
{

    public async Task SaveFileAsync(string destinationFilePath, IFormFile file, CancellationToken cancellationToken)
{
        if (File.Exists(destinationFilePath))
        {
            await DeleteFileAsync(destinationFilePath);
            await SaveFileAsync(destinationFilePath,file,cancellationToken);
        }
        else
        {
            using (var fileStream = new FileStream(destinationFilePath, FileMode.Create, FileAccess.Write))
            {
                await file.CopyToAsync(fileStream, cancellationToken);
               
            }
        }
}
    public async Task CreateDirectoryAsync(string directoryPath)
    {
         if (!Directory.Exists(directoryPath))
               {
        Directory.CreateDirectory(directoryPath);            
              }
    }
    public async Task DeleteFileAsync(string filePath)
    {
        if (await FileExistsAsync(filePath))
        {
            
            await Task.Run(() => File.Delete(filePath));
        }
    }
    public async Task<bool> DirectoryExistsAsync(string directoryPath)
{
    
    if (string.IsNullOrEmpty(directoryPath))
    {
        Console.WriteLine("Directory path is null or empty.");
        return false;
    }

            
    return await Task.Run(() => Directory.Exists(directoryPath));
}
    public async Task<bool> FileExistsAsync(string filePath)
{
             
    if (string.IsNullOrEmpty(filePath))
    {
        Console.WriteLine("File path is null or empty.");
        return false;
    }

            
    return await Task.Run(() => File.Exists(filePath));
}
    public async Task ManageFolderAsynic(string BasePath,string OldFilePath, string NewFilePath){
        string basepath=BasePath;
        string folderToDelete=Path.Combine(basepath,OldFilePath);
        string folderToCreate=Path.Combine(basepath,NewFilePath);
        try{
                    if(Directory.Exists(folderToDelete)){
                         Directory.Delete(folderToDelete,true);
                    }
                    if(!Directory.Exists(folderToCreate)){
                        Directory.CreateDirectory(folderToCreate);
                    }
           }catch(Exception ex){

            Console.WriteLine($"Error :{ex.Message}");
        }
    }
    public string GetLastSegment(string input){
        string inner=Path.GetDirectoryName(input);
        int LastSlashIndex=-1;
        for (int i =inner.Length-1;i>=0;i--){
            if(inner[i]=='/'){
                LastSlashIndex=i;
                break;
            }
        }
        return LastSlashIndex>=0?inner.Substring(LastSlashIndex+1):inner;
    }

}