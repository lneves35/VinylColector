namespace PandyIT.Core.Integration.Youtube
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using Google.Apis.Services;
    using Google.Apis.YouTube.v3;
    using log4net;
    using PandyIT.Core.Extensions;
    using YoutubeExtractor;

    public class YoutubeAdapter : IYoutubeAdapter
    {
        private readonly YoutubeServiceConfiguration configuration;
        private readonly ILog log;
        private readonly IFFmpegAdapter ffmpegAdapter;        

        public YoutubeAdapter(YoutubeServiceConfiguration configuration, ILog log)
        {
            this.configuration = configuration;
            this.log = log;
            this.ffmpegAdapter = new FFmpegAdapter(new FileInfo(configuration.FFMpegPath), log);
        }

        public IEnumerable<YoutubeSearchResult> Search(string text)
        {
            var youtubeService = new YouTubeService(new BaseClientService.Initializer()
            {
                ApiKey = this.configuration.GoogleAPIKey,
                ApplicationName = configuration.ApplicationName
            });

            var searchListRequest = youtubeService.Search.List("snippet");
            searchListRequest.Q = text;
            searchListRequest.MaxResults = 10;
            searchListRequest.Type = "video";

            var searchListResponse = searchListRequest.Execute();
            return searchListResponse.Items.Select(i => new YoutubeSearchResult(i));
        }

        public YoutubeDownloadResult DownloadVideo(Uri source, DirectoryInfo outputFolder)
        {
            Directory.CreateDirectory(outputFolder.FullName);
            try
            {

                var videoInfos = DownloadUrlResolver.GetDownloadUrls(source.AbsoluteUri);

                var video = videoInfos.First(vi => vi.VideoType == VideoType.Mp4);

                if (video.RequiresDecryption)
                {
                    DownloadUrlResolver.DecryptDownloadUrl(video);
                }
                this.log.Info(string.Format("Downloading video: {0} ({1})", source.AbsoluteUri, video.Title));
                var outputFile = Path.Combine(outputFolder.FullName, video.Title.ToSafeFilename() + video.VideoExtension);
                var videoDownloader = new VideoDownloader(video, outputFile);

                videoDownloader.Execute();

                return new YoutubeDownloadResult()
                {
                    HasError = false,
                    FilePath = outputFile
                };
            }
            catch (Exception ex)
            {
                var error = string.Format("Error downloading {0}: {1}", source, ex.Message);
                this.log.Error(error);
                return new YoutubeDownloadResult()
                {
                    HasError = true,
                    Error = error
                };
            }
        }

        public YoutubeDownloadResult ExtractMp3(Uri videoSource, DirectoryInfo outputFolder)
        {
            Directory.CreateDirectory(outputFolder.FullName);
            
            var videoResult = DownloadVideo(videoSource, configuration.WorkingFolder);

            if (videoResult.HasError)
            {
                return videoResult;
            }

            var videoFile = new FileInfo(videoResult.FilePath);

            var outputFile = new FileInfo(
                Path.Combine(
                    outputFolder.FullName,
                    Path.GetFileNameWithoutExtension(videoFile.Name) + ".mp3")
                    );

            ffmpegAdapter.ExtractMp3(videoFile, outputFile);

            return new YoutubeDownloadResult()
            {
                HasError = false,
                FilePath = outputFile.FullName
            };
        }

        public YoutubeDownloadResult ExtractMp3(Uri videoSource)
        {
            return this.ExtractMp3(videoSource, configuration.OutputFolder);
        }
    }
}
