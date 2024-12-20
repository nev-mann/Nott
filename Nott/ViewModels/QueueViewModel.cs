﻿using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Nott.Models;
using System.Collections.ObjectModel;

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
            //soundPlayer.QueueChange += UpdateView;
        }
        public void UpdateView()
        {
            SongQueue = [];
            var i = 1;
            foreach (var song in soundPlayer.songQueue)
            {
                SongQueue.Add(new PositionSong { Position = i, Song = song });
                i++;
            }
            Task.Delay(1);
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
