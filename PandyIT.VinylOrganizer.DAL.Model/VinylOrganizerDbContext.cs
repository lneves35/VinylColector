using System.Data.Entity;
using PandyIT.VinylOrganizer.DAL.Model.Entities;
using PandyIT.VinylOrganizer.DAL.Model.Interfaces;

namespace PandyIT.VinylOrganizer.DAL.Model
{
    public class VinylOrganizerDbContext : DbContext
    {
        public VinylOrganizerDbContext() { }

        public VinylOrganizerDbContext(string connectionString, IVinylOrganizerSeeder seeder)
            : base(connectionString)
        {
            Database.SetInitializer(new VinylOrganizerInitializer(seeder));
        }

        public VinylOrganizerDbContext(string connectionString)
            : base(connectionString)
        {

        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Conventions.Remove<System.Data.Entity.ModelConfiguration.Conventions.PluralizingTableNameConvention>();
        }

        public DbSet<MusicTrack> MusicTracks { get; set; }

        public DbSet<MusicTrackInstance> MusicTrackLocations { get; set; }

        public DbSet<Location> Locations { get; set; }

        public DbSet<Setting> Settings { get; set; }

        public DbSet<HarvestedMusicTrack> HarvestedMusicTracks { get; set; }

        public DbSet<HarvestedTrackList> HarvestedTrackLists { get; set; }

        public DbSet<HarvestingBatch> HarvestingBatchs { get; set; }
    }
}
