using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Nott.Models;
using System.Collections.ObjectModel;

namespace Nott.ViewModels;

public partial class PlaylistsViewModel : ObservableObject
{
    private readonly SoundPlayer soundPlayer;
    private readonly AppSettings appSettings;

    [ObservableProperty]
    ObservableCollection<Playlist> listOfPlaylists;

    [ObservableProperty]
    ObservableCollection<Song> listOfSongs;

    public PlaylistsViewModel(SoundPlayer sp, AppSettings settings)
    {
        soundPlayer = sp;
        appSettings = settings;
        listOfPlaylists = new ObservableCollection<Playlist>(new DatabaseHandler().AllPlaylists());
    }

    [RelayCommand]
    public void DisplayPlaylist(Playlist pl)
    {
        ListOfSongs = new ObservableCollection<Song>(new DatabaseHandler().PlaylistSongs(pl));
    }
}