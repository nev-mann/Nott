using Nott.Source;
using Nott.ViewModels;
using Plugin.Maui.Audio;
using static SQLite.TableMapping;

namespace Nott.Models
{
    public class SoundPlayer
    {
        private readonly IAudioManager audioManager;
        private IAudioPlayer audioPlayer;
        private FileStream musicFile;
        private AppSettings appSettings;

        private double _volume;
        public double Volume {
            get { return _volume; }
            set {
                _volume = value;
                appSettings.settings.Volume = value;
                if (audioPlayer != null)
                {
                    audioPlayer.Volume = value;
                }
            } 
        }

        public Song CurrentSong;

        public Queue<Song> SongQueue;

        public SoundPlayer(IAudioManager am,AppSettings ap) {
            audioManager = am;
            appSettings = ap;
            SongQueue = [];
            Volume = appSettings.settings.Volume;
        }

        public void PlayAudio()
        {            
            if (audioPlayer != null && audioPlayer.IsPlaying)
            {
                audioPlayer.PlaybackEnded -= PlayNextInQueue;
                audioPlayer.Dispose();
                musicFile.Dispose();
            }

            musicFile = File.Open(CurrentSong.Path, FileMode.Open);
            audioPlayer = audioManager.CreatePlayer(musicFile);

            audioPlayer.PlaybackEnded += new EventHandler(PlayNextInQueue);

            audioPlayer.Volume = Volume;
            audioPlayer.Play();
        }

        private void PlayNextInQueue(object? sender, EventArgs e)
        {
            musicFile.Close();
            if (SongQueue.Count > 0)
            {
                CurrentSong = SongQueue.Dequeue();
                PlayAudio();
            }
        }

        public void AddToQueue(Song song)
        {
            SongQueue.Enqueue(song);
        }

    }
}
