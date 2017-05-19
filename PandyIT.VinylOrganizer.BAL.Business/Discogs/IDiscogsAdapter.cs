using System.Collections.Generic;
using DiscogsClient.Data.Query;
using DiscogsClient.Data.Result;

namespace PandyIT.VinylOrganizer.BAL.Business.Discogs
{
    public interface IDiscogsAdapter
    {
        DiscogsRelease GetRelease(int releaseId);

        DiscogsMaster GetMaster(int masterId);

        IEnumerable<DiscogsSearchResult> Search(DiscogsSearch query);        
    }    
}
