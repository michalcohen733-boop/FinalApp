using CommunityToolkit.Maui;
using FinalApp.ViewModels;
using FinalApp.Views;
using Microsoft.Extensions.Logging;

namespace FinalApp
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .UseMauiCommunityToolkit()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                    fonts.AddFont("MaterialSymbolsOutlined.ttf", "MyMaterialSymbols");

                });
            builder.Services.AddTransient<UserDetailsPage>();
            builder.Services.AddTransient<UserDetailsPageViewModel>();
            builder.Services.AddTransient<UsersListPage>();
            builder.Services.AddTransient<UsersListViewModel>();
            builder.Services.AddTransient<AdminPage>();
            builder.Services.AddTransient<AdminPageViewModel>();
            builder.Services.AddTransient<SignInPage>();
            builder.Services.AddTransient<SignInPageViewModel>();

#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
