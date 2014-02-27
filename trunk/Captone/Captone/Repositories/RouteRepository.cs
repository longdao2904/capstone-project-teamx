using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Captone.Models;
using Captone.Repositories.Interfaces;

namespace Captone.Repositories
{
    public class RouteRepository : BaseRepository, IRouteRepository
    {
        public RouteRepository(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }

        public IQueryable<Route> GetAllRoutes()
        {
            return base.GetAll<Route>();
        }
    }
}