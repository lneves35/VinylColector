using System;
using PandyIT.Core.Repository;
using PandyIT.VinylOrganizer.DAL.Model.Entities;

namespace PandyIT.VinylOrganizer.BAL.Business
{
    public class VinylOrganizerBusinessContext : IVinylOrganizerBusinessContext
    {
        private IUnitOfWork vinylOrganizer;

        public VinylOrganizerBusinessContext(IUnitOfWork recordCaseUnitOfWork)
        {
            if (recordCaseUnitOfWork == null)
            {
                throw new ArgumentNullException(nameof(recordCaseUnitOfWork));
            }

            this.vinylOrganizer = recordCaseUnitOfWork;
        }

        public void AddMusicTrack(MusicTrack musicTrack)
        {
            this.vinylOrganizer.GetRepository<MusicTrack>().Add(musicTrack);
        }
    }
}
