using System.Collections.Generic;

namespace PandyIT.VinylOrganizer.DAL.Model.Entities
{
    public class HarvestedTrackList
    {
        public int HarvestedTrackListId { get; set; }

        public int HarvestingBatchId { get; set; }

        public string Title { get; set; }

        public string Uri { get; set; }

        public HarvestingBatch HarvestingBatch { get; set; }

        public ICollection<HarvestedMusicTrack> HarvestedMusicTracks { get; set; }
    }
}
