using Microsoft.Extensions.Logging;
using Nott.Models;
using Nott.Views;
using Nott.ViewModels;
using Plugin.Maui.Audio;

namespace Nott
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
                });

            builder.Services.AddTransient<SongsViewModel>();
            builder.Services.AddTransient<SongsPage>(); 

            builder.Services.AddTransient<AlbumsViewModel>();
            builder.Services.AddTransient<AlbumsPage>();

            builder.Services.AddSingleton<SoundPlayer>();
            builder.AddAudio();

#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
