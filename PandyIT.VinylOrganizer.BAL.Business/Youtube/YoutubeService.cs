using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using DiscogsNet;
using log4net;
using log4net.Core;
using PandyIT.Core.Extensions;
using PandyIT.Core.Media;
using PandyIT.Core.Repository;

namespace PandyIT.VinylOrganizer.BAL.Business.Youtube
{
    public class YoutubeService : BaseService
    {
        private YoutubeServiceConfiguration configuration;
        private readonly ILog log;
        private readonly IUnitOfWork recordCaseUnitOfWork;
        private readonly IYoutubeDownloader youtubeDownloader;
        private readonly IFFmpegAdapter ffmpegAdapter;
        private readonly IDiscogs3Api discogs;
        

        public YoutubeService(YoutubeServiceConfiguration configuration, ILog log, IUnitOfWork recordCaseUnitOfWork, IYoutubeDownloader youtubeDownloader, IFFmpegAdapter ffmpegAdapter, IDiscogs3Api discogs)
            : base(recordCaseUnitOfWork)
        {
            this.configuration = configuration;
            this.log = log;
            this.recordCaseUnitOfWork = recordCaseUnitOfWork;
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
                var outputFile = new FileInfo(Path.Combine(outputFolder.FullName, Path.GetFileNameWithoutExtension(videoFile.Name) + ".mp3"));
                ffmpegAdapter.ExtractMp3(videoFile, outputFile);
            }
            finally
            {
                videoFile?.Delete();
            }
        }

        public void ExtractMp3(int discogsId)
        {
            var release = discogs.GetRelease(discogsId);

            this.log.Info(string.Format("Extract MP3 from discogs release {0}: {1}", discogsId, release.ToString()));

            if (!release.Videos.Any())
            {
                this.log.Info(string.Format("No videos found for discogs release {0}", discogsId));
            }

            this.log.Info(string.Format("Found {0} videos for discogs release {1}", release.Videos.Length, discogsId));

            release.Videos?
                .ToList()
                .ForEach(v => ExtractMp3(
                    new Uri(v.Src), 
                    new DirectoryInfo(Path.Combine(configuration.OutputFolder.FullName, release.ToString().ToSafeFilename()))
                    ));
        }

        public void ExtractMp3(IEnumerable<int> discogsIds)
        {
            discogsIds
                .ToList()
                .ForEach(ExtractMp3);
        }
    }
}
