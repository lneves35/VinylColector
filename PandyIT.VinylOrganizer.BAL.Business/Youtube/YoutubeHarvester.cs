using System;
using System.IO;
using System.Linq;
using DiscogsClient.Data.Query;
using DiscogsClient.Data.Result;
using log4net;
using PandyIT.Core.Extensions;
using PandyIT.Core.Media;
using PandyIT.Core.Repository;
using PandyIT.VinylOrganizer.BAL.Business.Discogs;

namespace PandyIT.VinylOrganizer.BAL.Business.Youtube
{
    public class YoutubeHarvester : BaseService, IYoutubeHarvester
    {
        private readonly YoutubeServiceConfiguration configuration;
        private readonly ILog log;
        private readonly IYoutubeDownloader youtubeDownloader;
        private readonly IFFmpegAdapter ffmpegAdapter;
        private readonly IDiscogsAdapter discogs;
        

        public YoutubeHarvester(YoutubeServiceConfiguration configuration, ILog log, IUnitOfWork recordCaseUnitOfWork, IYoutubeDownloader youtubeDownloader, IFFmpegAdapter ffmpegAdapter, IDiscogsAdapter discogs)
            : base(recordCaseUnitOfWork)
        {
            this.configuration = configuration;
            this.log = log;
            this.youtubeDownloader = youtubeDownloader;
            this.ffmpegAdapter = ffmpegAdapter;
            this.discogs = discogs;
        }

        public void ExtractMp3(Uri videoSource)
        {
            this.ExtractMp3(videoSource, configuration.OutputFolder);
        }

        public void ExtractMp3(Uri videoSource, DirectoryInfo outputFolder)
        {
            FileInfo videoFile = null;
            Directory.CreateDirectory(outputFolder.FullName);
            try
            {
                videoFile = youtubeDownloader.DownloadVideo(videoSource, configuration.WorkingFolder);
                var outputFile =
                    new FileInfo(Path.Combine(outputFolder.FullName,
                        Path.GetFileNameWithoutExtension(videoFile.Name) + ".mp3"));
                ffmpegAdapter.ExtractMp3(videoFile, outputFile);
            }
            catch (YoutubeDownloaderException e)
            {
                //Should mark as unsuccessful download
            }
            finally
            {
                videoFile?.Delete();
            }
        }

        public void ExtractMp3(int discogsId, DiscogsEntityType entityType)
        {
            DiscogsReleaseBase release;
            if (entityType == DiscogsEntityType.release)
            {
                release = discogs.GetRelease(discogsId);
            }
            else
            {
                release = discogs.GetMaster(discogsId);
            }
           
            this.log.Info(string.Format("Process discogs release {0}: {1}", discogsId, release));

            if (release.videos == null)
            {
                this.log.Info(string.Format("No videos found for discogs release {0}", discogsId));
                return;
            }

            this.log.Info(string.Format("Found {0} videos for discogs release {1}", release.videos.Length, discogsId));

            var folderName = string.Format("{0} - {1}", release.artists.First().name, release.title).ToSafeFilename();

            release.videos
                .ToList()
                .ForEach(v => ExtractMp3(
                    new Uri(v.uri), 
                    new DirectoryInfo(Path.Combine(configuration.OutputFolder.FullName, folderName))
                    ));
        }

        public void ExtractMp3FromTextLines(string text)
        {
            var lines = text.Split(new string[] { Environment.NewLine }, StringSplitOptions.None);

            foreach (var line in lines)
            {
                var searchQuery = new DiscogsSearch() {query = line};
                var firstResult = discogs.Search(searchQuery).FirstOrDefault();
                if (firstResult != null)
                {
                    this.ExtractMp3(firstResult.id, firstResult.type);
                }
            }
        }

        public void ExtractMp3FromTextFile(string filePath)
        {
            string readText = File.ReadAllText(filePath);
            this.ExtractMp3FromTextLines(readText);
        }
    }
}
