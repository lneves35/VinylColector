namespace PandyIT.Core.Integration.Discogs.Entities
{
    using System.Linq;
    using DiscogsClient.Data.Result;

    public class Track
    {
        public Track(DiscogsTrack discogsTrack)
        {
            this.Artist = discogsTrack.artists?.First().name;
            this.Title = discogsTrack.title;
        }

        public string Artist { get; set; }

        public string Title { get; set; }
    }
}
