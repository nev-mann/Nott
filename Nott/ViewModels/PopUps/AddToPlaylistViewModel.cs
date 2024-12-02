using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Nott.Models;
using System.Collections.ObjectModel;

namespace Nott.ViewModels;

public partial class AddToPlaylistViewModel : ObservableObject
{
    [ObservableProperty]
    public ObservableCollection<Bp> listOfPlaylist = [];

    public delegate void EventHandler();
    public event EventHandler? ClosePopup;

    private readonly Song _song;
    public AddToPlaylistViewModel(Song song)
    {
        var db = new DatabaseHandler();
        _song = song;

        var songPlaylists = db.SongPlaylists(song);
        foreach (var playlist in db.AllPlaylists())
        {
            var found = false;
            foreach (var item in songPlaylists) { 
                if(playlist.Id == item.Id)
                {
                    ListOfPlaylist.Add(new Bp ( true, playlist));
                    found = true;
                    break;
                }
            }
            if(!found) ListOfPlaylist.Add(new Bp ( false, playlist ));
        }
    }

    [RelayCommand]
    public void Save()
    {
        var db = new DatabaseHandler();

        foreach (var playlist in ListOfPlaylist)
        {
            if (playlist.IsIn)
            {
                db.AddToPlaylist(_song, playlist.Playlist);
            }
            else
            {
                db.RemoveFromPlaylist(_song, playlist.Playlist);
            }
        }
        Cancel();
    }
    [RelayCommand]
    public void Cancel()
    {
        ClosePopup?.Invoke();
    }
}
public partial class Bp(bool isIn, Playlist playlist) : ObservableObject
{
    [ObservableProperty]
    public bool isIn = isIn;
    [ObservableProperty]
    public Playlist playlist = playlist;
}