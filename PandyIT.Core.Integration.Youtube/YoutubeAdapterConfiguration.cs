namespace PandyIT.Core.Integration.Youtube
{
    using System.IO;

    public class YoutubeServiceConfiguration
    {
        public DirectoryInfo WorkingFolder { get; set; }

        public DirectoryInfo OutputFolder { get; set; }

        public string FFMpegPath { get; set; }

        public string GoogleAPIKey { get; set; }

        public string ApplicationName { get; set; }
    }
}
