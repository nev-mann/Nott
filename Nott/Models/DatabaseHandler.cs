using SQLite;

namespace Nott.Source
{
    public class DatabaseHandler
    {
        private SQLiteConnection _db;

        public DatabaseHandler()
        {
            _db = new SQLiteConnection(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "Nott.db"));
            _db.CreateTable<Song>();
            _db.CreateTable<Album>();
        }
        public void AddSong(string path)
        {

            //Add song, if exists stop
            if (_db.Query<Song>("SELECT * FROM Songs WHERE path = \"" + path + "\"").Count > 0) return;
            
            var tfile = TagLib.File.Create(path);
            var song = new Song
            {
                Path = path,
                Album = tfile.Tag.Album,
                Title = tfile.Tag.Title == null ? path[(path.LastIndexOf('\\') + 1)..] : tfile.Tag.Title
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

        public List<Song> AllSongs()
        {
            return _db.Query<Song>("SELECT * FROM Songs");
        }

        public List<Album> AllAlbums()
        {
            return _db.Query<Album>("SELECT * FROM Albums");
        }

        public List<Song> AlbumsSongs(Album album)
        {
            return _db.Query<Song>("SELECT * FROM Songs INNER JOIN Albums ON Songs.album=Albums.albumName WHERE Songs.album=\""+ album.AlbumName + '\"');
        }
    };

}
