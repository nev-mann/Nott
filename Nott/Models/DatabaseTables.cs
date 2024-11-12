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
}
