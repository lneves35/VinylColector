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

        public void AddVinyl(int id)
        {
            var discogs = new Discogs3("wTIlBQlrElaTrepxOBIw");
            var release = discogs.GetRelease(id);

            var releaseDate = Convert.ToDateTime(release.ReleaseDate);

            var vinyl = new LocationVinyl
            {
                Name =  (release.Artists.First().Name + " - " + release.Title),
                ReleaseDate = releaseDate,
                Ref = GetVinylRef(releaseDate)
            };

            this.unitOfWork.GetRepository<LocationVinyl>().Add(vinyl);
        }

        public IEnumerable<LocationVinyl> GetAllLocationVinyl()
        {
            return this.unitOfWork.GetRepository<LocationVinyl>().Find(v => true);
        }

        public string GetVinylRef(DateTime date)
        {
            var curMax = this.unitOfWork.GetRepository<LocationVinyl>()
                .Find(lv => lv.ReleaseDate.HasValue && lv.ReleaseDate.Value.Year == date.Year)
                .Count();

            return "#" + date.Year + "-" + (curMax + 1).ToString().PadLeft(4, '0');
        }
    }
}
