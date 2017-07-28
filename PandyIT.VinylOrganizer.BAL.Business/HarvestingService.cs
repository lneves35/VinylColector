using System;
using System.Collections.Generic;
using System.Linq;
using log4net;
using PandyIT.Core.Extensions;
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

        public HarvestedTrackList FetchHarvestedTrackListByUriHash(string hash)
        {
            return this.unitOfWork.GetRepository<HarvestedTrackList>()
                .Find(htl => htl.UriHash == hash)
                .SingleOrDefault();
        }

        public void HarvestTracklists(Uri uri)
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
                var alreadyHarvested = (this.FetchHarvestedTrackListByUriHash(trackList.Uri.MD5()) != null);

                if (!alreadyHarvested)
                {
                    var musicTracks = trackList.HarvestedMusicTracks;
                    trackList.HarvestingBatchId = harvestingBatch.HarvestingBatchId;
                    trackList.HarvestedMusicTracks = new List<HarvestedMusicTrack>();
                    this.Add(trackList);

                    foreach (var track in musicTracks)
                    {
                        track.HarvestedTrackListId = trackList.HarvestedTrackListId;
                        youtubeHarvester.HarvestMusicTrack(track);
                        this.Add(track);

                        if (track.FilePath != null)
                        {
                            //Write ID3 Tag
                        }
                    }
                }
            }
        }
    }
}
