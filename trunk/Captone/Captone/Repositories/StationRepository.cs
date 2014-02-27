using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Captone.Models;
using Captone.Repositories.Interfaces;

namespace Captone.Repositories
{
    public class StationRepository : BaseRepository, IStationRepository
    {
        public StationRepository(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }

        public IQueryable<Station> GetAllStations()
        {
            return base.GetAll<Station>();
        }
    }
}