namespace PandyIT.Core.Integration.Discogs
{
    using System;
    using System.Diagnostics;
    using System.Linq;
    using System.Threading.Tasks;
    using DiscogsClient;
    using DiscogsClient.Data.Query;
    using log4net;
    using PandyIT.Core.Integration.Discogs.Entities;
    using PandyIT.VinylOrganizer.BAL.Business.Discogs;
    using RestSharpHelper.OAuth1;

    public class DiscogsAdapter : IDiscogsAdapter
    {
        private readonly DiscogsClient client;

        private readonly ILog log;

        public DiscogsAdapter(OAuthCompleteInformation oAuth, ILog log)
        {
            this.client = new DiscogsClient(oAuth);
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

        public Release GetRelease(int releaseId)
        {
            var discogsRelease = client.GetReleaseAsync(releaseId).Result;
            return new Release(discogsRelease);
        }

        public Release[] Search(string artist, string title)
        {
            var query = new DiscogsSearch()
            {
                query = artist + " " + title,
            };

            var result = this.client
                .SearchAsEnumerable(query)
                .Where(res => res.type == DiscogsEntityType.release)
                .ToArray();

            var releases = result.Where(r => r.type == DiscogsEntityType.release).Select(r => GetRelease(r.id)).ToArray();

            log.Info(string.Format("{0} results found on Discogs search for {1}", releases.Length, query.query));

            return releases;
        }       
    }
}
