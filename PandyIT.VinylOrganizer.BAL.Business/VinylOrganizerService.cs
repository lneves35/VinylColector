﻿using System.Collections.Generic;
using System.Linq;
using log4net;
using PandyIT.Core.Repository;
using PandyIT.VinylOrganizer.BAL.Business.Discogs;
using PandyIT.VinylOrganizer.DAL.Model.Entities;

namespace PandyIT.VinylOrganizer.BAL.Business
{
    public class VinylOrganizerService : BaseService, IVinylOrganizerService
    {
        private readonly IDiscogsAdapter discogs;
        private readonly ILog log;

        private int requestCount = 0;

        public VinylOrganizerService(IUnitOfWork recordCaseUnitOfWork, IDiscogsAdapter discogs, ILog log)
            : base(recordCaseUnitOfWork)
        {
            this.discogs = discogs;
            this.log = log;
        }

        public void AddMusicTrack(MusicTrack musicTrack)
        {
            this.unitOfWork.GetRepository<MusicTrack>().Add(musicTrack);
        }

        public void AddDiscogsVinyl(int releaseId, int parentLocationId)
        {            
            var release = this.discogs.GetRelease(releaseId);

            var releaseDate = release.date_added;

            var vinyl = new LocationVinyl
            {
                Title =  (release.artists.First().name + " - " + release.title),
                Year = (short)releaseDate.Year,
                Month = (byte)releaseDate.Month,
                Day = (byte)releaseDate.Day,
                Name = GetVinylLocationName((short)releaseDate.Year),
                Genre = release.genres?.First(),
                DiscogsId = releaseId,
                ParentLocationId = parentLocationId
            };

            this.unitOfWork.GetRepository<LocationVinyl>().Add(vinyl);
            log.Info(string.Format("{0} Discogs vinyl release added: {1} ", ++requestCount, (release.artists.First().name + " - " + release.title)));
        }

        public void AddLocation(Location location)
        {
            this.unitOfWork.GetRepository<Location>()
                .Add(location);
        }

        public LocationVinyl FetchVinylByDiscogsId(int id)
        {
            return this.unitOfWork.GetRepository<LocationVinyl>()
                .Find(lv => lv.DiscogsId == id)
                .Single();
        }

        public LocationVinyl FetchVinylByName(string name)
        {
            return this.unitOfWork.GetRepository<LocationVinyl>()
                .Find(lv => lv.Name == name)
                .Single();
        }

        public IEnumerable<LocationVinyl> GetAllLocationVinyl()
        {
            return this.unitOfWork.GetRepository<LocationVinyl>()
                .Find(v => true);
        }

        public string GetVinylLocationName(short year)
        {
            var curMax = this.unitOfWork.GetRepository<LocationVinyl>()
                .Find(lv => lv.Year.HasValue && lv.Year.Value == year)
                .Count();

            return "#" + year.ToString().PadLeft(4, '0') + "-" + (curMax + 1).ToString().PadLeft(4, '0');
        }
    }
}
