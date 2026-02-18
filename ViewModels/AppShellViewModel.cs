using FinalApp.Models;
using FinalApp.Views;
using System.Windows.Input;

namespace FinalApp.ViewModels
{
    public class AppShellViewModel : ViewModelBase
    {
        public AppUser CurrentUser => (App.Current as App)?.CurrentUser;

        public bool IsAdmin => CurrentUser?.IsAdmin ?? false;

        public ICommand LogoutCommand { get; }
        public ICommand AdminLoginCommand { get; }
        public ICommand HomeCommand { get; }
        public ICommand AccountPageCommand { get; }

        public AppShellViewModel()
        {
            AdminLoginCommand = new Command(async () => await OpenAdminLogin());
            //AccountPageCommand = new Command(async () => await OpenAccountPage());
            LogoutCommand = new Command(async () => await Logout());
            HomeCommand = new Command(async () => await GoToHomePage());
            AccountPageCommand = new Command(async () => await GoToAccountPage());
        }
        private async Task OpenAdminLogin()
        {
            await Shell.Current.GoToAsync(nameof(AdminPage));
        }

        private async Task GoToAccountPage()
        {
            var user = (App.Current as App)?.CurrentUser;

            if (user != null)
            {
                var parameters = new Dictionary<string, object> { { "selectedUser", user } };
                await Shell.Current.GoToAsync(nameof(UserDetailsPage), parameters);
            }
        }
        private async Task GoToHomePage()
        {
            await Shell.Current.GoToAsync($"///{nameof(MainPage)}");
        }

        private async Task Logout()
        {
            var app = (App.Current as App);
            if (app != null)
            {
                app.CurrentUser = null;
                Application.Current.MainPage = new NavigationPage(new SignInPage(new SignInPageViewModel()));
            }
        }
    }
}