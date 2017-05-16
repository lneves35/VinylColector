using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PandyIT.VinylOrganizer.DAL.Model.Entities
{
    public class Setting
    {
        public int SettingId { get; set; }

        [Required]
        [Index(IsUnique = true)]
        [MaxLength(40)]
        public string Key { get; set; }
        
        public string Value { get; set; }
    }
}
