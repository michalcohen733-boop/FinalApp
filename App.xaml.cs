using FinalApp.Models;
using FinalApp.Views;
namespace FinalApp
{
    public partial class App : Application
    {
        public AppUser? CurrentUser { get; set; } = null;

        public bool IsDebugMode { get; set; } = false;
        public App()
        {
            InitializeComponent();
        }

        protected override Window CreateWindow(IActivationState? activationState)
        {
            if ((App.Current as App)!.IsDebugMode) //if debug mode is on
            {
                //Navigate to MainPage
                AppUser testUser = new AppUser()
                {

                    UserEmail = "Kostya",
                    UserPassword = "a",
                    IsAdmin = true,
                };

                (App.Current as App)!.CurrentUser = testUser;
                return new Window(new AppShell());

            }
            return new Window(new NavigationPage(new SignInPage(new ViewModels.SignInPageViewModel())));
        }
    }
}