using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PandyIT.VinylOrganizer.DAL.Model.Entities
{
    public class HarvestedTrackList
    {
        public int HarvestedTrackListId { get; set; }

        [Required]        
        public int HarvestingBatchId { get; set; }

        [Required]        
        public string Title { get; set; }

        [Required]
        public string Uri { get; set; }

        [Required]
        [Index(IsUnique = true)]
        [MaxLength(40)]
        public string UriHash { get; set; }

        public HarvestingBatch HarvestingBatch { get; set; }

        public ICollection<HarvestedMusicTrack> HarvestedMusicTracks { get; set; }
    }
}
