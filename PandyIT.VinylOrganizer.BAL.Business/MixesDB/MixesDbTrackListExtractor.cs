using System;
using System.Text.RegularExpressions;

namespace PandyIT.VinylOrganizer.BAL.Business.MixesDB
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using HtmlAgilityPack;
    using PandyIT.Core.Extensions;

    public class MixesDbTrackListExtractor
    {
        public IEnumerable<string> GetTrackList(string url)
        {
            var rawHtml = new WebClient().DownloadString(url);
            var htmlDoc = new HtmlDocument();
            htmlDoc.LoadHtml(rawHtml);



            var type = GetTrackListType(htmlDoc);
            if (type == "Chart")
            {
                return htmlDoc.DocumentNode
                .Descendants("ol")
                .First()
                .Descendants()
                .Select(
                    li => li.InnerText
                    .RemoveBrackets()
                    .Trim()
                ).Distinct();
            }
            else
            {
                return htmlDoc.DocumentNode
                .Descendants("div")
                .Where(div => div.Attributes.Contains("class") && div.Attributes["class"].Value.Contains("list-track"))
                .Select(
                    div => div.InnerText
                    .RemoveBrackets()
                    .Trim()
                ).Distinct();
            }
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
