namespace FinalApp.Views;
using FinalApp.ViewModels;

public partial class UsersListPage : ContentPage
{
	public UsersListPage()
	{
		InitializeComponent();
        BindingContext = new UsersListViewModel();
        Routing.RegisterRoute(nameof(UserDetailsPage), typeof(UserDetailsPage));


    }
    protected override void OnAppearing()
    {
        base.OnAppearing();
        // Optionally, you can call a method to load data when the page appears
        if (BindingContext is UsersListViewModel vm)
        {
            vm.RefreshList(); // ????? ???? ?? ??????? ??? ??? ???? ????
        }
    }
}