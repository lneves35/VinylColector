using DiscogsClient.Data.Result;
using PandyIT.VinylOrganizer.BAL.Business.Discogs.Entities;

namespace PandyIT.VinylOrganizer.BAL.Business.Discogs
{
    public interface IDiscogsAdapter
    {
        DiscogsRelease GetRelease(int releaseId);
    }

    
}
