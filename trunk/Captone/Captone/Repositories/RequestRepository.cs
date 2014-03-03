using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Captone.Models;
using Captone.Repositories.Interfaces;

namespace Captone.Repositories
{
    public class RequestRepository: BaseRepository, IRequestRepository
    {
        public RequestRepository(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }

        public IQueryable<Request> GetAllRequests()
        {
            return GetDbSet<Request>();
        }

        public IQueryable<Request> GetPendingRequest()
        {
            return GetDbSet<Request>().Where(i => i.DeliveryStatusID == 1);
        }
    }
}