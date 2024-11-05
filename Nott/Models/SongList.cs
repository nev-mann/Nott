
namespace Nott.Models
{
    public class SongList
    {
        private List<string> SongsFolders;

        private List<string> Extensions = [
            ".mp3",
            ".flac",
            ".wav",
            ];

        private List<string> SongsPaths = [];        
        public SongList(AppSettings ap)
        {
            //Using ToList because without it, it is a refreance and I want a copy (better solution?) 
            SongsFolders = ap.settings.SongsFolders.ToList();
            SongListUpdate();
        }
        public void SongListUpdate()
        {
            List<string> tmp = new();

            foreach (string folder in SongsFolders)
            {
                tmp.AddRange(GetFolders(folder));
            }

            SongsFolders.AddRange(tmp);
            
            foreach (string folder in SongsFolders)
            {
                SongsPaths.AddRange(Directory.GetFiles(folder));
            }                       

            for (var i = 0;i < SongsPaths.Count;i++)
            {
                if (!IsMusicExt(SongsPaths[i])) {
                    SongsPaths.RemoveAt(i);
                    i--;
                }
            }
        }

        private List<string> GetFolders(string folder)
        {
            List<string> dir = Directory.GetDirectories(folder).ToList();

            List<string> tmp = new List<string>();

            foreach (var f in dir)
            {
                tmp.AddRange(GetFolders(f));
            }

            dir.AddRange(tmp);

            return dir;
        }

        public List<string> SongListGet()
        {
            return SongsPaths;
        }

        private bool IsMusicExt(string path)
        {
            foreach (string ext in Extensions)
            {
                if (path.EndsWith(ext)) return true;
            }
            return false;
        }
    }
}