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
    public AlbumWithPicture? selectedAlbum;

    [ObservableProperty]
    public Song? selectedSong;

    public AlbumsViewModel(SoundPlayer sp, DatabaseHandler db, AppSettings ap)
    {
        soundPlayer = sp; databaseHandler = db; appSettings = ap;

        var Albums = new List<Album>(databaseHandler.AllAlbums());
        foreach (Album album in Albums)
        {
            using var tfile = TagLib.File.Create(album.AlbumPath);
            if (tfile.Tag.Pictures.Length == 0) continue;
            ListOfAlbums.Add(new AlbumWithPicture
            (
                album,
                tfile.Tag.Pictures[0].Data.Data
            ));
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
    public void PlaySong()
    {
        if (SelectedSong == null) return;
        soundPlayer.currentSong = SelectedSong;
        soundPlayer.songQueue = ListOfAlbumsSongs.ToList();
        soundPlayer.PlayAudio();

        //Without this dalay the item was still selected
        //Delay somehow fixes that
        Task.Run(async () =>
        {
            await Task.Delay(10);
            SelectedSong = null;
        });
    }
    public partial class AlbumWithPicture : ObservableObject
    {
        [ObservableProperty]
        public Album album;
        [ObservableProperty]
        public byte[] data;

        public AlbumWithPicture(Album a, byte[] d)
        {
            album = a;
            data = d;
        }
    }
}