using CommunityToolkit.Mvvm.ComponentModel;
using Nott.Models;
using System.Collections.ObjectModel;

namespace Nott.ViewModels;

public partial class PopUpViewModel : ObservableObject
{
	[ObservableProperty]
	public string text;
	[ObservableProperty]
	public ObservableCollection<string> listOfPlaylist;
    public PopUpViewModel(Song song)
	{
		Text = song.Title;
		var db = new DatabaseHandler();
		ListOfPlaylist = new ObservableCollection<string>();
		foreach(var playlist in db.AllPlaylists())
		{
			ListOfPlaylist.Add(playlist.Name);
		}
	}
}