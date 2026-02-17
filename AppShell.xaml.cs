using FinalApp.Views;
using FinalApp.ViewModels;

namespace FinalApp
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();


            BindingContext = new AppShellViewModel();

            Routing.RegisterRoute(nameof(UserDetailsPage), typeof(UserDetailsPage));
            Routing.RegisterRoute(nameof(AdminPage), typeof(AdminPage));
            Routing.RegisterRoute(nameof(UsersListPage), typeof(UsersListPage));
        }
    }
}