using FinalApp.ViewModels;

namespace FinalApp.Views;

public partial class UserDetailsPage : ContentPage
{
    public UserDetailsPage(UserDetailsPageViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
    public UserDetailsPage()
    {
        InitializeComponent();
        BindingContext = Handler?.MauiContext?.Services.GetService<UserDetailsPageViewModel>()
                         ?? new UserDetailsPageViewModel();
    }
}