using System.Collections.Generic;
using System.Linq;
using Captone.Models;

namespace Captone.Repositories.Interfaces
{
    public interface ITripRepository
    {
        IQueryable<Trip> GetAllTrips();
        void Edit(Trip trip);
    }
}

