using System;
using System.IO;

namespace PandyIT.Core.Media
{
    public interface IYoutubeDownloader
    {
        FileInfo DownloadVideo(Uri source, DirectoryInfo outputFolder);
    }
}