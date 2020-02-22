using BrowseStorageXamarinForm.Services;
using BrowseStorageXamarinForm.Views;
using Xamarin.Forms;

namespace BrowseStorageXamarinForm
{
    public partial class App : Application
    {

        public App()
        {
            InitializeComponent();

            DependencyService.Register<DataStorage>();

            new InitiateSampleData();

            MainPage = new NavigationPage(new MainPage());
        }



    protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
