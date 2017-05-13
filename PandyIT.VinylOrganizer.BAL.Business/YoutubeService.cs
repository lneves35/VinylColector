using System;
using System.IO;
using System.Linq;
using PandyIT.Core.Repository;
using YoutubeExtractor;

namespace PandyIT.VinylOrganizer.BAL.Business
{
    public class YoutubeService : BaseService
    {
        public YoutubeService(IUnitOfWork recordCaseUnitOfWork) : base(recordCaseUnitOfWork) { }

        public void ExtractAudioMp3()
        {
            
        }

        public void DownloadVideo(Uri source, DirectoryInfo outputFolder)
        {
            var videoInfos = DownloadUrlResolver.GetDownloadUrls(source.AbsoluteUri);
            
            var video = videoInfos.First();

            if (video.RequiresDecryption)
            {
                DownloadUrlResolver.DecryptDownloadUrl(video);
            }

            var videoDownloader = new VideoDownloader(video, Path.Combine(outputFolder.FullName, video.Title + video.VideoExtension));

            videoDownloader.DownloadProgressChanged += (sender, args) => Console.WriteLine(args.ProgressPercentage);

            videoDownloader.Execute();
        }
    }
}
