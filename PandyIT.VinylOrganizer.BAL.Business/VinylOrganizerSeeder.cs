using PandyIT.VinylOrganizer.Common;
using PandyIT.VinylOrganizer.DAL.Model;
using PandyIT.VinylOrganizer.DAL.Model.Entities;
using PandyIT.VinylOrganizer.DAL.Model.Interfaces;

namespace PandyIT.VinylOrganizer.BAL.Business
{
    public class VinylOrganizerSeeder : IVinylOrganizerSeeder
    {
        private static VinylOrganizerSeeder seeder;

        private VinylOrganizerSeeder() { }

        public static VinylOrganizerSeeder GetSeeder()
        {
            return seeder ?? (seeder = new VinylOrganizerSeeder());
        }

        public void Seed(VinylOrganizerDbContext ctx)
        {
            ctx.Locations.Add(new Location { Name = "Collection" });
            ctx.Settings.Add(new Setting() { Key = Constants.DISCOGS_CONSUMER_KEY, Value = "wTIlBQlrElaTrepxOBIw" });
            ctx.Settings.Add(new Setting() { Key = Constants.DISCOGS_CONSUMER_SECRET, Value = "ULEFpvSYgzzafOLXRIOsIaUwUAlAHfam" });
        }
    }
}
