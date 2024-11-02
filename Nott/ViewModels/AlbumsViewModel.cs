using CommunityToolkit.Mvvm.Collections;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Nott.Models;
using Nott.Source;
using System.Collections.ObjectModel;
using System.Data;

namespace Nott.ViewModels;

public partial class AlbumsViewModel : ObservableObject
{
	[ObservableProperty]
	ObservableCollection<AlbumWithPicture> listOfAlbums = [];

    [ObservableProperty]
    ObservableCollection<Song> listOfAlbumsSongs = [];

	private SoundPlayer soundPlayer;

	[ObservableProperty]
	public AlbumWithPicture selectedAlbum;

	private DatabaseHandler databaseHandler;

    public AlbumsViewModel(SoundPlayer sp,DatabaseHandler db)
	{
        soundPlayer = sp;
		databaseHandler = db;
		var Albums = new List<Album>(databaseHandler.AllAlbums());
		foreach (Album album in Albums)
		{
            var tfile = TagLib.File.Create(album.AlbumPath);
			if (tfile.Tag.Pictures.Length == 0) continue;
            AlbumWithPicture x = new AlbumWithPicture
            {
                album = album,
                data = tfile.Tag.Pictures[0].Data.Data
			};
            ListOfAlbums.Add(x);
		}
	}

	[RelayCommand]
	public void DisplayAlbum(AlbumWithPicture ap)
	{
		ListOfAlbumsSongs.Clear();
		foreach(var x in databaseHandler.AlbumsSongs(ap.album))
		{
            ListOfAlbumsSongs.Add(x);
        }
	}

    [RelayCommand]
    public void PlaySong(Song song)
    {
        soundPlayer.CurrentSong = song;
        soundPlayer.PlayAudio();
    }

    public partial class AlbumWithPicture : ObservableObject
    {
		[ObservableProperty]
		public Album album;
		[ObservableProperty]
        public byte[] data;
	}
}