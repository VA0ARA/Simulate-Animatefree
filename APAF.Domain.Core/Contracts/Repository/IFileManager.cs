using System;
using Microsoft.AspNetCore.Http;

namespace APAF.Domain.Core.Contracts.Repository;

public interface IFileManager
{
    Task SaveFileAsync(string destinationFilePath, IFormFile file, CancellationToken cancellationToken);
    Task CreateDirectoryAsync(string directoryPath);
    Task DeleteFileAsync(string filePath);
    Task<bool> FileExistsAsync(string filePath);
    Task<bool> DirectoryExistsAsync(string directoryPath);
    Task ManageFolderAsynic(string BasePath,string OldFilePath, string NewFilePath);
    string GetLastSegment(string input);
}

