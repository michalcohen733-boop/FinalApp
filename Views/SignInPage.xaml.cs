using FinalApp.ViewModels;

namespace FinalApp.Views;

public partial class SignInPage : ContentPage
{
	public SignInPage()
	{
		InitializeComponent();
        BindingContext = new SignInPageViewModel();

    }
}