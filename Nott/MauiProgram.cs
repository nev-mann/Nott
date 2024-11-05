using Microsoft.Extensions.Logging;
using Nott.Models;
using Nott.Views;
using Nott.ViewModels;
using Plugin.Maui.Audio;
using CommunityToolkit.Maui;
using Nott.Views.Controls;

namespace Nott
{
    public static class MauiProgram
    {
        static IServiceProvider serviceProvider;
        public static SoundPlayer? GetSoundPlayer<SoundPlayer>()
            => serviceProvider.GetService<SoundPlayer>();
        public static SongBarViewModel? GetSongBarViewModel<SongBarViewModel>()
            => serviceProvider.GetService<SongBarViewModel>();

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
                });

            builder.Services.AddTransient<SongsViewModel>();
            builder.Services.AddTransient<SongsPage>(); 

            builder.Services.AddSingleton<AlbumsViewModel>();
            builder.Services.AddSingleton<AlbumsPage>();

            builder.Services.AddTransient<SettingsViewModel>();
            builder.Services.AddTransient<SettingsPage>();

            builder.Services.AddSingleton<QueueViewModel>();
            builder.Services.AddSingleton<QueuePage>();

            builder.Services.AddSingleton<SongBarViewModel>();
            builder.Services.AddSingleton<SongBarView>();

            builder.Services.AddSingleton<SoundPlayer>();
            builder.Services.AddSingleton<AppSettings>();
            builder.Services.AddSingleton<DatabaseHandler>();
            builder.AddAudio();

            File.Delete(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "Nott.json"));
            //File.Delete(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "Nott.db"));

#if DEBUG
            builder.Logging.AddDebug();
#endif
            var app= builder.Build();
            serviceProvider = app.Services;
            return app;
        }
    }
}
