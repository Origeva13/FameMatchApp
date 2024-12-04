using FameMatchApp.Services;
using FameMatchApp.View;
using FameMatchApp.ViewModels;
using FameMatchApp.Views;
using Microsoft.Extensions.Logging;
using Microsoft.Win32;

namespace FameMatchApp
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {

            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                })
                .RegisterDataServices()
                .RegisterPages()
                .RegisterViewModels();

#if DEBUG
    		builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
        public static MauiAppBuilder RegisterPages(this MauiAppBuilder builder)
        {
            builder.Services.AddTransient<AppShell>();
            builder.Services.AddTransient<LogInView>();
            builder.Services.AddTransient<RegisterView>();
            builder.Services.AddTransient<CastedHomeView>();
            builder.Services.AddTransient<CastedHomeView>();
            builder.Services.AddTransient<CastedProfileView>();
            builder.Services.AddTransient<CastorProfileView>();
            builder.Services.AddTransient<ConfirmPageView>();
            builder.Services.AddTransient<MatchView>();
            builder.Services.AddTransient<UsersListView>();
            return builder;
        }

        public static MauiAppBuilder RegisterDataServices(this MauiAppBuilder builder)
        {
            builder.Services.AddSingleton<FameMatchWebAPIProxy>();
            return builder;
        }
        public static MauiAppBuilder RegisterViewModels(this MauiAppBuilder builder)
        {
            builder.Services.AddTransient<LoginVIewModel>();
            builder.Services.AddTransient<RegisterViewModel>();
            builder.Services.AddTransient<AppShellViewModel>();
            builder.Services.AddTransient<CastedHomeViewModel>();
            builder.Services.AddTransient<CastedProfileViewModel>();
            builder.Services.AddTransient<CastorHomeViewModel>();
            builder.Services.AddTransient<CastorProfileViewModel>();
            builder.Services.AddTransient<ConfirmPageViewModel>();
            builder.Services.AddTransient<MatchViewModel>();
            builder.Services.AddTransient<UsersListViewModel>();
            return builder;
        }
    }
}
