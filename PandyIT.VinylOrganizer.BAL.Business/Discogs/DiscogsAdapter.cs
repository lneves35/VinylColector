using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using DiscogsClient;
using DiscogsClient.Data.Query;
using DiscogsClient.Data.Result;
using DiscogsClient.Internal;
using RestSharpHelper.OAuth1;

namespace PandyIT.VinylOrganizer.BAL.Business.Discogs
{
    public class DiscogsAdapter : IDiscogsAdapter
    {
        public static OAuthCompleteInformation Authenticate(Func<string> reader)
        {
            //Create authentificator information: you should fournish real keys here
            var oAuthConsumerInformation = new OAuthConsumerInformation("wTIlBQlrElaTrepxOBIw", "ULEFpvSYgzzafOLXRIOsIaUwUAlAHfam");

            //Create Authentifier client
            var discogsAuthentifierClient = new DiscogsAuthentifierClient(oAuthConsumerInformation);

            //Retreive Token and Token secret 
            var oauth = discogsAuthentifierClient.Authorize(s => Task.FromResult(GetToken(s, reader))).Result;
            return oauth;
        }

        private static string GetToken(string url, Func<string> reader)
        {
            Console.WriteLine("Please authorize the application and enter the final key in the console");
            Process.Start(url);
            return reader();
        }

        private DiscogsClient.DiscogsClient client;

        public DiscogsAdapter(OAuthCompleteInformation oAuth)
        {
            this.client = new DiscogsClient.DiscogsClient(oAuth);            
        }

        public DiscogsRelease GetRelease(int releaseId)
        {
            return client.GetReleaseAsync(releaseId).Result;
        }

        public IEnumerable<DiscogsSearchResult> Search(DiscogsSearch query)
        {
            return this.client.SearchAsEnumerable(query);
        }
    }
}
