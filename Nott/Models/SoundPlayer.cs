using Plugin.Maui.Audio;

namespace Nott.Models
{
    public class SoundPlayer
    {
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
                audioPlayer = null;
                return;
            }

            audioPlayer = audioManager.CreatePlayer(File.Open(CurrentSong,FileMode.Open));

            audioPlayer.Volume = 0.05f;
            audioPlayer.Play();
        }
    }
}
