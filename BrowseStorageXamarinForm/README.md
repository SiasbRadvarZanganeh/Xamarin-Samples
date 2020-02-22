### **Introduction**
This is a sample code written in C# that uses **Xamarin.Forms** and **.NET** functionality on the [**Xamarin Platform**] (https://dotnet.microsoft.com/apps/xamarin) to browse the application storage on the iPhone or the Android device. It allows the programmer to select the file in an application.

- I have written this code and publish it as open source so others could also use it if they want. Also, some people wanted to see my code, since I usually don’t publish open source code. It is written to be useful. It is not necessarily complete, It is meant as code snippet to be integrated into your code if you want.

- I have written it according to View-ModelView-Model concepts in Xamarin.Form. The code behind the XAML in the View, only Initializes the Components of the XAML pages.

- I have not written any Unit tests for this project.

- I have tested the application on the:
    * iPhone SE (IOS 13.3.1)
    * Simulator: iPhone 11 Pro Max (IOS 13.3)
    * SKY 4.0D (Android 4.4.2) - Android 4.4.2 Kit Kat. API level 19 (the target is set to Android 8.1, API level 27)
    * Emulator: Android_27 (API 27)

- Information on [**file system access in Xamarin:**](https://docs.microsoft.com/en-us/xamarin/ios/app-fundamentals/file-system)

>### **IOS setup**
You need to add the following 2 properties to the **info.plist** in the IOS section of your project.

>- **LSSupportsOpeningDocumentsInPlace** - **Yes**  :  If you want to see your files in the Files App
- **UIFileSharingEnabled** - **Yes** : Application supports iTunes sharing

### Features
- Uses CollectionView.
- Uses only Xamarin.Forms, and .net file access.
 - One directory listing per page.
 - Folder button of a directory in a listing leads to a new page with that sub-directory listings.
 - A path of each directory listing at the top of the page.
 - Navigation title shows the directory name on a sub-directory listing.
 - Android tabs at the bottom of the page are implemented.
 - DataTemplet selection according to file or directory is implemented.
 - File and directory names are sorted according to name.
 - Consistent colour thyme is used for legibility.
 - A consistent material design for application was done. (Used Sketch)
 - Sample data files are part of resources of the application. They are extracted at start of the application for test.


### Not implemented
- Thumbnail of files are not implemented.
- Sorting of files according to other criteria like creation time is not implemented.
- Multi directory items in columns on page display. With Collection view it should be easy.
- A SearchBar is not implemented to filter the directories and files in a directory listing.

### Application Screen Recordings
- [iPhone Screen Recording](https://github.com/SiasbRadvarZanganeh/Xamarin-Samples/tree/master/BrowseStorageXamarinForm/Assets/AppScreenRecordings/IOS.gif)
- [Android Screen Recording](https://github.com/SiasbRadvarZanganeh/Xamarin-Samples/tree/master/BrowseStorageXamarinForm/Assets/AppScreenRecordings/Android.gif)


