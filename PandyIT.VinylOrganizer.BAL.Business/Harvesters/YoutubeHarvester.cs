namespace PandyIT.VinylOrganizer.Services.Harvesters
{
    using System;
    using log4net;
    using PandyIT.Core.Integration.Youtube;
    using PandyIT.Core.Text;
    using PandyIT.VinylOrganizer.DAL.Model.Entities;

    public class YoutubeHarvester : IYoutubeHarvester
    {
        private readonly IYoutubeAdapter youtubeAdapter;
        private readonly ILog log;

        public YoutubeHarvester(IYoutubeAdapter youtubeAdapter, ILog log)
        {
            this.youtubeAdapter = youtubeAdapter;
            this.log = log;
        }

        public void HarvestMusicTrack(HarvestedMusicTrack musicTrack)
        {
            var artist = musicTrack.ArtistSearch;
            var title = musicTrack.TitleSearch;

            musicTrack.Status = "Failed";

            var infoHarvesting = string.Format("------Harvesting music track: {0} - {1}", artist, title);
			log.Info(infoHarvesting);

            var youtubeResults = youtubeAdapter.Search(artist + " " + title);

            YoutubeSearchResult topMatch = null;
            var bestDistance = 1000;

            string matchedArtist = null;
            string matchedTitle = null;

            foreach (var result in youtubeResults)
            {
                var youtubeEntries = result.Title.Split(new char[] {'-'}, StringSplitOptions.RemoveEmptyEntries);

                if (youtubeEntries.Length == 2)
                {
                    var youtubeArtist = youtubeEntries[0];
                    var youtubeTitle = youtubeEntries[1];

                    var artistDistance = TextUtils.Levenshtein(artist, youtubeArtist, true);
                    var titleDistance = TextUtils.Levenshtein(title, youtubeTitle, true);
                    var totalDistance = artistDistance + titleDistance;                    

					if (totalDistance < bestDistance)
					{
					    bestDistance = totalDistance;
					    topMatch = result;
					    matchedArtist = youtubeArtist;
					    matchedTitle = youtubeTitle;

					}
                }
            }

            if (topMatch != null)
            {
                musicTrack.Uri = topMatch.Uri;
                musicTrack.ArtistMatch = matchedArtist;
                musicTrack.TitleMatch = matchedTitle;
                musicTrack.Levenshtein = bestDistance;

                var topMatchInfo = string.Format("---- Top Match {0}: {1}", topMatch.Title, bestDistance);
                log.Info(topMatchInfo);
                var result = youtubeAdapter.ExtractMp3(new Uri(topMatch.Uri));
                if (!result.HasError)
                {
                    musicTrack.Status = "SUCCESS";
                }
            }
        }
    }
}
