using System;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using DiscogsClient.Data.Query;
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

        public int ParseTextLineToDiscogsId(string textLine)
        {
            var query = new DiscogsSearch()
            {
                query = textLine
            };

            var result = discogs.Search(query);
            var array =  result.ToArray();
            return 0;
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
