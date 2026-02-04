using FinalApp.ViewModels;

namespace FinalApp.Views;

public partial class SignUpPage : ContentPage
{
	public SignUpPage()
	{
		InitializeComponent();
        BindingContext = new SignUpPageViewModel();
        //vm.Navigation = this.Navigation;

    }
}