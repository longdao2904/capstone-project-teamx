using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Captone.Models;

namespace Captone.Repositories.Interfaces
{
    public interface IStationRepository
    {
        IQueryable<Station> GetAllStations();
    }
}