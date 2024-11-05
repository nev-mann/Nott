using System.Text.Json;

namespace Nott.Models
{
    public class AppSettings
    {
        public Settings settings;

        private string settingsPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "Nott.json");

        public AppSettings() 
        {
            if (!File.Exists(settingsPath))
            {
                settings = new Settings
                {
                    SongsFolders = [
                        //Environment.GetFolderPath(Environment.SpecialFolder.MyMusic),
                        //@"C:\a",
                        ],
                    Shuffle = true,
                    Volume = 0.15f,
                    Queue = []
                };
#if ANDROID
                var x = Android.App.Application.Context.GetExternalFilesDir(Android.OS.Environment.DirectoryDownloads).List();
#endif
                File.WriteAllText(settingsPath, JsonSerializer.Serialize(settings));
            }
            else
            {
                var jsonString = File.ReadAllText(settingsPath);
                settings = (Settings?)JsonSerializer.Deserialize(jsonString, typeof(Settings));
            }
        }

        public void Save() 
        {
            settings.Queue = MauiProgram.GetSoundPlayer<SoundPlayer>().SongQueue;
            File.WriteAllText(settingsPath, JsonSerializer.Serialize(settings));
        }

        public class Settings
        {
            public List<string> SongsFolders { get; set; }

            public bool Shuffle { get; set; }

            public double Volume { get; set; }  
            public Queue<Song> Queue { get; set; }
        }
    }
    
}
