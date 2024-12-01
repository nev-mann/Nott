using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Nott.Models;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace Nott.ViewModels;

public partial class PopUpViewModel : ObservableObject
{
    [ObservableProperty]
    public ObservableCollection<Bp> listOfPlaylist = [];

    public delegate void EventHandler();
    public event EventHandler? ClosePopup;
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

    [RelayCommand]
    public void Save()
    {
        Cancel();
    }
    [RelayCommand]
    public void Cancel()
    {
        ClosePopup?.Invoke();
    }
}
public partial class Bp : ObservableObject
{
    [ObservableProperty]
    public bool isIn;
    [ObservableProperty]
    public string playlist;
}