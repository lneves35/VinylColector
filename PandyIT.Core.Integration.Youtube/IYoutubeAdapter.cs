using System;
using System.Collections.Generic;
using System.IO;

namespace PandyIT.Core.Integration.Youtube
{
    public interface IYoutubeAdapter
    {
        IEnumerable<YoutubeSearchResult> Search(string text);
        YoutubeDownloadResult DownloadVideo(Uri source, DirectoryInfo outputFolder);
        YoutubeDownloadResult ExtractMp3(Uri videoSource, DirectoryInfo outputFolder);
        YoutubeDownloadResult ExtractMp3(Uri videoSource);
    }
}