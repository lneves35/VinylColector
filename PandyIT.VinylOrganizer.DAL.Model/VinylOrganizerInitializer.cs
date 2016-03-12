using System;
using System.Data.Entity;
using PandyIT.VinylOrganizer.DAL.Model.Interfaces;

namespace PandyIT.VinylOrganizer.DAL.Model
{
    public class VinylOrganizerInitializer : CreateDatabaseIfNotExists<VinylOrganizerDbContext>
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

        protected override void Seed(VinylOrganizerDbContext dbContext)
        {
            base.Seed(dbContext);
            Seeder.Seed(dbContext);
        }
    }
}
