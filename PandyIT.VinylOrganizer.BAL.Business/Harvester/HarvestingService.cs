using System;
using System.Collections.Generic;
using System.Linq;
using PandyIT.Core.Integration.Discogs.Entities;
using PandyIT.Core.Text;
using PandyIT.VinylOrganizer.BAL.Business.Discogs;
using PandyIT.VinylOrganizer.DAL.Model.Entities;

namespace PandyIT.VinylOrganizer.BAL.Business.Harvester
{
    public class HarvestingService
    {
        private readonly IDiscogsAdapter discogsAdapter;

        public HarvestingService(IDiscogsAdapter discogsAdapter)
        {
            this.discogsAdapter = discogsAdapter;
        }

        public void HarvestMusickTrack(MusicTrack musicTrack)
        {
            HarvestDiscogs(musicTrack);
        }

        private void HarvestDiscogs(MusicTrack musicTrack)
        {
            var releases = discogsAdapter.Search(musicTrack.Artist, musicTrack.Title);

            foreach (var release in releases)
            {
                var releaseName = string.Format("{0} - {1}", release.Artist, release.Title);
                Console.WriteLine(releaseName);

                foreach (var discogsTrack in release.Tracks)
                {
                    var artist = discogsTrack.Artist ?? release.Artist;

                    var artistDistance = TextUtils.Levenshtein(musicTrack.Artist, artist);
                    var titleDistance = TextUtils.Levenshtein(musicTrack.Title, discogsTrack.Title);

                    var track = string.Format("   {0} - {1} ({2})", artist, discogsTrack.Title, artistDistance + titleDistance);
                    Console.WriteLine(track);
                }
            }
        }

        public void HarvestMusickTracks(IEnumerable<MusicTrack> musicTracks)
        {
            musicTracks
                .ToList()
                .ForEach(HarvestMusickTrack);
        }
    }
}
