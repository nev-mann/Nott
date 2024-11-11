using CommunityToolkit.Maui.Core.Extensions;
using CommunityToolkit.Mvvm.ComponentModel;
using Nott.ViewModels;
using Plugin.Maui.Audio;
using System;

namespace Nott.Models
{
    public class SoundPlayer
    {
        private readonly IAudioManager audioManager;
        private IAudioPlayer? audioPlayer;

        public double volume;
        public int position;
        public bool shuffle;
        public bool repeat;

        public Song? currentSong;

        public List<Song> songQueue;

        public delegate void QueueEventHandler();
        public event QueueEventHandler OnChange;

        public SoundPlayer(IAudioManager am) {
            audioManager = am;
            OnChange = delegate { }; 
            position = 0;
            songQueue = [];
            Task.Run(UpdateDuration);
        }
        private async Task UpdateDuration()
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
                await Task.Delay(300);
            }
        }
        public void PlayAudio()
        {
            try
            {
                if (audioPlayer != null && audioPlayer.IsPlaying)
                {
                    audioPlayer.PlaybackEnded -= PlayNextInQueue;
                    audioPlayer.Stop();                    
                    audioPlayer.Dispose();
                }                
                if (currentSong is null) return;           
                audioPlayer = audioManager.CreatePlayer(new MemoryStream(File.ReadAllBytes(currentSong.Path)));

                audioPlayer.PlaybackEnded += new EventHandler(PlayNextInQueue);
                audioPlayer.Volume = volume;
                audioPlayer.Play();
            }
            catch (Exception ex) { 
                //Breakpoint for debugging
                Task.Delay(10);
            }
            
        }
        public void PauseAudio() => audioPlayer?.Pause();
        public void ResumeAudio() => audioPlayer?.Play();
        private void PlayNextInQueue(object? sender, EventArgs e)
        {
            position++;
            if (songQueue.Count > 0)
            {
                currentSong = songQueue[position];
                OnChange?.Invoke();
                PlayAudio();
            }
        }
        public void AddToQueue(Song song)
        {
            songQueue.Add(song);
            OnChange();
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
