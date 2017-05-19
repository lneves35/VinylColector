namespace PandyIT.VinylOrganizer.BAL.Business.MixesDB
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using HtmlAgilityPack;
    using PandyIT.Core.Extensions;
    using System;
    using System.Text.RegularExpressions;
    using PandyIT.VinylOrganizer.DAL.Model.Entities;

    public class MixesDbTrackListExtractor
    {
        public IEnumerable<MusicTrack> GetTrackList(string url)
        {
            var rawHtml = new WebClient().DownloadString(url);
            var htmlDoc = new HtmlDocument();
            htmlDoc.LoadHtml(rawHtml);

            var type = GetTrackListType(htmlDoc);
            var trackListLines = GetTrackListLines(type, htmlDoc);
            var musicTracks = trackListLines.Select(CreateMusicTrack);
            return musicTracks;

        }

        private MusicTrack CreateMusicTrack(string line)
        {
            var entries = line.Split(new []{ '-'}, StringSplitOptions.RemoveEmptyEntries);

            return new MusicTrack()
            {
                Artist = entries[0].Trim(),
                Title = entries[1].Trim()
            };
        }

        private static IEnumerable<string> GetTrackListLines(string type, HtmlDocument htmlDoc)
        {
            IEnumerable<string> trackListLines;

            if (type == "Chart")
            {
                trackListLines = htmlDoc.DocumentNode
                    .Descendants("ol")
                    .First()
                    .Descendants()
                    .Select(
                        li => li.InnerText
                    );
            }
            else
            {
                trackListLines = htmlDoc.DocumentNode
                    .Descendants("div")
                    .Where(div => div.Attributes.Contains("class") && div.Attributes["class"].Value.Contains("list-track"))
                    .Select(
                        div => div.InnerText
                    );
            }

            return trackListLines.Select(line => line.RemoveBrackets().Trim()).Distinct();
        }

        private static string GetTrackListType(HtmlDocument htmlDoc)
        {
            var scriptText = htmlDoc.DocumentNode
                .Descendants("script")
                .Single(script => script.InnerText.Contains("wgCanonicalNamespace")).InnerText;

            var pat = "\"wgCanonicalNamespace\"\\s*[:]\\s*\"\\w*\"";
            var r = new Regex(pat, RegexOptions.IgnoreCase);
            var match = r.Matches(scriptText)[0];
            return match.Value
                .Split(':')[1]
                .Trim('\"');
        }
    }
}
