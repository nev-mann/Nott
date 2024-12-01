using SQLite;

namespace Nott.Models
{
    public class DatabaseHandler
    {
        private SQLiteConnection _db;

        public DatabaseHandler()
        {
            _db = new SQLiteConnection(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "Nott.db"));
            _db.CreateTable<Song>();
            _db.CreateTable<Album>();
            _db.CreateTable<SongPlaylist>();
            _db.CreateTable<Playlist>();
        }
        public void AddSong(string path)
        {

            //Add song, if exists stop
            if (_db.Query<Song>("SELECT * FROM Songs WHERE path = \"" + path + "\"").Count > 0) return;
            
            var tfile = TagLib.File.Create(path);
            var song = new Song
            {
                Path = path,
                Artist = tfile.Tag.FirstAlbumArtist,
                Album = tfile.Tag.Album,                
                Title = tfile.Tag.Title == null ? path[(path.LastIndexOf('\\') + 1)..] : tfile.Tag.Title,
                Favorite = false,
                TimesListened = 0,
            };

            _db.Insert(song);



            if (tfile.Tag.Album == null) return;
            //Add song's album, if exists stop
            if (_db.Query<Album>("SELECT * FROM Albums WHERE AlbumName = \"" + tfile.Tag.Album + "\"").Count > 0) return;

            var album = new Album
            {
                AlbumName = tfile.Tag.Album,
                AlbumPath = path,               
            };

            //System.IO.File.WriteAllBytes(Path.Combine("C:\\Users\\kamne\\Desktop", tfile.Tag.Album + ".png"), tfile.Tag.Pictures[0].Data.Data);

            //using (StreamWriter outputFile = new StreamWriter(Path.Combine("C:\\Users\\kamne\\Desktop", tfile.Tag.Album+ ".png")))
            //{
            //    return;
            //}

            _db.Insert(album);
        }
        public void AddAlbum(string albumName)
        {

        }
        public void AddPlaylist(string name)
        {
            _db.Insert(new Playlist { Name = name });
        }
        public List<Song> AllSongs()
        {
            return _db.Query<Song>("SELECT * FROM Songs");
        }
        public List<Album> AllAlbums()
        {
            return _db.Query<Album>("SELECT * FROM Albums");
        }
        public List<Playlist> AllPlaylists()
        {
            return _db.Query<Playlist>("SELECT * FROM Playlists");
        }
        public void RemovePlaylist(Playlist pl)
        {
            _db.Delete(pl);
        }
        public List<Song> PlaylistSongs(Playlist pl)
        {
            return _db.Query<Song>("SELECT Songs.* FROM Songs INNER JOIN SongPlaylist ON Songs.id=SongPlaylist.song_id WHERE SongPlaylist.playlist_id="+pl.Id);
        }
        public List<Playlist> SongPlaylists(Song s)
        {
            return _db.Query<Playlist>("SELECT Playlists.* FROM Playlists INNER JOIN SongPlaylist ON Playlists.id=SongPlaylist.playlist_id WHERE SongPlaylist.song_id=" + s.Id);
        }
        public List<Song> AlbumsSongs(Album album)
        {
            return _db.Query<Song>("SELECT * FROM Songs INNER JOIN Albums ON Songs.album=Albums.albumName WHERE Songs.album=\""+ album.AlbumName + '\"');
        }
        public void Update(Song song)
        {
            _db.Update(song);
        }
    };

}
