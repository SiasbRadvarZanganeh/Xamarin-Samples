using System;
using System.IO;
using System.Collections.Generic;
using System.Collections.ObjectModel;

using System.Windows.Input;
using System.Threading.Tasks;

using BrowseStorageXamarinForm.Models;
using BrowseStorageXamarinForm.Services;
using BrowseStorageXamarinForm.Views;

using Xamarin.Forms;

namespace BrowseStorageXamarinForm.ViewModels
{
    // Structure to keep the directory page to directory view model maps
    public class PageViewModel
    {
        public string DirectoryPath { get; set; }
        public string DirectoryName { get; set; }
        public BrowseStoragePage DirectoryPage { get; set; }
        public BrowseStorageViewModel ViewModel { get; set; }
        public ObservableCollection<DirectoryItem> DirectoryItemList;
    }

    public class BrowseStorageViewModel : BaseViewModel
    {

        public static Dictionary<string, PageViewModel> pageViewModels = null;


        public List<DirectoryItem> thisDirectoryList;

        public DirectoryItem selectedItem;
        public DirectoryItem SelectedItem
        {
            get
            {
                return selectedItem;
            }
            set
            {
                if (selectedItem != value)
                {
                    selectedItem = value;
                }
            }
        }
        public DirectoryItem previousItemSelected;


        public BrowseStoragePage currentPageOnDisplay = null;

        public string DirectoryPath { get; set; }
        public string SelectedDirectoryItemMessage { get; set; }

        public ObservableCollection<DirectoryItem> DirectoryItems { get; private set; }

//        public List<DirectoryItem> EmptyDirectoryItems { get; private set; }


        public ICommand FolderBrowseSubDirectoryButtonCommand { get; private set; }
        public ICommand DirectoryItemSelectionChangedCommand { get; private set; }


        public BrowseStorageViewModel()
        {
            try
            {
                 FolderBrowseSubDirectoryButtonCommand = new Command<object>(BrowseSubFolder);
                 DirectoryItemSelectionChangedCommand = new  Command(DirectoryItemSelectionChanged);

                pageViewModels = (pageViewModels != null) ? pageViewModels : new Dictionary<string, PageViewModel>();

                // Create a directory item, if none was passed to the constructor
                Task<DirectoryItem> createDirectoryItemResult = DependencyService.Get<IDataStorage<DirectoryItem>>().createDirectoryItemAsync();
                DirectoryItem directoryItem = createDirectoryItemResult?.Result;

                // Set current directory path as root
                Task<string> homeDirectoryPathResult = DependencyService.Get<IDataStorage<DirectoryItem>>().GetDirectoryRoot();

                directoryItem.Name = null;
                directoryItem.Path = homeDirectoryPathResult?.Result;
                directoryItem.FileType = FileTypeEnum.DIRECTORY;

                DirectoryPath = directoryItem.Path;

                SelectedDirectoryItemMessage = string.Empty;

                InitBrowseStorgeViewModel(directoryItem);

                //------------------------------------

                var pageViewModel = new PageViewModel
                {
                    DirectoryName = "ApplicationRoot",
                    DirectoryPath = directoryItem.Path,
                    DirectoryItemList = this.DirectoryItems,
                    ViewModel = this
                };

                pageViewModels.Add(directoryItem.Path, pageViewModel);

                return;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return;
            }

        }

        public void InitBrowseStorgeViewModel(DirectoryItem directoryItem)
        {
            try
            {

                thisDirectoryList = new List<DirectoryItem>();

                // Get directories
                if (directoryItem != null)
                {

                    Task<IEnumerable<DirectoryItem>> thisDirectoryListResult = DependencyService.Get<IDataStorage<DirectoryItem>>().GetSubDirectories(directoryItem);
                    var resultItems = thisDirectoryListResult?.Result;

                    foreach (var item in resultItems)
                    {
                        thisDirectoryList.Add(item);
                    }

                }
                // Get files
                if (directoryItem != null)
                {

                    Task<IEnumerable<DirectoryItem>> thisDirectoryListResult = DependencyService.Get<IDataStorage<DirectoryItem>>().GetFilesInDirectory(directoryItem);
                    var resultItems = thisDirectoryListResult?.Result;

                    foreach (var item in resultItems)
                    {
                        thisDirectoryList.Add(item);
                    }
                }


                //            if (sortByName) // This should be the page option
                //            {
                thisDirectoryList.Sort(delegate (DirectoryItem directoryItemA,
                                                 DirectoryItem directoryItemB)
                {
                    if ((directoryItemA.Name == null) && (directoryItemB.Name == null)) return 0;
                    else if (directoryItemA.Name == null) return -1;
                    else if (directoryItemB.Name == null) return 1;
                    else return (directoryItemA.Name.CompareTo(directoryItemB.Name));

                });

                DirectoryItems = new ObservableCollection<DirectoryItem>(thisDirectoryList);

                return;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return;
            }


        }

