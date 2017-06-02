namespace PandyIT.VinylOrganizer.DAL.Model.Entities
{
    public class HarvestedMusicTrack
    {
        public int HarvestedMusicTrackId { get; set; }

        public int HarvestedTrackListId { get; set; }

        public string ArtistSearch { get; set; }

        public string TitleSearch { get; set; }

        public string ArtistMatch { get; set; }

        public string TitleMatch { get; set; }

        public int? Levenshtein { get; set; }

        public string Uri { get; set; }

        public string Status { get; set; }

        public HarvestedTrackList HarvestedTrackList { get; set; }
    }
}
