using Plugin.Maui.Audio;
using System.Diagnostics;

namespace Nott.Models
{
    public class SoundPlayer(IAudioManager am)
    {
        private readonly IAudioManager audioManager = am;
        public IAudioPlayer? audioPlayer;

        public double volume;
        public int position = 0;
        public bool shuffle;
        public bool repeat;

        public Song? currentSong;

        public List<Song> songQueue = [];

        public delegate void EventHandler();
        public event EventHandler QueueChange = delegate { };
        public event EventHandler PlaybackStarted = delegate { };

        public void PlayAudio()
        {
            try
            {
                if (audioPlayer != null)
                {
                    audioPlayer.PlaybackEnded -= PlaybackEnded;
                    audioPlayer.Stop();
                    audioPlayer.Dispose();
                }
                if (currentSong is null) return;
                audioPlayer = audioManager.CreatePlayer(new MemoryStream(File.ReadAllBytes(currentSong.Path)));

                audioPlayer.PlaybackEnded += PlaybackEnded;
                audioPlayer.Volume = volume;
                audioPlayer.Play();
                PlaybackStarted?.Invoke();
            }
            catch (Exception ex)
            {
                audioPlayer = null;                
                //Breakpoint for debugging
                Debug.WriteLine(ex.Message);
                Task.Delay(10);
            }

        }
        public void PauseAudio() => audioPlayer?.Pause();
        public void ResumeAudio() => audioPlayer?.Play();
        private void PlaybackEnded(object? sender, EventArgs e)
        {
            if(currentSong is null) return;
            //updates times listened
            var db = new DatabaseHandler();
            currentSong.TimesListened += 1;
            db.UpdateSong(currentSong);


            //play next song
            position++;
            if (songQueue.Count > 0)
            {
                currentSong = songQueue[position];
                //QueueChange?.Invoke();
                PlayAudio();
            }
        }
        public void AddToQueue(Song song)
        {
            songQueue.Add(song);
            QueueChange();
        }
        public void AddToQueue(List<Song> songs)
        {
            songQueue = songs;
            QueueChange();
        }
        public void SetVolume(double v)
        {
            volume = v;
            if (audioPlayer != null)
            {
                audioPlayer.Volume = v;
            }
        }
    }
}
