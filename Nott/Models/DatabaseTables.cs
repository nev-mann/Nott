using SQLite;

namespace Nott.Models
{

    [Table("Songs")]
    public class Song
    {
        [PrimaryKey, AutoIncrement]
        [Column("id")]
        public int Id { get; set; }

        [Unique]
        [Column("path")]
        public string? Path { get; set; }

        [Column("artist")]
        public string? Artist { get; set; }

        [Column("title")]
        public string? Title { get; set; }

        [Column("album")]
        public string? Album { get; set; }

        [Column("favorite")]
        public bool? Favorite { get; set; }

        [Column("timeslistened")]
        public int? TimesListened { get; set; }
    }

    [Table("Albums")]
    public class Album
    {
        [PrimaryKey, AutoIncrement]
        [Column("id")]
        public int Id { get; set; }

        [Unique]
        [Column("albumPath")]
        public string? AlbumPath { get; set; }

        [Column("albumName")]
        public string? AlbumName { get; set; }
    }

    [Table("SongPlaylist")]
    public class SongPlaylist
    {
        [Column("song_id")]
        public int SongId { get; set; }

        [Column("album_id")]
        public string AlbumId{ get; set; }
    }

    [Table("Playlists")]
    public class Playlist
    {
        [PrimaryKey, AutoIncrement]
        [Column("id")]
        public int Id { get; set; }

        [Unique]
        [Column("name")]
        public string Name { get; set; }
    }
}
