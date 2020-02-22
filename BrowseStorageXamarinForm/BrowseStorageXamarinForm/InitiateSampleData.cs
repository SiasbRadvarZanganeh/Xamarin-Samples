using System;
using System.IO;
using System.Reflection;
using Xamarin.Forms;
using BrowseStorageXamarinForm.Models;
using BrowseStorageXamarinForm.Services;
using System.Threading.Tasks;

namespace BrowseStorageXamarinForm
{
    public class InitiateSampleData
    {
        public InitiateSampleData()
        {

            string homeDirectoryPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments); // Set the home directory
            //Task<string> createRootDirectoryResult = DependencyService.Get<IDataStorage<DirectoryItem>>().GetDirectoryRoot();
            //homeDirectoryPath = createRootDirectoryResult?.Result;

            // Create testing directories and files
            DirectoryItem testDirectory;

            // Create Test1 directory

            Task<DirectoryItem> createDirectoryResult = DependencyService.Get<IDataStorage<DirectoryItem>>().createDirectoryItemAsync();
            testDirectory = createDirectoryResult?.Result;

            testDirectory.FileType = FileTypeEnum.DIRECTORY;
            testDirectory.Name = "test1";
            testDirectory.Path = homeDirectoryPath;

            Task<bool> thisDirectoryListResult = DependencyService.Get<IDataStorage<DirectoryItem>>().AddDirectoryAsync(testDirectory);

            // ***********************
            // Create Test2 directory

            createDirectoryResult = DependencyService.Get<IDataStorage<DirectoryItem>>().createDirectoryItemAsync();
            testDirectory = createDirectoryResult?.Result;

            testDirectory.FileType = FileTypeEnum.DIRECTORY;
            testDirectory.Name = "test2";
            testDirectory.Path = homeDirectoryPath;

            thisDirectoryListResult = DependencyService.Get<IDataStorage<DirectoryItem>>().AddDirectoryAsync(testDirectory);

            // ***********************
            // Create Test3 directory

            createDirectoryResult = DependencyService.Get<IDataStorage<DirectoryItem>>().createDirectoryItemAsync();
            testDirectory = createDirectoryResult?.Result;

            testDirectory.FileType = FileTypeEnum.DIRECTORY;
            testDirectory.Name = "test3";
            testDirectory.Path = homeDirectoryPath;

            thisDirectoryListResult = DependencyService.Get<IDataStorage<DirectoryItem>>().AddDirectoryAsync(testDirectory);

            // ***********************
            // Create Test4 directory

            createDirectoryResult = DependencyService.Get<IDataStorage<DirectoryItem>>().createDirectoryItemAsync();
            testDirectory = createDirectoryResult?.Result;

            testDirectory.FileType = FileTypeEnum.DIRECTORY;
            testDirectory.Name = "test4";
            testDirectory.Path = homeDirectoryPath;

            thisDirectoryListResult = DependencyService.Get<IDataStorage<DirectoryItem>>().AddDirectoryAsync(testDirectory);

            // ***********************
            // Create Test5 directory

            createDirectoryResult = DependencyService.Get<IDataStorage<DirectoryItem>>().createDirectoryItemAsync();
            testDirectory = createDirectoryResult?.Result;

            testDirectory.FileType = FileTypeEnum.DIRECTORY;
            testDirectory.Name = "test5";
            testDirectory.Path = homeDirectoryPath;

            thisDirectoryListResult = DependencyService.Get<IDataStorage<DirectoryItem>>().AddDirectoryAsync(testDirectory);

            //*********************************
            // Create sample subdirectories

            //****************************
            // Create TestA directory under Test1

            createDirectoryResult = DependencyService.Get<IDataStorage<DirectoryItem>>().createDirectoryItemAsync();
            testDirectory = createDirectoryResult?.Result;

            testDirectory.FileType = FileTypeEnum.DIRECTORY;
            testDirectory.Name = "testA";
            testDirectory.Path = Path.Combine(homeDirectoryPath, "test1");

            thisDirectoryListResult = DependencyService.Get<IDataStorage<DirectoryItem>>().AddDirectoryAsync(testDirectory);

            //****************************
            // Create TestA directory under Test1/TestA

            createDirectoryResult = DependencyService.Get<IDataStorage<DirectoryItem>>().createDirectoryItemAsync();
            testDirectory = createDirectoryResult?.Result;

            testDirectory.FileType = FileTypeEnum.DIRECTORY;
            testDirectory.Name = "testA";
            testDirectory.Path = Path.Combine(homeDirectoryPath, "test1");
            testDirectory.Path = Path.Combine(testDirectory.Path, "testA");

            thisDirectoryListResult = DependencyService.Get<IDataStorage<DirectoryItem>>().AddDirectoryAsync(testDirectory);


