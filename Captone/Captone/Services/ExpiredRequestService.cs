using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using Captone.Models;
using Captone.Repositories.Interfaces;
using Captone.Services.Interfaces;

namespace Captone.Services
{
    public class ExpiredRequestService : IExpiredRequestService
    {
        private readonly IGenericRepository<Request> _requestRepository;
        public ExpiredRequestService(IGenericRepository<Request> requestRepository)
        {
            _requestRepository = requestRepository;
        }

        public void ExpireRequest()
        {
            var requests = _requestRepository.GetAll().ToList();
            foreach (var request in requests.Where(request => request.DateRequest <= DateTime.Now.AddDays(-1)))
            {
                request.DeliveryStatusID = 9;
            }
            _requestRepository.UpdateList(requests);
        }
    }
}