using System;
using log4net;
using PandyIT.VinylOrganizer.DAL.Model.Entities;

namespace PandyIT.VinylOrganizer.Services.Extractors
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using HtmlAgilityPack;

    public class TrackListExtractingService
    {
        private readonly List<ITrackListExtractor> extractors;
        private readonly ILog log;

        public TrackListExtractingService(ILog log)
        {
            this.extractors = new List<ITrackListExtractor>();
            this.log = log;
        }

        public void AddExtractor(ITrackListExtractor extractor)
        {
            this.extractors.Add(extractor);
        }

        public IEnumerable<HarvestedTrackList> ExtractTrackListsFromUrl(Uri baseUri)
        {
            var result = new List<HarvestedTrackList>();

            var rawHtml = new WebClient().DownloadString(baseUri);
            var htmlDoc = new HtmlDocument();
            htmlDoc.LoadHtml(rawHtml);

            var uris = htmlDoc.DocumentNode
                .Descendants("a")
                .Where(node => node.Attributes.Contains("href"))
                .Select(node => GetUri(node, baseUri))
                .Where(uri => uri!= null)
                .ToArray();

            foreach (var uri in uris)
            {
                var extractor = extractors.FirstOrDefault(ex => ex.CanExtract(uri));

                if (extractor != null)
                {
                    try
                    {
                        var trackList = extractor.GetTrackList(uri);
                        result.Add(trackList);
                    }
                    catch (Exception ex)
                    {
                        var error = string.Format("Error getting tracklist from: {0}{1}{2}", uri.AbsoluteUri, Environment.NewLine, ex.Message);
                        log.Error(ex.Message);
                    }
                }
            }

            return result;
        }

        private Uri GetUri(HtmlNode node, Uri baseUri)
        {
            Uri outUri = null;
            var href = node.Attributes["href"].Value;

            if (href.StartsWith("/"))
            {
                href = baseUri.GetLeftPart(UriPartial.Authority) + href;
            };

            Uri.TryCreate(href, UriKind.Absolute, out outUri);
            return outUri;
        }
    }
}
