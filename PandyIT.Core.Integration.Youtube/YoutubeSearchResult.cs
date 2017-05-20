namespace PandyIT.Core.Integration.Youtube
{
    using Google.Apis.YouTube.v3.Data;

    public class YoutubeSearchResult
    {
        private const string baseUri = "https://www.youtube.com/watch?v=";

        public YoutubeSearchResult(SearchResult item)
        {
            this.Uri = baseUri + item.Id.VideoId;
            this.Title = item.Snippet.Title;
        }

        public string Uri { get; set; }

        public string Title { get; set; }
    }
}
