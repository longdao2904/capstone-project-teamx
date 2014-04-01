using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Captone.Models;
using Captone.Repositories.Interfaces;
using Captone.Services.Interfaces;

namespace Captone.Services
{
    public class TripService : ITripService
    {
        private readonly ITripRepository _tripRepository;

        public TripService(ITripRepository tripRepository)
        {
            _tripRepository = tripRepository;
        }

        public List<Trip> GetAllTrips()
        {
            return _tripRepository.GetAllTrips().ToList();
        }
    }
}