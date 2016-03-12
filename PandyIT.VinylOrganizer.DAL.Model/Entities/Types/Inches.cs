using System.ComponentModel.DataAnnotations;

namespace PandyIT.VinylOrganizer.DAL.Model.Entities.Types
{
    public class Inches
    {
        public int InchesId { get; set; }

        [Required]
        public string Name { get; set; }
    }
}
