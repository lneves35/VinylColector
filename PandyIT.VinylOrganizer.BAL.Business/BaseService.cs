using System;
using PandyIT.Core.Repository;

namespace PandyIT.VinylOrganizer.BAL.Business
{
    public class BaseService
    {
        protected readonly IUnitOfWork unitOfWork;

        public BaseService(IUnitOfWork recordCaseUnitOfWork)
        {
            if (recordCaseUnitOfWork == null)
            {
                throw new ArgumentNullException(nameof(recordCaseUnitOfWork));
            }

            this.unitOfWork = recordCaseUnitOfWork;
        }
    }
}
