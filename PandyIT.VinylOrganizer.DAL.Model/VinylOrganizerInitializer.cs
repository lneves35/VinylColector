using System;
using System.Data.Entity;
using PandyIT.VinylOrganizer.DAL.Model.Interfaces;

namespace PandyIT.VinylOrganizer.DAL.Model
{
    public class VinylOrganizerInitializer : CreateDatabaseIfNotExists<VinylOrganizerContext>
    {
        private IVinylOrganizerSeeder Seeder { get; set; }

        public VinylOrganizerInitializer(IVinylOrganizerSeeder seeder)
        {
            if (seeder == null)
            {
                throw new ArgumentNullException(nameof(seeder));
            }

            Seeder = seeder;
        }

        protected override void Seed(VinylOrganizerContext context)
        {
            base.Seed(context);
            Seeder.Seed(context);
        }
    }
}
