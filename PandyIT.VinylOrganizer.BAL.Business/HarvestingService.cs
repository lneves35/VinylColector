using System;
using log4net;
using PandyIT.Core.Repository;
using PandyIT.VinylOrganizer.BAL.Business;
using PandyIT.VinylOrganizer.DAL.Model.Entities;
using PandyIT.VinylOrganizer.Services.Extractors;
using PandyIT.VinylOrganizer.Services.Harvesters;

namespace PandyIT.VinylOrganizer.Services
{
    public class HarvestingService : BaseService
    {
        private readonly ILog log;
        private readonly IYoutubeHarvester youtubeHarvester;

        public HarvestingService(IUnitOfWork recordCaseUnitOfWork, IYoutubeHarvester youtubeHarvester, ILog log)
            : base(recordCaseUnitOfWork)
        {
            this.youtubeHarvester = youtubeHarvester;
            this.log = log;
        }

        public void Add(HarvestingBatch harvestingBatch)
        {
            this.unitOfWork.GetRepository<HarvestingBatch>().Add(harvestingBatch);
        }

        public void Add(HarvestedTrackList harvestedTrackList)
        {
            this.unitOfWork.GetRepository<HarvestedTrackList>().Add(harvestedTrackList);
        }

        public void Add(HarvestedMusicTrack musickTrack)
        {
            this.unitOfWork.GetRepository<HarvestedMusicTrack>().Add(musickTrack);
        }

        public void GetTracksFromMixesDbTrackList(Uri uri)
        {
            var extractingService = new TrackListExtractingService(log);
            extractingService.AddExtractor(new MixesDbTrackListExtractor(log));
            
            var harvestingBatch = new HarvestingBatch()
            {
                Date = DateTime.Now
            };

            this.Add(harvestingBatch);

            var trackLists = extractingService.ExtractTrackListsFromUrl(uri);
            foreach (var trackList in trackLists)
            {
                var harvestedTrackList = new HarvestedTrackList()
                {
                    Title = "hello",
                    Uri = uri.AbsoluteUri,
                    HarvestingBatchId = harvestingBatch.HarvestingBatchId
                };
                this.Add(harvestedTrackList);

                foreach (var track in trackList)
                {
                    var musicTrack = new HarvestedMusicTrack()
                    {
                        HarvestedTrackListId = harvestedTrackList.HarvestedTrackListId,
                        Artist = track.Artist,
                        Title = track.Title,
                    };

                    youtubeHarvester.HarvestMusicTrack(musicTrack);
                    this.Add(musicTrack);
                }
            }
        }
    }
}
