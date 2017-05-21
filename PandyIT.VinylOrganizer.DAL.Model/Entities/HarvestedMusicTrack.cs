namespace PandyIT.VinylOrganizer.DAL.Model.Entities
{
    public class HarvestedMusicTrack
    {
        public int HarvestedMusicTrackId { get; set; }

        public int HarvestedTrackListId { get; set; }

        public string Artist { get; set; }

        public string Title { get; set; }

        public string Uri { get; set; }

        public string Status { get; set; }

        public HarvestedTrackList HarvestedTrackList { get; set; }
    }
}
