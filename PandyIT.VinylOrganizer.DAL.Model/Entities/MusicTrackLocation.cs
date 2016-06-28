using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PandyIT.VinylOrganizer.DAL.Model.Entities
{
    public class MusicTrackInstance
    {
        public int MusicTrackInstanceId { get; set; }

        [Required]
        [Index("IX_MusicTrackInstance", 1, IsUnique = true)]
        public int MusicTrackId { get; set; }

        [Required]
        [Index("IX_MusicTrackInstance", 2, IsUnique = true)]
        public int LocationId { get; set; }

        public MusicTrack MusicTrack { get; set; }

        public Location Location { get; set; }
    }
}
