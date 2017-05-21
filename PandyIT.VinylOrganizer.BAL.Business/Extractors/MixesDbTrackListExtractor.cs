using System.Diagnostics.Eventing.Reader;

namespace PandyIT.VinylOrganizer.Services.Extractors
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using HtmlAgilityPack;
    using log4net;
    using PandyIT.Core.Extensions;
    using PandyIT.VinylOrganizer.DAL.Model.Entities;

    public class MixesDbTrackListExtractor : ITrackListExtractor
    {
        private ILog log;

        public MixesDbTrackListExtractor(ILog log)
        {
            this.log = log;
        }

        public IEnumerable<MusicTrack> GetTrackList(Uri url)
        {
            var rawHtml = new WebClient().DownloadString(url);
            var htmlDoc = new HtmlDocument();
            htmlDoc.LoadHtml(rawHtml);

            var trackListLines = GetTrackListLines(htmlDoc).ToArray();

            var musicTracks = trackListLines.Select(CreateMusicTrack);

            var parsingInfo = string.Format("-------- Parsed {0} tracks from {1} -------- ", trackListLines.Length, url.AbsoluteUri);
            log.Info(parsingInfo);
            //trackListLines.ToList().ForEach(track => this.log.Info(track));

            return musicTracks;
        }

        public bool CanExtract(Uri uri)
        {
            return uri.Host == "www.mixesdb.com";
        }

        private static MusicTrack CreateMusicTrack(string line)
        {
            var entries = line.Split(new []{ '-'}, StringSplitOptions.RemoveEmptyEntries);

            return new MusicTrack()
            {
                Artist = entries[0].Trim(),
                Title = entries[1].Trim()
            };
        }

        private static IEnumerable<string> GetTrackListLines(HtmlDocument htmlDoc)
        {
            var ol = htmlDoc.DocumentNode.Descendants("ol").ToArray();

            if (!ol.Any())
            {
                return new string[] {};
            }

            return ol.First()
                .Descendants()
                .Select(
                    li => li.InnerText
                    )
                .Select(line => line.RemoveBrackets().Trim())
                .Where(line => line.IndexOf("-") < line.Length -1 && line.IndexOf("-") > 0)
                .Distinct();
        }
    }
}
