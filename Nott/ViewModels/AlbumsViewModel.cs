using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Nott.Models;
using System.Collections.ObjectModel;

namespace Nott.ViewModels;

public partial class AlbumsViewModel : ObservableObject
{
    private DatabaseHandler databaseHandler;
    private SoundPlayer soundPlayer;
    private AppSettings appSettings;

    [ObservableProperty]
    ObservableCollection<AlbumWithPicture> listOfAlbums = [];

    [ObservableProperty]
    ObservableCollection<Song> listOfAlbumsSongs = [];

    [ObservableProperty]
    public AlbumWithPicture selectedAlbum;

    public AlbumsViewModel(SoundPlayer sp, DatabaseHandler db,AppSettings ap)
    {
        soundPlayer = sp; databaseHandler = db; appSettings = ap;

        var Albums = new List<Album>(databaseHandler.AllAlbums());
        foreach (Album album in Albums)
        {
            using var tfile = TagLib.File.Create(album.AlbumPath);
            if (tfile.Tag.Pictures.Length == 0) continue;
            ListOfAlbums.Add(new AlbumWithPicture
            {
                Album = album,
                Data = tfile.Tag.Pictures[0].Data.Data
            });
        }
    }

    [RelayCommand]
    public void DisplayAlbum(AlbumWithPicture ap)
    {
        ListOfAlbumsSongs.Clear();
        foreach (var x in databaseHandler.AlbumsSongs(ap.Album))
        {
            ListOfAlbumsSongs.Add(x);
        }
    }

    [RelayCommand]
    public void PlaySong(Song song)
    {
        if (song == null) return;
        soundPlayer.currentSong = song;
        soundPlayer.songQueue = ListOfAlbumsSongs.ToList();
        soundPlayer.PlayAudio();
    }
}