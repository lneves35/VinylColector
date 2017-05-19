using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using DiscogsClient;
using DiscogsClient.Data.Query;
using DiscogsClient.Data.Result;
using log4net;
using RestSharpHelper.OAuth1;

namespace PandyIT.VinylOrganizer.BAL.Business.Discogs
{
    public class DiscogsAdapter : IDiscogsAdapter
    {
        private readonly DiscogsClient.DiscogsClient client;

        private readonly ILog log;

        public DiscogsAdapter(OAuthCompleteInformation oAuth, ILog log)
        {
            this.client = new DiscogsClient.DiscogsClient(oAuth);
            this.log = log;
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

        public DiscogsMaster GetMaster(int masterId)
        {
            return client.GetMasterAsync(masterId).Result;
        }

        public IEnumerable<DiscogsSearchResult> Search(DiscogsSearch query)
        {
            var result = this.client.SearchAsEnumerable(query)
                .ToArray();

            log.Info(string.Format("{0} results found on Discogs search for {1}", result.Length, query.query));
            return result;
        }       
    }
}
