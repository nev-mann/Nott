using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Nott.Models;
using System.Collections.ObjectModel;

namespace Nott.ViewModels;

public partial class SongsViewModel : ObservableObject
{
	[ObservableProperty]
	public ObservableCollection<Song> listOfSongs = [];

	[ObservableProperty]
	Song selectedSong;

	private readonly SoundPlayer soundPlayer;
	private readonly AppSettings appSettings;

    [RelayCommand]
    public void AddToQueue(Song song)
    {
        soundPlayer.AddToQueue(song);        
    }

    [RelayCommand]
	public async Task PlaySong(Song song)
    {
        if (SelectedSong == null) return;
        soundPlayer.CurrentSong = song;
        soundPlayer.PlayAudio();
        //Without this dalay the item was still selected
        //Delay somehow fixes that
        await Task.Delay(10);
        SelectedSong = null;
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

	public SongsViewModel(SoundPlayer sp,AppSettings settings)
	{
        //MauiProgram.GetSongBarViewModel<SongBarViewModel>().Volume = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "Nott.json");

        soundPlayer = sp;
		appSettings = settings;
		UpdateListSongs();
        SelectedSong = new Song();
    }
}