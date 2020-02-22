using Xamarin.Forms;
using BrowseStorageXamarinForm.Models;

namespace BrowseStorageXamarinForm.Services
{
    public class BrowseStorageDataTemplateSelector : DataTemplateSelector
    {
        public DataTemplate DirectoryItemTemplate { get; set; }
        public DataTemplate FileItemTemplate { get; set; }

        protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
        {
            return ((DirectoryItem)item).FileType == FileTypeEnum.DIRECTORY ? DirectoryItemTemplate : FileItemTemplate;
        }

    }
}
