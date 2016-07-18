﻿using PandyIT.VinylOrganizer.DAL.Model;
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
            if (seeder == null)
                seeder = new VinylOrganizerSeeder();
            return seeder;
        }

        public void Seed(VinylOrganizerDbContext ctx)
        {
            var rootLocation = new Location();
            rootLocation.Name = "Collection";
            ctx.Locations.Add(rootLocation);
        }
    }
}
