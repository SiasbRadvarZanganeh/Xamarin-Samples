using System;

namespace BrowseStorageXamarinForm.Models
{
    public enum DriveTypeEnum { LOCAL, CLOUD, SD } // ...
    public enum DirectoryTypeEnum { DOCUMENT, LIBRARY, CACHE, TMP } // ... CACHE, TMP do not get backedup
    public enum FileTypeEnum { DIRECTORY, PNG, HTML, JPG, TXT } // ...


    public class DirectoryItem // : IEquatable<DirectoryItem>
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public FileTypeEnum FileType { get; set; } // "DIRECTORY", "PNG", "HTML", "JPG", "TXT" ...
        public string Path { get; set; }
        public DateTime LastAccessTime { get; set; }
        public byte[] fileAsBytes;
    }
}