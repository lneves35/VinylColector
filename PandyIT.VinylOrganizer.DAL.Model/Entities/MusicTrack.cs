using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PandyIT.VinylOrganizer.DAL.Model.Entities
{
    public class MusicTrack
    {
        public int MusicTrackId { get; set; }

        [Required]
        public string Artist { get; set; }

        [Required]
        public string Title { get; set; }

        public string Key { get; set; }

        public uint? Bpm { get; set; }

        public byte? Rating { get; set; }                

        public virtual ICollection<MusicTrackInstance> MusicTrackInstances { get; set; }
    }
}
