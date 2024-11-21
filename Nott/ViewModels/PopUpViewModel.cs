using CommunityToolkit.Mvvm.ComponentModel;
using Nott.Models;
using System.Collections.ObjectModel;

namespace Nott.ViewModels;

public partial class PopUpViewModel : ObservableObject
{
    [ObservableProperty]
    public ObservableCollection<Bp> listOfPlaylist = [];
    public PopUpViewModel(Song song)
    {
        var db = new DatabaseHandler();

        var songPlaylists = db.SongPlaylists(song);
        foreach (var playlist in db.AllPlaylists())
        {
            var found = false;
            foreach (var item in songPlaylists) { 
                if(playlist.Id == item.Id)
                {
                    ListOfPlaylist.Add(new Bp { IsIn = true, Playlist = playlist.Name });
                    found = true;
                    break;
                }
            }
            if(!found) ListOfPlaylist.Add(new Bp { IsIn = false, Playlist = playlist.Name });
        }
    }
}
public partial class Bp : ObservableObject
{
    [ObservableProperty]
    public bool isIn;
    [ObservableProperty]
    public string playlist;
}