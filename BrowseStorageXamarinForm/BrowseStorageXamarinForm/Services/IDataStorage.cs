using System;
using System.IO;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace BrowseStorageXamarinForm.Services
{
    public interface IDataStorage<T>
    {
        // Unique Item creation
        Task<T> createDirectoryItemAsync();

        // File operations
        Task<bool> AddImageFileAsync(T directoryItem);
        Task<bool> AddHTMLFileAsync(T directoryItem);
        Task<bool> DeleteFileAsync(T directoryItem);
        Task<FileInfo> GetFileInfoAsync(T directoryItem);
        Task<bool> DoesFileExist(T directoryItem);

        Task<IEnumerable<T>> GetFilesInDirectory(T directoryItem);

        // Directory operations
        Task<bool> AddDirectoryAsync(T directoryItem);
        Task<bool> DeleteDirectoryAsync(T directoryItem);
        Task<bool> DoesDirectoryExist(T directoryItem);

        Task<IEnumerable<T>> GetSubDirectories(T directoryItem); // Returns directory and files

        //        Task<bool> CopyDirectroyAsync(T sourceDirectroy, T destinationDirectroy);
        //        Task<bool> MoveDirectoryAsync(T sourceDirectroy, T destinationDirectroy);

        Task<bool> SetCurrentDirectory(T directory);
        Task<string> GetCurrentDirectory();
        Task<string> GetParentDirectory(T directory);
        Task<string> GetDirectoryRoot();

        //        Task<IEnumerable<T>> GetLogicalDrives();

        Task<string> GetLastAccessTime(T directoryItem);
    }
}
