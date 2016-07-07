using System.IO;
using PandyIT.VinylOrganizer.DAL.Model.Entities;

namespace PandyIT.VinylOrganizer.BAL.Business
{
    public interface IVinylOrganizerBusinessContext
    {
        void AddMusicTrack(MusicTrack musicTrack);

        void AddDiscogsVinyl(int releaseId);
    }
}
