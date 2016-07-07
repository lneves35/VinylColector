using System.ComponentModel.DataAnnotations.Schema;

namespace PandyIT.VinylOrganizer.DAL.Model.Entities
{
    [Table("LocationVinyl")]
    public class LocationVinyl : Location
    {   
        public string Title { get; set; }

        public short? Year { get; set; }

        public byte? Month { get; set; }

        public byte? Day { get; set; }

        public string Genre { get; set; }

        public int? DiscogsId { get; set; }

    }
}
