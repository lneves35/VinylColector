using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PandyIT.VinylOrganizer.DAL.Model.Entities
{
    public class Location
    {
        public int LocationId { get; set; }

        [Required]
        [Index("IX_NameAndParent", 1, IsUnique = true)]
        public String UniqueName { get; set; }

        [ForeignKey("ParentLocation")]
        [Index("IX_NameAndParent", 2, IsUnique = true)]
        public int? ParentLocationId { get; set; }

        public Location ParentLocation { get; set; }
        public ICollection<Location> ChildLocations { get; set; }

    }
}
