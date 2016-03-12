using System.Data.Entity;
using PandyIT.VinylOrganizer.DAL.Model.Entities;
using PandyIT.VinylOrganizer.DAL.Model.Entities.Types;
using PandyIT.VinylOrganizer.DAL.Model.Interfaces;

namespace PandyIT.VinylOrganizer.DAL.Model
{
    public class VinylOrganizerContext : DbContext
    {
        public VinylOrganizerContext(string connectionString, IVinylOrganizerSeeder seeder)
            : base(connectionString)
        {
            Database.SetInitializer(new VinylOrganizerInitializer(seeder));
        }

        protected VinylOrganizerContext(string connectionString)
            : base(connectionString)
        {

        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Conventions.Remove<System.Data.Entity.ModelConfiguration.Conventions.PluralizingTableNameConvention>();
        }

        public DbSet<MusicTrack> MusicTracks { get; set; }
        public DbSet<MusicTrackLocation> MusicTrackLocations { get; set; }
        public DbSet<Location> Locations { get; set; }
        public DbSet<Inches> Inches { get; set; }
    }
}
