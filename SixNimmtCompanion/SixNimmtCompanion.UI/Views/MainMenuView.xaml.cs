using Microsoft.Toolkit.Uwp.UI.Extensions;
using SixNimmtCompanion.UI.ViewModels;
using Windows.Foundation.Metadata;
using Windows.UI.Core;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace SixNimmtCompanion.UI.Views
{
    public sealed partial class MainMenuView : Page
    {
        public MainMenuView()
        {
            InitializeComponent();

            if (ApiInformation.IsApiContractPresent("Windows.Phone.PhoneContract", 1, 0))
            {
                StatusBar.SetIsVisible(this, false);
            }

            DataContext = new MainMenuViewModel();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            SystemNavigationManager systemNavigationManager = SystemNavigationManager.GetForCurrentView();
            systemNavigationManager.BackRequested += MainMenuView_BackRequested;
            systemNavigationManager.AppViewBackButtonVisibility = AppViewBackButtonVisibility.Visible;
        }

        private void MainMenuView_BackRequested(object sender, BackRequestedEventArgs e)
        {
            e.Handled = true;
            App.Current.Exit();
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            base.OnNavigatedFrom(e);

            SystemNavigationManager systemNavigationManager = SystemNavigationManager.GetForCurrentView();
            systemNavigationManager.BackRequested -= MainMenuView_BackRequested;
            systemNavigationManager.AppViewBackButtonVisibility = AppViewBackButtonVisibility.Collapsed;
        }
    }
}
