using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using DiscogsClient;
using DiscogsClient.Data.Query;
using DiscogsClient.Data.Result;
using RestSharpHelper.OAuth1;

namespace PandyIT.VinylOrganizer.BAL.Business.Discogs
{
    public class DiscogsAdapter : IDiscogsAdapter
    {
        private DiscogsClient.DiscogsClient client;

        public DiscogsAdapter(OAuthCompleteInformation oAuth)
        {
            this.client = new DiscogsClient.DiscogsClient(oAuth);            
        }

        public static OAuthCompleteInformation Authenticate(Func<string> reader, OAuthConsumerInformation consumerInformation)
        {
            var discogsAuthentifierClient = new DiscogsAuthentifierClient(consumerInformation);
            return discogsAuthentifierClient.Authorize(s => Task.FromResult(GetToken(s, reader))).Result;
        }

        private static string GetToken(string url, Func<string> reader)
        {
            Console.WriteLine("Please authorize the application and enter the final key in the console");
            Process.Start(url);
            return reader();
        }

        public DiscogsRelease GetRelease(int releaseId)
        {
            return client.GetReleaseAsync(releaseId).Result;
        }

        public IEnumerable<DiscogsSearchResult> Search(DiscogsSearch query)
        {
            return this.client.SearchAsEnumerable(query);
        }

        public IEnumerable<int> ExtractReleaseIdsFromText(string text)
        {
            string[] queries = text.Split(new string[] { Environment.NewLine }, StringSplitOptions.None);
            return queries
                .ToList()
                .Select(q => new DiscogsSearch() {query = q})
                .Select(ds => Search(ds).FirstOrDefault())
                .Where(sr => sr != null)
                .Select(sr => sr.id);
        }
    }
}