            // ***********************
            // Copy resource files from assembly to directories
            Task copyResourceResult = CopyResourceToDirectoryAsync("BrowseStorageXamarinForm.Resources.SampleFiles.1.html", Path.Combine(homeDirectoryPath, "1.html"));
            copyResourceResult = CopyResourceToDirectoryAsync("BrowseStorageXamarinForm.Resources.SampleFiles.2.html", Path.Combine(homeDirectoryPath, "2.html"));
            copyResourceResult = CopyResourceToDirectoryAsync("BrowseStorageXamarinForm.Resources.SampleFiles.3.html", Path.Combine(homeDirectoryPath, "3.html"));
            copyResourceResult = CopyResourceToDirectoryAsync("BrowseStorageXamarinForm.Resources.SampleFiles.camera.png", Path.Combine(homeDirectoryPath, "camera.png"));
            copyResourceResult = CopyResourceToDirectoryAsync("BrowseStorageXamarinForm.Resources.SampleFiles.Emoticon48dp.png", Path.Combine(homeDirectoryPath, "Emoticon48p.png"));
            copyResourceResult = CopyResourceToDirectoryAsync("BrowseStorageXamarinForm.Resources.SampleFiles.Emoticon96dp.png", Path.Combine(homeDirectoryPath, "Emoticon96p.png"));
            copyResourceResult = CopyResourceToDirectoryAsync("BrowseStorageXamarinForm.Resources.SampleFiles.Hart.png", Path.Combine(homeDirectoryPath, "Hart.png"));
            copyResourceResult = CopyResourceToDirectoryAsync("BrowseStorageXamarinForm.Resources.SampleFiles.IMG_0157.jpeg", Path.Combine(homeDirectoryPath, "IMG_0157.jpeg"));
            copyResourceResult = CopyResourceToDirectoryAsync("BrowseStorageXamarinForm.Resources.SampleFiles.IMG_0486.jpeg", Path.Combine(homeDirectoryPath, "IMG_0486.jpeg"));
            copyResourceResult = CopyResourceToDirectoryAsync("BrowseStorageXamarinForm.Resources.SampleFiles.IMG_0511.jpeg", Path.Combine(homeDirectoryPath, "IMG_0511.jpeg"));
            copyResourceResult = CopyResourceToDirectoryAsync("BrowseStorageXamarinForm.Resources.SampleFiles.IMG_1310.jpeg", Path.Combine(homeDirectoryPath, "IMG_1310.jpeg"));
            copyResourceResult = CopyResourceToDirectoryAsync("BrowseStorageXamarinForm.Resources.SampleFiles.IMG_1313.jpeg", Path.Combine(homeDirectoryPath, "IMG_1313.jpeg"));
            copyResourceResult = CopyResourceToDirectoryAsync("BrowseStorageXamarinForm.Resources.SampleFiles.IMG_1320.jpeg", Path.Combine(homeDirectoryPath, "IMG_1320.jpeg"));
            copyResourceResult = CopyResourceToDirectoryAsync("BrowseStorageXamarinForm.Resources.SampleFiles.IMG_1321.jpeg", Path.Combine(homeDirectoryPath, "IMG_1321.jpeg"));

            // Put sample file under test1
            copyResourceResult = CopyResourceToDirectoryAsync("BrowseStorageXamarinForm.Resources.SampleFiles.1.html", Path.Combine(homeDirectoryPath + Path.DirectorySeparatorChar + "test1", "1.html"));
            copyResourceResult = CopyResourceToDirectoryAsync("BrowseStorageXamarinForm.Resources.SampleFiles.2.html", Path.Combine(homeDirectoryPath + Path.DirectorySeparatorChar + "test1", "2.html"));
            copyResourceResult = CopyResourceToDirectoryAsync("BrowseStorageXamarinForm.Resources.SampleFiles.3.html", Path.Combine(homeDirectoryPath + Path.DirectorySeparatorChar + "test1", "3.html"));

            // Put sample file under test2
            copyResourceResult = CopyResourceToDirectoryAsync("BrowseStorageXamarinForm.Resources.SampleFiles.1.html", Path.Combine(homeDirectoryPath + Path.DirectorySeparatorChar + "test2", "1.html"));
            copyResourceResult = CopyResourceToDirectoryAsync("BrowseStorageXamarinForm.Resources.SampleFiles.2.html", Path.Combine(homeDirectoryPath + Path.DirectorySeparatorChar + "test2", "2.html"));

            // Put sample file under test3
            copyResourceResult = CopyResourceToDirectoryAsync("BrowseStorageXamarinForm.Resources.SampleFiles.1.html", Path.Combine(homeDirectoryPath + Path.DirectorySeparatorChar + "test3", "1.html"));
            copyResourceResult = CopyResourceToDirectoryAsync("BrowseStorageXamarinForm.Resources.SampleFiles.3.html", Path.Combine(homeDirectoryPath + Path.DirectorySeparatorChar + "test3", "3.html"));

            // Put sample files under Test1/TestA
            copyResourceResult = CopyResourceToDirectoryAsync("BrowseStorageXamarinForm.Resources.SampleFiles.2.html", Path.Combine(homeDirectoryPath + Path.DirectorySeparatorChar + "test1" + Path.DirectorySeparatorChar + "testA", "1.html"));
            copyResourceResult = CopyResourceToDirectoryAsync("BrowseStorageXamarinForm.Resources.SampleFiles.3.html", Path.Combine(homeDirectoryPath + Path.DirectorySeparatorChar + "test1" + Path.DirectorySeparatorChar + "testA", "2.html"));

            // Put a sample file under Test1/TestA/TestA
            copyResourceResult = CopyResourceToDirectoryAsync("BrowseStorageXamarinForm.Resources.SampleFiles.1.html", Path.Combine(homeDirectoryPath + Path.DirectorySeparatorChar + "test1" + Path.DirectorySeparatorChar + "testA" + Path.DirectorySeparatorChar + "testA", "1.html"));

        }

        public async Task CopyResourceToDirectoryAsync(string resourceName, string destinationFilePath)
        {
            try
            {
                var assembly = Assembly.GetExecutingAssembly();
                if ((resourceName != null) && (destinationFilePath != null))
                {
                    // Get the resource file stream from the assembly
                    using (Stream stream = assembly.GetManifestResourceStream(resourceName))
                    {
                        // Create the destination file or overwrite it and get a stream as handle
                        using (FileStream destinationStream = File.Create(destinationFilePath))
                        {
                            await stream.CopyToAsync(destinationStream);
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return;
            }
        }
    }
}
