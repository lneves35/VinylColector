using System;
using System.Collections.Generic;

namespace PandyIT.VinylOrganizer.DAL.Model.Entities
{
    public class HarvestingBatch
    {
        public int HarvestingBatchId { get; set; }

        public DateTime Date { get; set; }

        public ICollection<HarvestedTrackList> HarvestedTrackLists { get; set; }
    }
}
