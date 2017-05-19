using PandyIT.Core.Integration.Discogs.Entities;

namespace PandyIT.VinylOrganizer.BAL.Business.Discogs
{
    public interface IDiscogsAdapter
    {
        Release GetRelease(int releaseId);
        Release[] Search(string artist, string title);
    }
}