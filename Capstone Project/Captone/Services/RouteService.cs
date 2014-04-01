using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Captone.Models;
using Captone.Repositories.Interfaces;
using Captone.Services.Interfaces;

namespace Captone.Services
{
    public class RouteService : IRouteService
    {
         private readonly IRouteRepository _routeRepository;

         public RouteService(IRouteRepository routeRepository)
        {
            _routeRepository = routeRepository;
        }

        public List<Route> GetAllRoutes()
        {
            return _routeRepository.GetAllRoutes().ToList();
        }
    }
}