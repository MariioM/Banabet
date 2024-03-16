using BanaBet.ViewModel;
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
            return builder.Build();
        }
    }
}
