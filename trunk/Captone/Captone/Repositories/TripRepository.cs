using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Providers.Entities;
using Captone.Models;
using Captone.Repositories.Interfaces;

namespace Captone.Repositories
{
    public class TripRepository : BaseRepository, ITripRepository
    {
        public TripRepository(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }

        public IQueryable<Trip> GetAllTrips()
        {
            return base.GetAll<Trip>();
        }

        public void Edit(Trip trip)
        {
            base.Update(trip);
        }
    }
}