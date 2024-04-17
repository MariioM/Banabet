using Banabet.Services;
using Banabet.ViewModel;
using Microsoft.Extensions.Logging;

namespace BanaBet
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
                    fonts.AddFont("Poppins-Regular.ttf", "Poppins");
                    fonts.AddFont("Poppins-Medium.ttf", "PoppinsMedium");
                    fonts.AddFont("Poppins-SemiBold.ttf", "PoppinsSemi");
                    fonts.AddFont("Poppins-Bold.ttf", "PoppinsBold");
                });
            //Singleton es para crear una copia
            builder.Services.AddSingleton<MainPage>();
            builder.Services.AddSingleton<MainViewModel>();

            builder.Services.AddSingleton<RegisterPage>();
            builder.Services.AddSingleton<RegisterViewModel>();
            builder.Services.AddSingleton<PanicPage>();
            builder.Services.AddSingleton<PanicViewModel>();
            builder.Services.AddTransient<FormPage1>();
            builder.Services.AddTransient<FormPage2>();
            builder.Services.AddTransient<FormPage3>();
            builder.Services.AddTransient<FormPage4>();
            builder.Services.AddTransient<FormPage5>();

            builder.Services.AddTransient<MainPage>();
            builder.Services.AddTransient<RegisterPage>();
            builder.Services.AddTransient<LoadingPage>();
            builder.Services.AddTransient<AuthService>();
            builder.Services.AddTransient<ProfilePage>();
            builder.Services.AddTransient<LoginPage>();

            return builder.Build();
        }
    }
}