        async void BrowseSubFolder(object arg)
        {
            try
            {
                if (arg != null)
                {
                    SelectedDirectoryItemMessage = string.Empty;
                    await BrowseSubFolderAsync(arg);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return;
            }
        }

        async Task BrowseSubFolderAsync(object arg)
        {
            try
            {
                DirectoryItem directoryItemArg = (DirectoryItem)arg;

                string subDirectoryName = directoryItemArg.Name;
                string subDirectoryPath = directoryItemArg.Path;

                BrowseStorageViewModel subDirectoryViewModel = new BrowseStorageViewModel();

                DirectoryPath = Path.Combine(subDirectoryPath, subDirectoryName);

                // Create a directory item, if none was passed to the constructor
                Task<DirectoryItem> createDirectoryItemResult = DependencyService.Get<IDataStorage<DirectoryItem>>().createDirectoryItemAsync();
                DirectoryItem directoryItem = createDirectoryItemResult?.Result;

                directoryItem.Name = subDirectoryName;
                directoryItem.Path = subDirectoryPath;
                directoryItem.FileType = FileTypeEnum.DIRECTORY;

                InitBrowseStorgeViewModel(directoryItem);


                SelectedDirectoryItemMessage = subDirectoryName;

                SelectedItem = null;

                subDirectoryViewModel = this;

                subDirectoryPath = DirectoryPath;

                BrowseStoragePage subDirectoryStoragePage;

                if (!pageViewModels.ContainsKey(subDirectoryPath)) // If the subdirectory has not been viewed before
                {
                    PageViewModel pageViewModel = new PageViewModel
                    {
                        DirectoryName = directoryItem.Name,
                        DirectoryPath = directoryItem.Path,
                        ViewModel = subDirectoryViewModel,
                        DirectoryItemList = subDirectoryViewModel.DirectoryItems
                    };

                    subDirectoryStoragePage = new BrowseStoragePage
                    {
                        Title = directoryItem.Name,
                        BindingContext = subDirectoryViewModel
                    };

                    pageViewModel.DirectoryPage = subDirectoryStoragePage;

                    pageViewModels.Add(subDirectoryPath, pageViewModel);
                }
                else
                {
                    PageViewModel pageViewModel = pageViewModels[subDirectoryPath];
                    subDirectoryStoragePage = pageViewModel.DirectoryPage;
                }

                await Application.Current.MainPage.Navigation.PushAsync(subDirectoryStoragePage);

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return;
            }
        }

        async void DirectoryItemSelectionChanged()
        {
            try
            {
                if (selectedItem != null)
                {
                    if (selectedItem?.FileType != FileTypeEnum.DIRECTORY) // Selection is a file
                    {
                        if ((pageViewModels != null) && (pageViewModels.ContainsKey(selectedItem?.Path)))
                        {
                            PageViewModel displayedPageViewModel = pageViewModels[selectedItem?.Path];

                            displayedPageViewModel.ViewModel.SelectedDirectoryItemMessage = selectedItem?.Name;

                            Console.WriteLine("Selected File =" + selectedItem?.Name);
                        }
                    }
                    else // Selection is a directory
                    {
                        if ((previousItemSelected != null) && (previousItemSelected != selectedItem))
                        {
                            Console.WriteLine("Selected Directory =" + selectedItem?.Name);

                            previousItemSelected = selectedItem;
                            await BrowseSubFolderAsync(selectedItem);
                        }
                    }
                    previousItemSelected = selectedItem;
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
