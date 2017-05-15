using System;
using System.Text;
using System.Text.RegularExpressions;
using PandyIT.VinylOrganizer.BAL.Business.Discogs;

namespace PandyIT.VinylOrganizer.BAL.Business.Parsers
{
    public class ParsingService
    {
        private string pattern = @"\w* - \w*";

        private IDiscogsAdapter discogs;

        public ParsingService(IDiscogsAdapter discogs)
        {
            this.discogs = discogs;
        }

        public void ParseTextLineToDiscogs(string textLine)
        {
            var splitted = ValidateAndSplit(textLine);
            var artist = splitted[0].Trim();
            var title = splitted[1].Trim();

            //SearchQuery query = new SearchQuery();
            //query.Artist = artist;
            //query.Title = title;
            //query.Query = "q=Nirvana&token=qXdqQlDKAhFpWYTcTgVIDmvehYahJBvAqZvNbiHF";

            //var sb = new StringBuilder();
            //sb.AddQueryParam("token", "qXdqQlDKAhFpWYTcTgVIDmvehYahJBvAqZvNbiHF");
            //query.AddQueryParams(sb);

            //var results = discogsApi.Search(query);


        }

        private string[] ValidateAndSplit(string textLine)
        {
            var rgx = new Regex(pattern, RegexOptions.IgnoreCase);
            var matches = rgx.Matches(textLine);

            if (matches.Count != 1)
            {
                throw new Exception("Parsing error");
            }

            var splitted = textLine.Split(new[] {'-'}, StringSplitOptions.RemoveEmptyEntries);
            return splitted;
        }
    }
}
