using System;
using System.IO;
using System.Text;
using System.Collections.Generic;
using System.Threading.Tasks;

using BrowseStorageXamarinForm.Models;

namespace BrowseStorageXamarinForm.Services
{
    public class DataStorage : IDataStorage<DirectoryItem>
    {

        static public Dictionary<FileTypeEnum, string> fileTypeEnumMap;
        static public Dictionary<string, FileTypeEnum> fileTypeEnumReverseMap;

        static public Dictionary<DirectoryTypeEnum, string> directoryTypeEnumMap;


        public DataStorage()
        {

            if (fileTypeEnumMap == null)
            {
                fileTypeEnumMap = new Dictionary<FileTypeEnum, string>();
                fileTypeEnumMap.Add(FileTypeEnum.DIRECTORY, "DIRECTORY");
                fileTypeEnumMap.Add(FileTypeEnum.PNG, "PNG");
                fileTypeEnumMap.Add(FileTypeEnum.HTML, "HTML");
                fileTypeEnumMap.Add(FileTypeEnum.JPG, "JPG");
                fileTypeEnumMap.Add(FileTypeEnum.TXT, "TXT");

            }

            if (fileTypeEnumReverseMap == null)
            {
                fileTypeEnumReverseMap = new Dictionary<string, FileTypeEnum>();
                fileTypeEnumReverseMap.Add("DIRECTORY", FileTypeEnum.DIRECTORY);
                fileTypeEnumReverseMap.Add("PNG", FileTypeEnum.PNG);
                fileTypeEnumReverseMap.Add("HTML", FileTypeEnum.HTML);
                fileTypeEnumReverseMap.Add("JPG", FileTypeEnum.JPG);
                fileTypeEnumReverseMap.Add("JPEG", FileTypeEnum.JPG);
            }

            if (directoryTypeEnumMap == null)
            {
                directoryTypeEnumMap = new Dictionary<DirectoryTypeEnum, string>();

                //Application directories
                string homeDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                string libraryDirectory = Path.Combine(homeDirectory, "..", "Library");
                string cacheDirectory = Path.Combine(homeDirectory, "..", "Library", "Caches");
                string tmpDirectory = Path.Combine(homeDirectory, "..", "Library", "tmp");

                directoryTypeEnumMap.Add(DirectoryTypeEnum.DOCUMENT, homeDirectory);
                directoryTypeEnumMap.Add(DirectoryTypeEnum.LIBRARY, libraryDirectory);
                directoryTypeEnumMap.Add(DirectoryTypeEnum.CACHE, cacheDirectory);
                directoryTypeEnumMap.Add(DirectoryTypeEnum.TMP, tmpDirectory);
            }
        }


        public async Task<DirectoryItem> createDirectoryItemAsync()
        {
            DirectoryItem directoryItem = new DirectoryItem();

            directoryItem.Id = Guid.NewGuid().ToString();
            // For a better performance, maybe use a long static integer
            // as a unique number generator instead

            return await Task.FromResult(directoryItem);
        }

