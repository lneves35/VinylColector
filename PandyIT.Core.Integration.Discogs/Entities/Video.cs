namespace PandyIT.Core.Integration.Discogs.Entities
{
    using DiscogsClient.Data.Result;

    public class Video
    {
        internal Video(DiscogsVideo discogsVideo)
        {
            this.Title = discogsVideo.title;
            this.Uri = discogsVideo.uri;
        }

        public string Title { get; set; }
        public string Uri { get; set; }
    }
}
