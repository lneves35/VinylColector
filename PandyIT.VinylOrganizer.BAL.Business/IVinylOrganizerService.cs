using System.IO;
using PandyIT.VinylOrganizer.DAL.Model.Entities;

namespace PandyIT.VinylOrganizer.BAL.Business
{
    public interface IVinylOrganizerService
    {
        void AddMusicTrack(MusicTrack musicTrack);

        void AddDiscogsVinyl(int releaseId, int parentLocationId);

        LocationVinyl FetchVinylByName(string name);

        void AddLocation(Location location);
    }
}
