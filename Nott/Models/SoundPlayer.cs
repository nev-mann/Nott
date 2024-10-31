using Plugin.Maui.Audio;

namespace Nott.Models
{
    public class SoundPlayer
    {
        private FileStream musicFile;

        public string CurrentSong { get; set; }

        public bool IsPlaying { get; set; }

        private readonly IAudioManager audioManager;

        private IAudioPlayer audioPlayer;

        public SoundPlayer(IAudioManager audioManager) {
            this.audioManager = audioManager;
            IsPlaying = false;
            CurrentSong = "Test";
        }

        public async void PlayAudio()
        {            
            if (audioPlayer != null && audioPlayer.IsPlaying)
            {
                audioPlayer.Stop();
                musicFile.Close();
                return;
            }

            musicFile = File.Open(CurrentSong, FileMode.Open);
            audioPlayer = audioManager.CreatePlayer(musicFile);

            audioPlayer.Volume = 0.05f;
            audioPlayer.Play();
        }
    }
}
