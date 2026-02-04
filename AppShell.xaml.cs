using FinalApp.Views;
using FinalApp.ViewModels;
namespace FinalApp
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            BindingContext = new AppShellViewModel(new SignInPage(new SignInPageViewModel()));
            //Routing.RegisterRoute("SignUpPage", typeof(SignUpPage));
            //Routing.RegisterRoute("SignUpPage", typeof(SignInPage));


        }
    }
}
