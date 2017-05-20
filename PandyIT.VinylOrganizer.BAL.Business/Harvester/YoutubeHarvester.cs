namespace PandyIT.VinylOrganizer.BAL.Business.Harvester
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using log4net;
    using PandyIT.Core.Integration.Youtube;
    using PandyIT.Core.Text;
    using PandyIT.VinylOrganizer.DAL.Model.Entities;

    public class YoutubeHarvester
    {
        private IYoutubeAdapter youtubeAdapter;
        private ILog log;

        public YoutubeHarvester(IYoutubeAdapter youtubeAdapter, ILog log)
        {
            this.youtubeAdapter = youtubeAdapter;
            this.log = log;
        }

        public void HarvestMusicTrack(MusicTrack musicTrack)
        {
            var infoHarvesting = string.Format("------Harvesting music track: {0} - {1}", musicTrack.Artist, musicTrack.Title);
			log.Info(infoHarvesting);

            var youtubeResults = youtubeAdapter.Search(musicTrack.Artist + " " + musicTrack.Title);


            YoutubeSearchResult topMatch = null;
            var lastDistance = 1000;

            foreach (var result in youtubeResults)
            {
                var youtubeEntries = result.Title.Split(new char[] {'-'}, StringSplitOptions.RemoveEmptyEntries);

                if (youtubeEntries.Length == 2)
                {
                    var youtubeArtist = youtubeEntries[0];
                    var youtubeTitle = youtubeEntries[1];

                    var artistDistance = TextUtils.Levenshtein(musicTrack.Artist, youtubeArtist, true);
                    var titleDistance = TextUtils.Levenshtein(musicTrack.Title, youtubeTitle, true);
                    var totalDistance = artistDistance + titleDistance;
                    //var matchInfo = string.Format("{0}   {1} - {2} ", totalDistance, youtubeArtist, youtubeTitle);
                    //log.Info(matchInfo);

					if (totalDistance < lastDistance)
					{
					    lastDistance = totalDistance;
					    topMatch = result;
					}
                }
            }

            if (topMatch != null)
            {
                var topMatchInfo = string.Format("---- Top Match {0}: {1}", topMatch.Title, lastDistance);
                log.Info(topMatchInfo);
                youtubeAdapter.ExtractMp3(new Uri(topMatch.Uri));
            }
        }

        public void HarvestMusickTracks(IEnumerable<MusicTrack> musicTracks)
        {
            musicTracks
                .ToList()
                .ForEach(HarvestMusicTrack);
        }
    }
}
