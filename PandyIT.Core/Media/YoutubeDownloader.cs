using System;
using System.IO;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using log4net;
using PandyIT.Core.Extensions;
using YoutubeExtractor;

namespace PandyIT.Core.Media
{
    public class YoutubeDownloader : IYoutubeDownloader
    {
        private ILog log;

        public EventHandler<ProgressEventArgs> ProgressHandler { get; set; }

        public YoutubeDownloader(ILog log, EventHandler<ProgressEventArgs> progressHandler)
        {
            this.log = log;
            this.ProgressHandler = progressHandler;
        }

        public FileInfo DownloadVideo(Uri source, DirectoryInfo outputFolder)
        {
            this.log.Info(string.Format("Downloading video: {0}", source.AbsoluteUri));

            Directory.CreateDirectory(outputFolder.FullName);
            try
            {

                var videoInfos = DownloadUrlResolver.GetDownloadUrls(source.AbsoluteUri);

                var video = videoInfos.First(vi => vi.VideoType == VideoType.Mp4);

                if (video.RequiresDecryption)
                {
                    DownloadUrlResolver.DecryptDownloadUrl(video);
                }

                var outputFile = Path.Combine(outputFolder.FullName, video.Title.ToSafeFilename() + video.VideoExtension);
                var videoDownloader = new VideoDownloader(video, outputFile);


                if (this.ProgressHandler != null)
                {
                    videoDownloader.DownloadProgressChanged += this.ProgressHandler;
                }

            
                videoDownloader.Execute();
                return new FileInfo(outputFile);
            }
            catch (Exception ex)
            {
                log.Error(string.Format("Error downloading {0}: {1}", source, ex.Message));
                throw new YoutubeDownloaderException(ex);
            }
        }

        
    }
}
