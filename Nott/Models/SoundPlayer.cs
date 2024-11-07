using Nott.ViewModels;
using Plugin.Maui.Audio;

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

        public List<Song> SongQueue;
        public delegate void QueueEventHandler();
        public event QueueEventHandler OnChange;

        public SoundPlayer(IAudioManager am,AppSettings ap) {
            audioManager = am;
            appSettings = ap;
            SongQueue = appSettings.settings.Queue;
            Volume = appSettings.settings.Volume;
            OnChange = delegate { }; 
            Task.Run(Loop);
        }

        private async Task Loop()
        {
            while (true)
            {
                try
                {
                    if (audioPlayer != null && audioPlayer.CurrentPosition != 0)
                    {
                        MauiProgram.GetSongBarViewModel<SongBarViewModel>().Duration = (audioPlayer.CurrentPosition / audioPlayer.Duration);
                    }
                }
                catch 
                {
                    await Task.Delay(1);
                }
                await Task.Delay(1000);
            }
        }

        public void PlayAudio()
        {
            try
            {
                if (audioPlayer != null)
                {
                    audioPlayer.PlaybackEnded -= PlayNextInQueue;
                    audioPlayer.Stop();
                    musicFile.Dispose();
                }
                if (CurrentSong.Path is null) return;
                musicFile = File.Open(CurrentSong.Path, FileMode.Open);
                audioPlayer = audioManager.CreatePlayer(musicFile);

                audioPlayer.PlaybackEnded += new EventHandler(PlayNextInQueue);
                audioPlayer.Volume = Volume;
                audioPlayer.Play();
            }
            catch (Exception ex) { 
                //Breakpoint for debugging
                Task.Delay(10);
            }
            
        }

        private void PlayNextInQueue(object? sender, EventArgs e)
        {
            musicFile.Close();
            if (SongQueue.Count > 0)
            {
                CurrentSong = SongQueue[0];
                SongQueue.RemoveAt(0);
                if (OnChange != null) OnChange();
                PlayAudio();
            }
        }

        public void AddToQueue(Song song)
        {
            SongQueue.Add(song);
            OnChange();
        }
    }
}
