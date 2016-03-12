using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PandyIT.VinylOrganizer.DAL.Model.Entities
{
    [Table("LocationVinylSide")]
    public class LocationVinylSide : Location
    {
        public string Side { get; set; }
    }
}
