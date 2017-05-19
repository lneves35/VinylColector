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
}
