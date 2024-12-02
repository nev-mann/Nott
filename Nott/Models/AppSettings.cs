using CommunityToolkit.Mvvm.ComponentModel;
using System.Text.Json;

namespace Nott.Models
{
    public class AppSettings
    {
        public Settings settings;
        private SoundPlayer soundPlayer;
        private DatabaseHandler databaseHandler;

        private string settingsPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "Nott.json");

        public AppSettings(SoundPlayer sp, DatabaseHandler db)
        {
            soundPlayer = sp; databaseHandler = db;
            if (!File.Exists(settingsPath))
            {
                settings = new Settings(
                    [
                        //Environment.GetFolderPath(Environment.SpecialFolder.MyMusic),
                        //@"C:\a",
                        ],
                    true,
                    false,
                    0.15f,
                    []
                );
#if ANDROID
                    //var x = Android.App.Application.Context.GetExternalFilesDir(Android.OS.Environment.DirectoryDownloads).List();
#endif
                File.WriteAllText(settingsPath, JsonSerializer.Serialize(settings));
            }
            else
            {
                var jsonString = File.ReadAllText(settingsPath);
                settings = JsonSerializer.Deserialize<Settings>(jsonString) ?? new Settings([], true, false, 0.15f, []);
            }

            soundPlayer.repeat = settings.Repeat;
            soundPlayer.shuffle = settings.Shuffle;
            soundPlayer.songQueue = settings.Queue;
            soundPlayer.volume = settings.Volume;
        }

        public void Save()
        {
            settings.Repeat = soundPlayer.repeat;
            settings.Shuffle = soundPlayer.shuffle;
            settings.Queue = soundPlayer.songQueue;
            settings.Volume = soundPlayer.volume;
            File.WriteAllText(settingsPath, JsonSerializer.Serialize(settings));
        }
    }
    public class Settings
    {
        public List<string> SongsFolders { get; set; }
        public bool Shuffle { get; set; }
        public bool Repeat { get; set; }
        public double Volume { get; set; }
        public List<Song> Queue { get; set; }

        public Settings(List<string> SongsFolders, bool Shuffle, bool Repeat, double Volume, List<Song> Queue)
        {
            this.SongsFolders = SongsFolders;
            this.Shuffle = Shuffle;
            this.Repeat = Repeat;
            this.Volume = Volume;
            this.Queue = Queue;
        }
    }
}
