using Captone.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Captone.Repositories.Interfaces
{
    public interface IRouteRepository
    {
        IQueryable<Route> GetAllRoutes();
        void Edit(Route route);
    }
}