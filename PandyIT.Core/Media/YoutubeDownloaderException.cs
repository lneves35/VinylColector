using System;

namespace PandyIT.Core.Media
{
    public class YoutubeDownloaderException : Exception
    {
        public YoutubeDownloaderException(Exception ex) : base(ex.Message)
        {
        }
    }
}
