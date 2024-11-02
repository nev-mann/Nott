using Nott.Source;
using Plugin.Maui.Audio;

namespace Nott.Models
{
    public class SoundPlayer
    {
        private readonly IAudioManager audioManager;
        private IAudioPlayer audioPlayer;
        private FileStream musicFile;

        public Song CurrentSong { get; set; }

        public Queue<Song> SongQueue = [];

        public SoundPlayer(IAudioManager audioManager) {
            this.audioManager = audioManager;
        }

        public void PlayAudio()
        {            
            if (audioPlayer != null && audioPlayer.IsPlaying)
            {
                audioPlayer.Stop();
                musicFile.Close();
            }

            musicFile = File.Open(CurrentSong.Path, FileMode.Open);
            audioPlayer = audioManager.CreatePlayer(musicFile);

            audioPlayer.PlaybackEnded += new EventHandler(PlayNextInQueue);

            audioPlayer.Volume = 0.05f;
            audioPlayer.Play();
        }

        private void PlayNextInQueue(object? sender, EventArgs e)
        {
            CurrentSong = SongQueue.Dequeue();
            PlayAudio();
        }

        public void AddToQueue(Song song)
        {
            SongQueue.Enqueue(song);
        }
    }
}
