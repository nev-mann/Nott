using Microsoft.Maui.Storage;
using System.Text.Json;

namespace Nott.Models
{
    public class AppSettings
    {
        private Settings settings { get; set; }

        private string settingsPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "Nott.json");

        public AppSettings() {
            if (!File.Exists(settingsPath))
            {
                var js = new Settings
                {
                    SongsFolders = [
                        Environment.GetFolderPath(Environment.SpecialFolder.MyMusic)
                        ],
                    Shuffle = true,
                };
                File.WriteAllText(settingsPath, JsonSerializer.Serialize(js));
            }
            else
            {
                var jsonString = File.ReadAllText(settingsPath);
                settings = (Settings?)JsonSerializer.Deserialize(jsonString, typeof(Settings));
            }
        }

        public void Save() {
            File.WriteAllText(settingsPath, JsonSerializer.Serialize(settings));
        }

        public bool getShuffle()
        {
            return settings.Shuffle;
        }

        public void setShuffle(bool s)
        {
            settings.Shuffle = s;
        }

        private class Settings
        {
            public List<string> SongsFolders { get; set; }

            public bool Shuffle { get; set; }

        }
    }
    
}
