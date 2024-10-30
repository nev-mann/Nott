using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Nott.Models;
using Nott.Source;
using Plugin.Maui.Audio;
using System.Collections.ObjectModel;

namespace Nott.ViewModels;

public partial class SongsViewModel : ObservableObject
{
	[ObservableProperty]
	public ObservableCollection<Song> listOfSongs = [];

	[ObservableProperty]
	string currentSong;

	[ObservableProperty]
	string? selectedSong;

	private readonly SoundPlayer soundPlayer;
	private readonly AppSettings appSettings;

	[RelayCommand]
	public void PlaySong(Song song)
	{
		soundPlayer.CurrentSong = song.Path;
        CurrentSong = song.Title;
		soundPlayer.PlayAudio();
		selectedSong = null;
	}

	public void UpdateListSongs()
    {
		ListOfSongs.Clear();
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
		soundPlayer = sp;
		appSettings = settings;
		UpdateListSongs();
        CurrentSong = sp.CurrentSong;
    }
}