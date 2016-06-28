using System;
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
    }
}
