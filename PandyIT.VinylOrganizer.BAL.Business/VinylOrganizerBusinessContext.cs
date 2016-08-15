using System;
using System.Collections.Generic;
using System.Linq;
using DiscogsNet.Api;
using PandyIT.Core.Repository;
using PandyIT.VinylOrganizer.DAL.Model.Entities;

namespace PandyIT.VinylOrganizer.BAL.Business
{
    public class VinylOrganizerBusinessContext : IVinylOrganizerBusinessContext
    {
        private readonly IUnitOfWork unitOfWork;

        public VinylOrganizerBusinessContext(IUnitOfWork recordCaseUnitOfWork)
        {
            if (recordCaseUnitOfWork == null)
            {
                throw new ArgumentNullException(nameof(recordCaseUnitOfWork));
            }

            this.unitOfWork = recordCaseUnitOfWork;
        }

        public void AddMusicTrack(MusicTrack musicTrack)
        {
            this.unitOfWork.GetRepository<MusicTrack>().Add(musicTrack);
        }

        public void AddDiscogsVinyl(int releaseId, int parentLocationId)
        {
            var discogs = new Discogs3("wTIlBQlrElaTrepxOBIw");
            var release = discogs.GetRelease(releaseId);


            var releaseDate = release.ReleaseDate.Split('-');
            var year = releaseDate[0].Length > 0 ? (short?)Convert.ToInt16(releaseDate[0]) : null;
            var month = releaseDate.Length > 1 ? (byte?)Convert.ToByte(releaseDate[1]) : null;
            var day = releaseDate.Length > 2 ? (byte?)Convert.ToByte(releaseDate[2]) : null;


            var vinyl = new LocationVinyl
            {
                Title =  (release.Artists.First().Name + " - " + release.Title),
                Year = year,
                Month = month,
                Day = day,
                Name = year.HasValue ? GetVinylLocationName(year.Value) : "#undefined",
                Genre = release.Genres.First(),
                DiscogsId = releaseId,
                ParentLocationId = parentLocationId
            };

            this.unitOfWork.GetRepository<LocationVinyl>().Add(vinyl);
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

            return "#" + year + "-" + (curMax + 1).ToString().PadLeft(4, '0');
        }
    }
}
