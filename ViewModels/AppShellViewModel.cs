using FinalApp.Service;
using FinalApp.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace FinalApp.ViewModels
{
    public class AppShellViewModel
    {
        private Page _signInPage;

        public ICommand LogoutCommand { get; }
        public AppShellViewModel(SignInPage signInPage)
        {
            LogoutCommand = new Command(async () => await Logout());

        }
        private async Task Logout()
        {
            (App.Current as App)!.CurrentUser = null;
            Application.Current!.Windows[0].Page = new NavigationPage(new SignInPage(new ViewModels.SignInPageViewModel()));
        }
    }
}
