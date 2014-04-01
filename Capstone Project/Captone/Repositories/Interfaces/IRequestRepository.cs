using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Captone.Models;

namespace Captone.Repositories.Interfaces
{
    public interface IRequestRepository
    {
        IQueryable<Request> GetAllRequests();
        IQueryable<Request> GetPendingRequest();
        void Edit(Request request);
    }
}