        //**************************************************
        // File operations
        public async Task<bool> AddImageFileAsync(DirectoryItem directoryItem)
        {
            try // Save to storage
            {
                if ((directoryItem != null)
                     &&
                     (directoryItem.FileType != FileTypeEnum.DIRECTORY)
                     &&
                     (directoryItem.fileAsBytes != null)
                     )
                {
                    string filePath = Path.Combine(directoryItem.Path, directoryItem.Name + "." + fileTypeEnumMap[directoryItem.FileType]);

                    using (FileStream imageFileStream = File.Open(filePath, FileMode.OpenOrCreate))
                    {
                        imageFileStream.Seek(0, SeekOrigin.End);
                        await imageFileStream.WriteAsync(directoryItem.fileAsBytes, 0, directoryItem.fileAsBytes.Length);
                    }
                }
                else
                {
                    return await Task.FromResult(false);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return await Task.FromResult(false);
            }

            return await Task.FromResult(true);

        }

        //**************************************************

        public async Task<bool> AddHTMLFileAsync(DirectoryItem directoryItem)
        {
            try // Save to storage
            {
                if ((directoryItem != null)
                     &&
                     (directoryItem.FileType != FileTypeEnum.DIRECTORY)
                     &&
                     (directoryItem.fileAsBytes != null)
                     )
                {
                    string filePath = Path.Combine(directoryItem.Path, directoryItem.Name + ".html");

                    UnicodeEncoding uniencoding = new UnicodeEncoding();
                    string fileAsText = uniencoding.GetString(directoryItem.fileAsBytes);

                    await Task.Factory.StartNew(() =>
                    {
                        File.WriteAllText(filePath, fileAsText);
                    });
                }
                else
                {
                    return await Task.FromResult(false);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return await Task.FromResult(false);
            }

            return await Task.FromResult(true);

        }

        //**************************************************

        public async Task<bool> DeleteFileAsync(DirectoryItem directoryItem)
        {
            try // Delete from storage
            {
                if ((directoryItem != null)
                     &&
                     (directoryItem.FileType != FileTypeEnum.DIRECTORY)
                     &&
                     (directoryItem.Path != null)
                     )
                {
                    string filePath = Path.Combine(directoryItem.Path, directoryItem.Name + "." + fileTypeEnumMap[directoryItem.FileType]);

                    await Task.Factory.StartNew(() =>
                    {
                        if (File.Exists(filePath))
                        {
                            File.Delete(filePath);
                        }
                    });
                }
                else
                {
                    return await Task.FromResult(false);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return await Task.FromResult(false);
            }

            return await Task.FromResult(true);

        }

        //**************************************************

        public async Task<FileInfo> GetFileInfoAsync(DirectoryItem directoryItem)
        {
            try // Get FileInfo from storage
            {
                if ((directoryItem != null)
                     &&
                     (directoryItem.FileType != FileTypeEnum.DIRECTORY)
                     &&
                     (directoryItem.Path != null)
                     )
                {
                    string filePath = Path.Combine(directoryItem.Path, directoryItem.Name + "." + fileTypeEnumMap[directoryItem.FileType]);
                    return await Task.FromResult(new FileInfo(directoryItem.Path));
                }
                else
                {
                    return await Task.FromResult(new FileInfo(null));
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return await Task.FromResult(new FileInfo(null));
            }

        }

        //**************************************************

        public async Task<bool> DoesFileExist(DirectoryItem directoryItem)
        {
            try // Does file exists on storage
            {
                if ((directoryItem != null)
                     &&
                     (directoryItem.FileType != FileTypeEnum.DIRECTORY)
                     &&
                     (directoryItem.Path != null)
                     )
                {
                    string filePath = Path.Combine(directoryItem.Path, directoryItem.Name + "." + fileTypeEnumMap[directoryItem.FileType]);

                    return await Task.FromResult(File.Exists(filePath) ? true : false);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return await Task.FromResult(false);
            }

            return await Task.FromResult(false);
        }

        //**************************************************

        public async Task<IEnumerable<DirectoryItem>> GetFilesInDirectory(DirectoryItem directoryItem)
        {

            List<DirectoryItem> result = null;

            try
            {
                if ((directoryItem != null)
                     &&
                     (directoryItem.FileType == FileTypeEnum.DIRECTORY)
                     )
                {
                    string directoryPath;

                    if (directoryItem.Name != null)
                    {
                        directoryPath = Path.Combine(directoryItem.Path, directoryItem.Name);
                    }
                    else
                    {
                        directoryPath = directoryItem.Path;
                    }

                    result = new List<DirectoryItem>();

                    var files = Directory.EnumerateFiles(directoryPath);

                    foreach (string file in files)
                    {
                        Task<DirectoryItem> fileItemResult = createDirectoryItemAsync();

                        DirectoryItem fileItem = fileItemResult.Result;

                        // Extract file name
                        int lastIndex = file.LastIndexOf(Path.DirectorySeparatorChar);
                        string fileName = file.Substring(lastIndex + 1);

                        fileItem.Name = fileName;
                        fileItem.Path = directoryPath;
                        fileItem.LastAccessTime = Directory.GetLastAccessTime(fileItem.Path);

                        // Extract file extension
                        lastIndex = file.LastIndexOf('.');
                        string fileExt = file.Substring(lastIndex + 1).ToUpper();
                        fileItem.FileType = fileTypeEnumReverseMap[fileExt];

                        result.Add(fileItem);
                    }

                    return await Task.FromResult((IEnumerable<DirectoryItem>)result);
                }
                else
                {
                    return null;
                }
            }
            catch (Exception e)
            {

                Console.WriteLine(e.Message);
                result?.Clear();
                return null;
            }

        }

        //**************************************************
        // Directory operations

        public async Task<IEnumerable<DirectoryItem>> GetSubDirectories(DirectoryItem directoryItem)
        {

            List<DirectoryItem> result = null;

            try
            {
                if ((directoryItem != null)
                     &&
                     (directoryItem.FileType == FileTypeEnum.DIRECTORY)
                     )
                {
                    string directoryPath;

                    if (directoryItem.Name != null)
                    {
                        directoryPath = Path.Combine(directoryItem.Path, directoryItem.Name);
                    }
                    else
                    {
                        // At the root or the home directory
                        directoryPath = directoryItem.Path;
                    }

                    result = new List<DirectoryItem>();

                    var directories = Directory.EnumerateDirectories(directoryPath);

                    foreach (string directory in directories)
                    {
                        Task<DirectoryItem> directoryItemResult = createDirectoryItemAsync();

                        DirectoryItem tempDirectoryItem = directoryItemResult.Result;

                        // Extract directory name
                        int lastIndex = directory.LastIndexOf(Path.DirectorySeparatorChar);
                        string directoryName = directory.Substring(lastIndex + 1);

                        tempDirectoryItem.Name = directoryName;
                        tempDirectoryItem.Path = directoryPath;
                        tempDirectoryItem.LastAccessTime = Directory.GetLastAccessTime(directoryPath);
                        tempDirectoryItem.FileType = FileTypeEnum.DIRECTORY;

                        result.Add(tempDirectoryItem);
                    }

                    return await Task.FromResult((IEnumerable<DirectoryItem>)result);
                }
                else
                {
                    return null;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                result?.Clear();
                return null;
            }

        }

        //**************************************************

        public async Task<bool> AddDirectoryAsync(DirectoryItem directoryItem)
        {
            try // Add a directory on the storage
            {
                if ((directoryItem != null)
                     &&
                     (directoryItem.FileType == FileTypeEnum.DIRECTORY)
                     )
                {
                    string directoryPath = Path.Combine(directoryItem.Path, directoryItem.Name);

                    DirectoryInfo directoryInfo = Directory.CreateDirectory(directoryPath);
                }
                else
                {
                    return await Task.FromResult(false);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return await Task.FromResult(false);
            }

            return await Task.FromResult(true);

        }

        //**************************************************

        public async Task<bool> DeleteDirectoryAsync(DirectoryItem directoryItem)
        {
            try // Delete from storage
            {
                if ((directoryItem != null)
                     &&
                     (directoryItem.FileType == FileTypeEnum.DIRECTORY)
                     &&
                     (directoryItem.Path != null)
                     )
                {
                    string directoryPath = Path.Combine(directoryItem.Path, directoryItem.Name);

                    await Task.Factory.StartNew(() =>
                    {
                        if (Directory.Exists(directoryPath))
                        {
                            Directory.Delete(directoryPath);
                        }
                    });
                }
                else
                {
                    return await Task.FromResult(false);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return await Task.FromResult(false);
            }

            return await Task.FromResult(true);

        }

        //**************************************************

        public async Task<bool> DoesDirectoryExist(DirectoryItem directoryItem)
        {
            try // Does directory exists on storage
            {
                if ((directoryItem != null)
                     &&
                     (directoryItem.FileType == FileTypeEnum.DIRECTORY)
                     )
                {
                    string directoryPath = Path.Combine(directoryItem.Path, directoryItem.Name);

                    return await Task.FromResult(Directory.Exists(directoryPath) ? true : false);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return await Task.FromResult(false);
            }

            return await Task.FromResult(false);
        }

        //**************************************************
        //**************************************************


        public async Task<string> GetDirectoryRoot()
        {
            try // Get the current working directory
            {
                string defaultWorkingDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

                return await Task.FromResult(defaultWorkingDirectory);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
        }
        //**************************************************
        //**************************************************

        public async Task<string> GetCurrentDirectory()
        {
            try // Get the current working directory
            {
                Task<string> defaultWorkingDirectoryResult = GetDirectoryRoot();
                string defaultWorkingDirectory = defaultWorkingDirectoryResult?.Result;


                string currentDirectoryPath = Directory.GetCurrentDirectory();

                if (currentDirectoryPath != null)
                {
                    return await Task.FromResult(currentDirectoryPath);
                }
                else if (defaultWorkingDirectory != null)
                {
                    return await Task.FromResult(defaultWorkingDirectory);
                }
                else
                {
                    return null;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
        }
        //**************************************************
        public async Task<string> GetParentDirectory(DirectoryItem directoryItem)
        {

            try // Add a directory on the storage
            {
                if ((directoryItem != null)
                     &&
                     (directoryItem.FileType == FileTypeEnum.DIRECTORY)
                     )
                {
                    string directoryPath = Path.Combine(directoryItem.Path, directoryItem.Name);

                    DirectoryInfo parentDirectoryInfo = Directory.GetParent(directoryPath);

                    string parentDirectoryPath = parentDirectoryInfo?.FullName;


                    if (parentDirectoryPath != null)
                    {
                        return await Task.FromResult(parentDirectoryPath);
                    }
                    else
                    {
                        return null;
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
            return null;
        }

        //**************************************************

        public async Task<string> GetLastAccessTime(DirectoryItem directoryItem)
        {
            try // Return last acces time of dorectory on storage or return DateTime.min
            {
                if ((directoryItem != null)
                     &&
                     (directoryItem.FileType == FileTypeEnum.DIRECTORY)
                     )
                {
                    string directoryPath = Path.Combine(directoryItem.Path, directoryItem.Name);

                    DateTime directoryDateTime = Directory.GetLastAccessTime(directoryPath);

                    return await Task.FromResult(directoryDateTime.ToString());
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }

            return null;

        }

        //**************************************************

        public async Task<bool> SetCurrentDirectory(DirectoryItem directoryItem)
        {
            try // Add a directory on the storage
            {
                if ((directoryItem != null)
                     &&
                     (directoryItem.FileType == FileTypeEnum.DIRECTORY)
                     )
                {
                    string directoryPath = Path.Combine(directoryItem.Path, directoryItem.Name);

                    Directory.SetCurrentDirectory(directoryPath);
                }
                else
                {
                    return await Task.FromResult(false);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return await Task.FromResult(false);
            }

            return await Task.FromResult(true);

        }

        //**************************************************
    }
}

/*


    // Directory operations

    Task<bool> CopyDirectroyAsync(T sourceDirectroy, T destinationDirectroy);
    Task<bool> MoveDirectoryAsync(T sourceDirectroy, T destinationDirectroy);

    Task<IEnumerable<T>> GetLogicalDrives();

*/
