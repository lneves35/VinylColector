using DiscogsClient.Data.Result;
using DiscogsClient.Internal;

namespace PandyIT.VinylOrganizer.BAL.Business.Discogs
{
    public class DiscogsAdapter : IDiscogsAdapter
    {
        private DiscogsClient.DiscogsClient client;

        public DiscogsAdapter(string token)
        {
            var tokenInformation = new TokenAuthenticationInformation(token);
            this.client = new DiscogsClient.DiscogsClient(tokenInformation);
        }

        public DiscogsRelease GetRelease(int releaseId)
        {
            var release = client.GetReleaseAsync(1704673).Result;
            return release;
        }
    }
}
