using CommunityToolkit.Maui;
using Microsoft.Extensions.Logging;
using Nott.Models;
using Nott.ViewModels;
using Nott.Views;
using Nott.Views.Controls;
using Plugin.Maui.Audio;

namespace Nott
{
    public static class MauiProgram
    {
        static IServiceProvider? serviceProvider;
        public static SongBarViewModel GetSongBarViewModel()
        {
            return serviceProvider?.GetService<SongBarViewModel>()!;
        }

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

            builder.Services.AddSingleton<SongsViewModel>();
            builder.Services.AddSingleton<SongsPage>();

            builder.Services.AddSingleton<AlbumsViewModel>();
            builder.Services.AddSingleton<AlbumsPage>();

            builder.Services.AddSingleton<SettingsViewModel>();
            builder.Services.AddSingleton<SettingsPage>();

            builder.Services.AddSingleton<QueueViewModel>();
            builder.Services.AddSingleton<QueuePage>();

            builder.Services.AddTransient<AddToPlaylistViewModel>();
            builder.Services.AddTransient<AddToPlaylistView>();

            builder.Services.AddSingleton<SongBarViewModel>();
            builder.Services.AddSingleton<SongBarView>();

            builder.Services.AddSingleton<PlaylistsViewModel>();
            builder.Services.AddSingleton<PlaylistsPage>();

            builder.Services.AddSingleton<SoundPlayer>();
            builder.Services.AddSingleton<AppSettings>();
            builder.Services.AddSingleton<DatabaseHandler>();
            builder.AddAudio();


            //File.Delete(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "Nott.json"));
            //File.Delete(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "Nott.db"));

#if DEBUG
            builder.Logging.AddDebug();
#endif
            var app = builder.Build();
            serviceProvider = app.Services;
            return app;
        }
    }
}
