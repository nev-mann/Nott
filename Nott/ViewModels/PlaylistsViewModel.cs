using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Nott.Models;
using System.Collections.ObjectModel;

namespace Nott.ViewModels;

public partial class PlaylistsViewModel : ObservableObject
{
    [ObservableProperty]
    ObservableCollection<Playlist> listOfPlaylists;

    [ObservableProperty]
    Playlist? selectedPlaylist;

    [ObservableProperty]
    ObservableCollection<Song> listOfSongs;

    public PlaylistsViewModel()
    {
        listOfSongs = [];
        ListOfPlaylists = new ObservableCollection<Playlist>(new DatabaseHandler().AllPlaylists());
    }

    [RelayCommand]
    public void RemovePlaylist(Playlist pl)
    {
        new DatabaseHandler().RemovePlaylist(pl);
        ListOfPlaylists = new ObservableCollection<Playlist>(new DatabaseHandler().AllPlaylists());
    }

    [RelayCommand]
    public void DisplayPlaylist(Playlist pl)
    {
        ListOfSongs = new ObservableCollection<Song>(new DatabaseHandler().PlaylistSongs(pl));
    }

    [RelayCommand]
    public void AddPlaylist(string name)
    {
        new DatabaseHandler().AddPlaylist(name);
        ListOfPlaylists = new ObservableCollection<Playlist>(new DatabaseHandler().AllPlaylists());
    }
}