
using System.ComponentModel;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace BrowseStorageXamarinForm.ViewModels
{
    public class AboutViewModel : BaseViewModel
    {
        public AboutViewModel()
        {
            Title = "About";
            OpenWebCommand = new Command(async () => await Browser.OpenAsync("https://github.com/SiasbRadvarZanganeh/Xamarin-Samples")); // link to Github page
        }

        public ICommand OpenWebCommand { get; }
    }
}