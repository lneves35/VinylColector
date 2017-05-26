using System;

namespace PandyIT.Core.Integration.Discogs.Entities
{
    using System.Linq;
    using DiscogsClient.Data.Result;

    public class Release
    {
        public Release(DiscogsRelease discogsRelease)
        {
            this.Artist = discogsRelease.artists.First().name;
            this.Title = discogsRelease.title;
            this.Tracks = discogsRelease.tracklist.Select(t => new Track(t)).ToArray();
            this.Videos = discogsRelease.videos?.Select(v => new Video(v)).ToArray();
            this.Genres = discogsRelease.genres;

            var dateString = discogsRelease.released ?? DateTime.MinValue.ToString("yyyy-MM-dd");
            var releaseDate = dateString.Split('-');
            var year = releaseDate[0].Length > 0 ? Convert.ToInt16(releaseDate[0]) : DateTime.MinValue.Year;
            var month = releaseDate.Length > 1 && Convert.ToByte(releaseDate[1]) != 0 ? Convert.ToByte(releaseDate[1]) : 1;
            var day = releaseDate.Length > 2 && Convert.ToByte(releaseDate[2])!= 0 ? Convert.ToByte(releaseDate[2]) : 1;
            this.Released = new DateTime(year, month, day);
        }

        public string Artist { get; set; }
        
        public string Title { get; set; }

        public DateTime Released { get; set; }

        public Track[] Tracks { get; set; }

        public Video[] Videos { get; set; }

        public string[] Genres { get; set; }
    }
}
