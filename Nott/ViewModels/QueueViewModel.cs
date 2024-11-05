using CommunityToolkit.Mvvm.ComponentModel;
using Nott.Models;

namespace Nott.ViewModels
{
    public partial class QueueViewModel : ObservableObject
    {
        private readonly SoundPlayer soundPlayer;
        [ObservableProperty]
        List<Song> songQueue;
        public QueueViewModel(SoundPlayer sp) {
            soundPlayer = sp;
            songQueue = soundPlayer.SongQueue.ToList();
            //Task.Run(Loop);
        }
        private async Task Loop()
        {
            while (true)
            {
                SongQueue = soundPlayer.SongQueue.ToList();
                
                await Task.Delay(10);
            }
        }
        public void UpdateView()
        {
            SongQueue = soundPlayer.SongQueue.ToList();
        }
    }
}
