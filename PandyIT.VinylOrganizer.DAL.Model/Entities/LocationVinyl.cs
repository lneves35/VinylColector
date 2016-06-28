using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PandyIT.VinylOrganizer.DAL.Model.Entities.Types;

namespace PandyIT.VinylOrganizer.DAL.Model.Entities
{
    [Table("LocationVinyl")]
    public class LocationVinyl : Location
    {   
        public string Ref { get; set; }

        public int? InchesId { get; set; }

        public Inches Inches { get; set; }

        public DateTime? ReleaseDate { get; set; }
    }
}
