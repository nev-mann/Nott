using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Nott.Models;
using System.Collections.ObjectModel;
using System.Linq;

namespace Nott.ViewModels
{
    public partial class QueueViewModel : ObservableObject
    {
        private readonly SoundPlayer soundPlayer;

        [ObservableProperty]
        ObservableCollection<PositionSong> songQueue;

        public QueueViewModel(SoundPlayer sp)
        {
            soundPlayer = sp;
            songQueue = [];
            soundPlayer.OnChange += UpdateView;
        }
        public void UpdateView()
        {
            SongQueue = [];
            var i = 0;
            foreach (var song in soundPlayer.songQueue)
            {
                SongQueue.Add(new PositionSong { Position = i, Song = song });
                i++;
            }
        }

        [RelayCommand]
        public void RemoveSong(int position)
        {
            SongQueue.RemoveAt(position);
            soundPlayer.songQueue.RemoveAt(position);
            UpdateView();
        }

        public partial class PositionSong : ObservableObject
        {
		[ObservableProperty]
            public int position;
		[ObservableProperty]
            public Song? song;
        }
    }
}
