using CommunityToolkit.Maui.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Nott.Models;
using Nott.ViewModels;
using Nott.Views.Controls;
using System.Collections.ObjectModel;

namespace Nott.ViewModels;

public partial class SongsViewModel : ObservableObject
{
	[ObservableProperty]
	public ObservableCollection<Song> listOfSongs = [];

	[ObservableProperty]
	Song? selectedSong;

	private readonly SoundPlayer soundPlayer;
	private readonly AppSettings appSettings;

    public SongsViewModel(SoundPlayer sp, AppSettings settings)
    {
        soundPlayer = sp;
        appSettings = settings;
        UpdateListSongs();
        SelectedSong = new Song();
    }

    [RelayCommand]
    public void AddToQueue(Song song)
    {
        soundPlayer.AddToQueue(song);        
    }
    [RelayCommand]
    public async Task AddToPlaylist(Song song)
    {
        var popup = new PopUpView(new PopUpViewModel(song));
        Shell.Current.CurrentPage.ShowPopup(popup);
    }
    [RelayCommand]
	public void PlaySong(Song song)
    {
        if (SelectedSong is null) return;
        soundPlayer.currentSong = song;
        soundPlayer.AddToQueue(ListOfSongs.ToList());
        soundPlayer.PlayAudio();
        //Without this dalay the item was still selected
        //Delay somehow fixes that
        Task.Run(async () => {
            await Task.Delay(10);
            SelectedSong = null;
        });
    }
    [RelayCommand]
    public void HeartClicked(Song song)
    {
        var db = new DatabaseHandler();
        song.Favorite ^= true;
        ListOfSongs[song.Id - 1] = song;
        db.Update(song);
    }
    public void UpdateListSongs()
    {
        var db = new DatabaseHandler();
        var sl = new SongList(appSettings);
        foreach (var x in sl.SongListGet())
        {
            db.AddSong(x);
        }
        ListOfSongs = new ObservableCollection<Song>(db.AllSongs());
    }
}