using cis414_project.Models.Song;

namespace cis414_project.Models.Playlist
{
    public class Playlist
    {
        public int PlaylistId { get; set; }

        public string PlaylistName { get; set; }

        public DateTime CreatedDate { get; set; }

        public List<SongItem> AvailableSongs { get; set; }

        public List<SongItem> SelectedSongs { get; set; }
    }
}
