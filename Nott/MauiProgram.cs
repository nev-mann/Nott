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

            builder.Services.AddTransient<SettingsViewModel>();
            builder.Services.AddTransient<SettingsPage>();

            builder.Services.AddSingleton<SoundPlayer>();
            builder.Services.AddSingleton<AppSettings>();
            builder.AddAudio();

            //File.Delete(@"C:\Users\kamne\AppData\Local\Packages\com.companyname.nott_9zz4h110yvjzm\LocalCache\Roaming\Nott.json");
            //File.Delete(@"C:\Users\kamne\AppData\Local\Packages\com.companyname.nott_9zz4h110yvjzm\LocalCache\Roaming\Nott.db");

#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
