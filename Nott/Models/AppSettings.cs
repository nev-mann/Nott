using System.Text.Json;

namespace Nott.Models
{
    public class AppSettings
    {
        public Settings settings;

        private string settingsPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "Nott.json");

        public AppSettings() {
            if (!File.Exists(settingsPath))
            {
                settings = new Settings
                {
                    SongsFolders = [
                        @"C:\a"
                        ],
                    Shuffle = true,
                };
                File.WriteAllText(settingsPath, JsonSerializer.Serialize(settings));
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

        public class Settings
        {
            public List<string> SongsFolders { get; set; }

            public bool Shuffle { get; set; }

        }
    }
    
}
