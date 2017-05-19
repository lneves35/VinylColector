using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Google.Apis.Services;
using Google.Apis.YouTube.v3;

namespace PandyIT.Core.Integration.Youtube
{
    public class YoutubeAdapter
    {
        public static void Test()
        {
            var youtubeService = new YouTubeService(new BaseClientService.Initializer()
            {
                ApiKey = "AIzaSyCb_j_yNGwAuQqKpPgWt-XSsm8eEhmDhYQ",
                ApplicationName = "Rerec"
            });

            var searchListRequest = youtubeService.Search.List("snippet");
            searchListRequest.Q = "hello world"; 
            searchListRequest.MaxResults = 50;

            var searchListResponse = searchListRequest.Execute();

        }
    }
}
