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
	ObservableCollection<Song> listOfSongs ;

	[ObservableProperty]
	string currentSong;

	[ObservableProperty]
	string selectedSong;

	private SoundPlayer soundPlayer;

	[RelayCommand]
	public void PlaySong(Song song)
	{
		soundPlayer.CurrentSong = song.Path;
        CurrentSong = song.Title;
		soundPlayer.PlayAudio();
		selectedSong = null;
	}

	public SongsViewModel(SoundPlayer sp)
	{
		soundPlayer = sp;
		var db = new DatabaseHandler();
		var sl = new SongList();
		foreach(var x in sl.SongListGet())
		{
			db.AddSong(x);
		}
		listOfSongs = new ObservableCollection<Song>(db.AllSongs());
        ListOfSongs = listOfSongs;
		CurrentSong = sp.CurrentSong;
    }
}