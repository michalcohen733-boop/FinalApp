using FinalApp.ViewModels;

namespace FinalApp.Views;

public partial class SignInPage : ContentPage
{
	public SignInPage(SignInPageViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;
		vm.Navigation = this.Navigation;
    }
    //protected override void OnAppearing()
    //{
    //    base.OnAppearing();
    //    if (BindingContext is SignInPageViewModel vm) 
    //    {
    //        vm.OnAppearing();
    //    }
    //}
}